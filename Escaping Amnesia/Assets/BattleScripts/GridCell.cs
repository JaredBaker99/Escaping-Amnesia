using UnityEngine;

public class GridCell : MonoBehaviour
{
    public Vector2 gridIndex;

    public bool cellFull = false;

    public GameObject objectInCell;

    // to get to the player's energy
    //public GameManger gameManger;

    // public void SacraficeOnClick()
    // {
        //SacraficeDialogUI.Instance.ShowQuestion("Are you sure you want to Sacrafice the card?", () => { 
        // destroy objectincell && +2 energy
        //}, () => {
        // do nothing
        // });
    // }

    
}
