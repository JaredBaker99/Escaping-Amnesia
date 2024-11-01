using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Vector2 gridIndex;

    public bool cellFull = false;

    public GameObject objectInCell;

    // to get to the player's energy
    //public GameManger gameManger;

    public void SacraficeOnClick()
    {
        //call the sacrafice ui box returns yes or no

        // if no do nothing

        // if yes set cell full = false and object incell to null / destroy gameobject
    }
}
