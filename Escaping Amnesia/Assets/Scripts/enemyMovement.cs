using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //NEW

public class enemyMovement : MonoBehaviour
{
    public GameObject player;
    public float speed ;
    public float tracking ;

    private float distance ;
    private bool found ;
   

    void Update()
        {
            distance = Vector2.Distance(transform.position, player.transform.position);
            Vector2 direction = player.transform.position - transform.position;

            if(found || distance < tracking) {
                if(!found) {
                    found = true ;
                }
                transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            }
            

        }

}