using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCounter : MonoBehaviour
{
    public int counter;
    public bool quadFirst = false;
    public bool quadSecond = false;
    public int lastBattleRoom  = -1 ;
    public int[] battleRooms = new int[12];
    public bool puzzleSolved = false ;
    public bool backwards = false ;

    void Start() {
        for(int i = 0 ; i < 12 ; i++) {
            battleRooms[i] = 0 ;
        }
    }

    public void reset() {
        counter = 0 ;
        for(int i = 0 ; i < 12 ; i++) {
            battleRooms[i] = 0 ;
        }
        lastBattleRoom = -1 ;
        backwards = false ;
    }
}
