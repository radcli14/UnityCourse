using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    // Transform for the Wright flyer child
    Transform wrightTransform;

    // Scales the yaw rate as a function of the roll angle
    float yawScale = -3.14f;

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
        // Update the yaw angle as a function of the roll
        float roll = wrightTransform.eulerAngles.z;
        float rollSine = Mathf.Sin(0.01745329251f * roll);
        transform.Rotate(yawScale * rollSine * Vector3.up);

        // Move the vehicle forward
        transform.Translate(0.1f * Vector3.forward);
    }
}
