using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{
    public float initialRotX;
    public float initialRotY;
    public float initialRotZ;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial angular velocity from the user settings
        rb = GetComponent<Rigidbody>();
        rb.angularVelocity = new Vector3(initialRotX, initialRotY, initialRotZ);
        rb.maxAngularVelocity = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;

        // Get inertia tensor components
        /*var Ixx = rb.inertiaTensor.x;
        var Iyy = rb.inertiaTensor.y;
        var Izz = rb.inertiaTensor.z;

        // Get angular velocity components in each axis
        var wx = rb.angularVelocity.x;
        var wy = rb.angularVelocity.y;
        var wz = rb.angularVelocity.z;

        // Update the angular rates
        var rightHandSide = new Vector3(
            (Iyy*wy*wz - Izz*wy*wz)/Ixx,
            (-Ixx*wx*wz + Izz*wx*wz)/Iyy,
            (Ixx*wx*wy - Iyy*wx*wy)/Izz
        );
        rb.angularVelocity += rightHandSide * dt;*/
        Vector3 u = rb.angularVelocity;
        //Vector3 u = transform.InverseTransformDirection(rb.angularVelocity);
        rb.angularVelocity = rk4(u, dt);
    }


    /**
     * Runga-Kutta Fourth Order
     */
    Vector3 rk4(Vector3 u, float dt)
    {
        Vector3 k1 = f(u);
        Vector3 k2 = f(u + 0.5f * dt * k1);
        Vector3 k3 = f(u + 0.5f * dt * k2);
        Vector3 k4 = f(u + dt * k3);
        return u + 1f / 6f * (k1 + 2f * k2 + 2f * k3 + k4) * dt;
    }

    /**
     * Equation of motion
     */
    Vector3 f(Vector3 u)
    {
        //var dt = Time.deltaTime;

        // Get inertia tensor components
        float Ixx = rb.inertiaTensor.x;
        float Iyy = rb.inertiaTensor.y;
        float Izz = rb.inertiaTensor.z;

        // Get angular velocity components in each axis
        float wx = u.x;
        float wy = u.y;
        float wz = u.z;

        // Return the derivatives of angular rates
        return new Vector3(
            (Iyy * wy * wz - Izz * wy * wz) / Ixx,
            (-Ixx * wx * wz + Izz * wx * wz) / Iyy,
            (Ixx * wx * wy - Iyy * wx * wy) / Izz
        );
        //rb.angularVelocity += rightHandSide * dt;
    }
}
