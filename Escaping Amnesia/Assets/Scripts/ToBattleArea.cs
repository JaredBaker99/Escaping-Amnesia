using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBattleArea  :  MonoBehaviour

{
    public string sceneName ;
    public bool toBattleArea ;
    public Vector3 playerPosition ;
    public string enemyName ;

    public void setToBattle(bool set) {
        toBattleArea = set ;
    }

    public void setPlayerPosition(Vector3  pos) {
        playerPosition = pos ;
    }

}
