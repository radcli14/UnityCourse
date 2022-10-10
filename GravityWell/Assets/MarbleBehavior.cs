using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarbleBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, 2.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
