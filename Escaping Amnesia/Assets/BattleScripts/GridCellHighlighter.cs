using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GridCellHighlighter : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public Color highlightColor = Color.yellow;
    public Color posColor = Color.green;
    public Color negColor = Color.red;
    private Color originalColor;

    public GridCell gridCell;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gridCell = GetComponent<GridCell>();
        originalColor = spriteRenderer.color;
    }

    void OnMouseEnter()
    {
        if (!GameManager.Instance.PlayingCard)
        {
            spriteRenderer.color = highlightColor;
        }
        else if (gridCell.cellFull || gridCell.gridIndex.x > 1)
        {
            spriteRenderer.color = negColor;
        }
        else spriteRenderer.color = posColor;
    }

    void OnMouseExit()
    {
        spriteRenderer.color = originalColor;
    }
}
