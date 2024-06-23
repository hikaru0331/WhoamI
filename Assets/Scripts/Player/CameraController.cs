using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public Transform goal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < player.position.x)
        {
            if (goal.position.x > player.position.x)
            {
                transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
            }
        }
    }
}
