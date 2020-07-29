using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceControl : MonoBehaviour
{
    public float movingDistance = 5;
    public float moving_speed = 1;
    bool dir = true; //true -> ; false <-
    Vector3 orig_pos;
    void Start()
    {
        orig_pos = transform.position;
    }
    
    void FixedUpdate()
    {
        if (dir)
        {
            transform.position += new Vector3(moving_speed/20, 0 ,0);
        }
        else
        {
            transform.position += new Vector3(-1*moving_speed/20, 0, 0);
        }

        if (transform.position.x >= orig_pos.x + movingDistance)
        {
            dir = false;
        }
        else if(transform.position.x <= orig_pos.x - movingDistance)
        {
            dir = true;
        }
    }
}
