using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToBattleArea  :  MonoBehaviour

{
    public string sceneName ;
    public bool toBattleArea ;
    public GameObject player ; 

    public void Start() {

    }

    public void setToBattle(bool set) {
        toBattleArea = set ;
    }

}
