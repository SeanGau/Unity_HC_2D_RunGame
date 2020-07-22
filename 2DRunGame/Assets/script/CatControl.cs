using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform trans;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        trans.position += new Vector3(0.1f, 0, 0);
    }
}
