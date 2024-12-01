using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPlayerPosition : MonoBehaviour
{

    public GameObject player ;
    public Transform resetPosition ;

    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = resetPosition.position ;
    }

}
