using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
Transform wrightTransform;

    // Start is called before the first frame update
    void Start()
    {
        wrightTransform = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, 0.1f));
        float roll = wrightTransform.eulerAngles.z;
        transform.Rotate(new Vector3(0f, -3.14f * Mathf.Sin(0.01745329251f * roll), 0f));
    }
}
