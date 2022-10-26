using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    // Transform for the Wright flyer child
    Transform wrightTransform;

    // Scales the yaw rate as a function of the roll angle
    float yawScale = -3.14f;

    // Setting for how far the plane has rolled, 0 is hard left, 1 hard right
    float rollNormTime = 0f;
    float rollBlend = 0.25f;
    float rollTarget = 0.5f;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        wrightTransform = transform.GetChild(0);

        animator = GetComponent<Animator>();
        animator.SetFloat("Blend", rollBlend);
    }

    // Update is called once per frame
    float dt = 0f;
    float randomizeAtTime = 1.333f;
    void Update()
    {
        // If sufficient time has passed, update the intended roll angle
        dt += Time.deltaTime;
        if (dt >= randomizeAtTime) 
        {
            dt = 0f;
            rollTarget = Random.Range(0f, 1f);
        }
    }

    void FixedUpdate()
    {
        // Update the blended roll angle in the animator
        rollBlend += 0.1f * (rollTarget - rollBlend);
        rollBlend = Mathf.Clamp(rollBlend, 0f, 1f);
        animator.SetFloat("Blend", rollBlend);

        // Update the yaw angle as a function of the roll
        float roll = wrightTransform.eulerAngles.z;
        float rollSine = Mathf.Sin(0.01745329251f * roll);
        transform.Rotate(yawScale * rollSine * Vector3.up);

        // Move the vehicle forward
        transform.Translate(0.1f * Vector3.forward);
    }
}
