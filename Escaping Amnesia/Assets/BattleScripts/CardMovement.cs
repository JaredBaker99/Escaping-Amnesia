using UnityEngine;
using UnityEngine.EventSystems;

public class CardMovement : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    // gets rectTransform of our current object
    private RectTransform rectTransform;

    // gets basic canvas
    private Canvas canvas;

    // stores pointer position mouse pointer
    private Vector2 originalLocalPointerPosition;

    // stores original location of card
    private Vector3 originalPanelLocalPosition;
    
    private Vector3 originalScale;

    private int currentState = 0;
    // storing rotation
    private Quaternion originalRotation;

    // storing original position
    private Vector3 originalPosition;

    private GridManager gridManager;

    [SerializeField] private float selectScale = 1.1f;

    // the position where if our mouse goes pass it, it will push our card into the play position
    [SerializeField] private Vector2 cardPlay;

    // stores our play positoin where our card will jump to
    [SerializeField] private Vector3 playPosition;

    // stores glow effect sprite
    [SerializeField] private GameObject glowEffect;

    [SerializeField] private GameObject playArrow;

    void Awake(){
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        originalScale = rectTransform.localScale;
        originalPosition = rectTransform.localPosition;
        originalRotation = rectTransform.localRotation;
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update() 
    {
        switch (currentState)
        {
            case 1:
                HandleHoverState();
                break;
            case 2:
                HandleDragState();
                //check if mouse button is release
                if(!Input.GetMouseButton(0))
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
        //reset scale
        rectTransform.localScale = originalScale; 
        //reset rotation
        rectTransform.localRotation = originalRotation;
        //reset position
        rectTransform.localPosition = originalPosition;
        //disables glow effect
        glowEffect.SetActive(false);
        //disables playArrow
        playArrow.SetActive(false); 
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

    public void OnPointerExit(PointerEventData eventdata)
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
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out originalLocalPointerPosition);
            originalPanelLocalPosition = rectTransform.localPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentState == 2)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                rectTransform.position = Input.mousePosition;

                if (rectTransform.localPosition.y > cardPlay.y)
                {
                    currentState = 3;
                    playArrow.SetActive(true);
                    rectTransform.localPosition = playPosition;
                }
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
        //set card's rotations to 0
        rectTransform.localRotation = Quaternion.identity;
    }

    private void HandlePlayState()
    {
        rectTransform.localPosition = playPosition;
        rectTransform.localRotation = Quaternion.identity;

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.GetComponent<GridCell>())
            {
                GridCell cell = hit.collider.GetComponent<GridCell>();
                Vector2 targetPos = cell.gridIndex;
                if (gridManager.AddObjectToGrid(GetComponent<CardDisplay>().cardData.prefab, targetPos))
                {
                    HandManager handManager = FindAnyObjectByType<HandManager>();
                    handManager.cardsInHand.Remove(gameObject);
                    handManager.UpdateHandVisuals();
                    Debug.Log("placed Character");
                    Destroy(gameObject);
                }
            }
            TransitionToState0();
        }

        if(Input.mousePosition.y < cardPlay.y)
        {
            currentState = 2;
            playArrow.SetActive(false);
        }
    }
}
