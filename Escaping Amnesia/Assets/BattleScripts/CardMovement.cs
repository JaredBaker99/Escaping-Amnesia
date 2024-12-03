using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using BattleCards;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // RectTransform rectTransform; gets rectTransform of our current object
    // private Canvas canvas;gets basic canvas
    // stores pointer position mouse pointer
    // stores original location of card
    // storing rotation
    // storing original position
    private RectTransform rectTransform;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    private Vector3 originalScale;
    private int currentState = 0;
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private GridManager gridManager;
    private readonly int maxRow = 1;

    [SerializeField] private float selectScale = 1.1f;
    [SerializeField] private Vector2 cardPlay;
    [SerializeField] private Vector3 playPosition;
    [SerializeField] private GameObject glowEffect;
    [SerializeField] private GameObject playArrow;
    [SerializeField] private float lerpFactor = 0.1f;
    [SerializeField] private int cardPlayDivider = 4;
    [SerializeField] private float cardPlayMultiplier = 1f;
    [SerializeField] private bool needUpdateCardPlayPosition = false;
    [SerializeField] private int playPositionYDivider = 2;
    [SerializeField] private float playPositionYMultiplier = 1f;
    [SerializeField] private int playPositionXDivider = 4;
    [SerializeField] private float playPositionXMultiplier = 1f;
    [SerializeField] private bool needUpdatePlayPosition = false;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();

        if (canvas != null)
        {
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;

        updateCardPlayPostion();
        updatePlayPosition();
        gridManager = FindAnyObjectByType<GridManager>();
    }

    void Update()
    {
        if (needUpdateCardPlayPosition)
        {
            updateCardPlayPostion();
        }

        if (needUpdatePlayPosition)
        {
            updatePlayPosition();
        }
        
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                if (!Input.GetMouseButton(0)) 
                {
                    TransitionToState0();
                }
                break;
            case 3:
                HandlePlayState();
                break;
        }
    }

    private void TransitionToState0()
    {
        currentState = 0;
        GameManager.Instance.PlayingCard = false;
        rectTransform.localScale = originalScale; //Reset Scale
        rectTransform.localRotation = originalRotation; //Reset Rotation
        rectTransform.localPosition = originalPosition; //Reset Position
        glowEffect.SetActive(false); //Disable glow effect
        playArrow.SetActive(false); //Disable playArrow
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (currentState == 0)
        {
            originalPosition = rectTransform.localPosition;
            originalRotation = rectTransform.localRotation;
            originalScale = rectTransform.localScale;

            currentState = 1;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            TransitionToState0();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentState == 1)
        {
            currentState = 2;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            if (Input.mousePosition.y > cardPlay.y)
            {
                currentState = 3;
                playArrow.SetActive(true);
                rectTransform.localPosition = Vector3.Lerp(rectTransform.position, playPosition, lerpFactor);
            }
        }
    }

    private void HandleHoverState()
    {
        glowEffect.SetActive(true);
        rectTransform.localScale = originalScale * selectScale;
    }

    private void HandleDragState()
    {
        //Set the card's rotation to zero
        rectTransform.localRotation = Quaternion.identity;
        rectTransform.position = Vector3.Lerp(rectTransform.position, Input.mousePosition, lerpFactor);
    }

    private void HandlePlayState()
    {
        if (!GameManager.Instance.PlayingCard)
        {
            GameManager.Instance.PlayingCard = true;
        }
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (!Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.GetComponent<GridCell>())
            {
                GridCell cell = hit.collider.GetComponent<GridCell>();
                Vector2 targetPos = cell.gridIndex;
                //cell.gridIndex.x < maxColumn &&
                Debug.Log("Before the if statement: ");
                if (GetComponent<CardDisplay>().cardData is SpellCards)
                {
                    if(GameManager.Instance.currentEnergy >= GetComponent<CardDisplay>().cardData.energy)
                    {
                        int energyDifference = GameManager.Instance.currentEnergy - GetComponent<CardDisplay>().cardData.energy;
                        SpellCards currentCard = (SpellCards) GetComponent<CardDisplay>().cardData;
                        Debug.Log(currentCard.cardName);
                        int who = currentCard.WhatTarget(currentCard);
                        Debug.Log("The value of who: " + who);
                        // you are a spell card affecting the enemy...
                        if (who == 1 || who == 3)
                        {
                            if (cell.gridIndex.y == maxRow && gridManager.AddSpellToGrid((SpellCards) GetComponent<CardDisplay>().cardData, targetPos))
                            {
                                HandManager handManager = FindAnyObjectByType<HandManager>();
                                DiscardManager discardManager = FindObjectOfType<DiscardManager>();
                                discardManager.AddToDiscard(GetComponent<CardDisplay>().cardData);
                                Debug.Log("Before remove: ");
                                Debug.Log(gameObject);
                                handManager.cardsInHand.Remove(gameObject);
                                Debug.Log("After remove: ");
                                Debug.Log(gameObject);
                                handManager.UpdateHandVisuals();
                                Debug.Log("placed Character");
                                GameManager.Instance.currentEnergy = energyDifference;
                                Destroy(gameObject);
                            }
                        } else
                        {
                            // if you aren't affecting the enemy you are affecting the player...
                            if (cell.gridIndex.y < maxRow && gridManager.AddSpellToGrid((SpellCards) GetComponent<CardDisplay>().cardData, targetPos))
                            {
                                HandManager handManager = FindAnyObjectByType<HandManager>();
                                DiscardManager discardManager = FindObjectOfType<DiscardManager>();
                                discardManager.AddToDiscard(GetComponent<CardDisplay>().cardData);
                                Debug.Log("Before remove: ");
                                Debug.Log(gameObject);
                                handManager.cardsInHand.Remove(gameObject);
                                Debug.Log("After remove: ");
                                Debug.Log(gameObject);
                                handManager.UpdateHandVisuals();
                                Debug.Log("placed Character");
                                GameManager.Instance.currentEnergy = energyDifference;
                                Destroy(gameObject);
                            }
                        }
                    }
                }

                else if (GameManager.Instance.currentEnergy >= GetComponent<CardDisplay>().cardData.energy)
                {
                    int energyDifference = GameManager.Instance.currentEnergy - GetComponent<CardDisplay>().cardData.energy;
                    if (cell.gridIndex.y < maxRow && gridManager.AddObjectToGrid(GetComponent<CardDisplay>().cardData.prefab, targetPos,GetComponent<CardDisplay>().cardData))
                    {
                        HandManager handManager = FindAnyObjectByType<HandManager>();
                        DiscardManager discardManager = FindObjectOfType<DiscardManager>();
                        discardManager.AddToDiscard(GetComponent<CardDisplay>().cardData);
                        Debug.Log("Before remove: ");
                        Debug.Log(gameObject);
                        handManager.cardsInHand.Remove(gameObject);
                        Debug.Log("After remove: ");
                        Debug.Log(gameObject);
                        handManager.UpdateHandVisuals();
                        Debug.Log("placed Character");
                        GameManager.Instance.currentEnergy = energyDifference;
                        Destroy(gameObject);
                    }
                }
            }
            TransitionToState0();
        }

        if (Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }

    private void updateCardPlayPostion()
    {
        if (cardPlayDivider != 0 && canvasRectTransform != null)
        {
            float segment = cardPlayMultiplier / cardPlayDivider;

            cardPlay.y = canvasRectTransform.rect.height * segment;
            // bruteforcing the cardplay.y to be 
            cardPlay.y = 150;
        }
    }

    private void updatePlayPosition()
    {
        if (canvasRectTransform != null && playPositionYDivider != 0 && playPositionXDivider != 0)
        {
            float segmentX = playPositionXMultiplier / playPositionXDivider;
            float segmentY = playPositionYMultiplier / playPositionYDivider;

            playPosition.x = canvasRectTransform.rect.width * segmentX;
            playPosition.y = canvasRectTransform.rect.height * segmentY;
        }
    }
}
