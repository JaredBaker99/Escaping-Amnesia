using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public bool[] isAlive ;
    public bool[] bossIsAlive ;
    public bool trueBoss ;

    public int enemiesSpawned = 0 ;
    public int enemiesKilled = 0 ;

    void Start() {
        isAlive = new bool[3] ;
        bossIsAlive = new bool[40] ;
    }

    public void setAlive(string enemyName) {
        int enemyNum ;
        int.TryParse(enemyName,  out enemyNum) ;
        enemyNum -= 8 ; //  adjust the number to match your enemy names
        isAlive[enemyNum] = true ;
    }

    public void setAllAlive() {
        for (int i = 0; i < isAlive.Length; i++) {
            isAlive[i] = true ;
        }
        for(int i = 0; i < bossIsAlive.Length; i++) {
            bossIsAlive[i] = true ;
        }
        trueBoss = true ;
    }

    public void setDead(string enemyName) {
        int enemyNum ;
        int.TryParse(enemyName,  out enemyNum) ;
        enemyNum -= 8 ; //  adjust the number to match your enemy names
        enemiesKilled++ ;
        isAlive[enemyNum] = false ;
    }

    public bool getIsAlive(string enemyName) {
        int enemyNum ;
        int.TryParse(enemyName,  out enemyNum) ;
        enemyNum -= 8 ;
        return isAlive[enemyNum] ;
    }

    public bool getBossIsAlive(string bossName) {
        int bossNum ;
        int.TryParse(bossName, out bossNum);
        return bossIsAlive[bossNum] ;
    }

    public void setBossAlive(string bossName) {
        int bossNum ;
        int.TryParse(bossName, out bossNum);
        bossIsAlive[bossNum] = true ;
    }

    public void setBossDead(string bossName) {
        int bossNum ;
        int.TryParse(bossName, out bossNum);
        bossIsAlive[bossNum] = false ;
    }
}
