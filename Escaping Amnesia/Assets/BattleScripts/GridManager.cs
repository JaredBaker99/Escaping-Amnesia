using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    public int width = 4;

    public int height = 2;

    public GameObject gridCellPrefab;

    public List<GameObject> gridObjects = new List<GameObject>();

    public GameObject[,] gridCells;

    public OnFieldDisplay onFieldDisplay;

    void Start()
    {
        CreateGrid();
        transform.localScale = new Vector3(1.25f,2.25f,1f);
    }

    void CreateGrid()
    {
        gridCells = new GameObject[width, height];
        Vector2 centerOffset = new Vector2(width/2.0f - 0.5f, height/2.0f - 0.5f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector2 gridPosition = new Vector2(x,y);
                Vector2 spawnPosition = gridPosition - centerOffset;

                GameObject gridCell = Instantiate(gridCellPrefab, spawnPosition, Quaternion.identity);
    
                gridCell.transform.SetParent(transform);
                

                gridCell.GetComponent<GridCell>().gridIndex = gridPosition;


                gridCells[x,y] = gridCell;
            }
        }
    }

    public bool AddObjectToGrid(GameObject obj, Vector2 gridPosition)
    {
        if (gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height)
        {
            GridCell cell = gridCells[(int)gridPosition.x, (int)gridPosition.y].GetComponent<GridCell>();

            if (cell.cellFull) return false;
            else
            {
                Debug.Log("Inside of AddObjectToGrid ");
                Debug.Log(obj);
                GameObject newObj = Instantiate(obj, cell.GetComponent<Transform>().position, Quaternion.identity);
                Debug.Log("afasfafa ");
                //newCard.GetComponent<OnFieldDisplay>().cardData = cardData;
                
                newObj.transform.SetParent(transform);
                gridObjects.Add(newObj);
                cell.objectInCell = newObj;
                cell.cellFull = true;
                return true;
            }  
        }
        else return false;
    }
}
