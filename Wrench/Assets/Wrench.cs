using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour
{

    // Initial angular rates
    public float initialRotX;
    public float initialRotY;
    public float initialRotZ;

    //private Rigidbody rb;

    // State variables
    Vector4 q;
    Vector3 u;

    // Inertia tensor components
    float Ixx;
    float Iyy;
    float Izz;

    // Start is called before the first frame update
    void Start()
    {
        // Get inertia tensor components, which Unity derives from the shapes
        Rigidbody rb = GetComponent<Rigidbody>();
        Ixx = rb.inertiaTensor.x;
        Iyy = rb.inertiaTensor.y;
        Izz = rb.inertiaTensor.z;

        // I had to set the isKinematic false, otherwise the inertias are
        // arbitrary, but after startup, want to write my own kinematics
        rb.isKinematic = true;

        q = new Vector4(0f, 0f, 0f, 1f);
        u = new Vector3(initialRotX, initialRotY, initialRotZ);
    }

    // Update is called once per frame
    void Update()
    {
        var dt = Time.deltaTime;

        // Call the Runge-Kutta method to get the updated states
        var (newQ, newU) = rk4(q, u, dt);
        transform.rotation = new Quaternion(newQ.x, newQ.y, newQ.z, newQ.w);

        // Carry the states for the subsequent update
        q = newQ;
        u = newU;
    }

    /**
     * Runga-Kutta Fourth Order
     */
    (Vector4, Vector3) rk4(Vector4 q, Vector3 u, float dt)
    {
        var (k1q, k1u) = f(q, u);
        var (k2q, k2u) = f(q + 0.5f * dt * k1q, u + 0.5f * dt * k1u);
        var (k3q, k3u) = f(q + 0.5f * dt * k2q, u + 0.5f * dt * k2u);
        var (k4q, k4u) = f(q + dt * k3q, u + dt * k3u);
        return (
            q + 1f / 6f * (k1q + 2f * k2q + 2f * k3q + k4q) * dt,
            u + 1f / 6f * (k1u + 2f * k2u + 2f * k3u + k4u) * dt
        );
    }

    /**
     * Equation of motion
     */
    (Vector4, Vector3) f(Vector4 q, Vector3 u)
    {
        // Get the derivative of the quaternion
        Vector4 dq = new Vector4(
            0.5f * (+u.x * q.w + 0.0f* q.z + u.z * q.y - u.y * q.z),
            0.5f * (+u.y * q.w - u.z * q.x + 0.0f* q.y + u.x * q.z),
            0.5f * (+u.z * q.w + u.y * q.x - u.x * q.y + 0.0f* q.z),
            0.5f * (0.0f * q.w - u.x * q.x - u.y * q.y - u.z * q.z)
        );

        // Calculate the derivatives of angular rates
        Vector3 dOmega = new Vector3(
            (Iyy * u.y * u.z - Izz * u.y * u.z) / Ixx,
            (-Ixx * u.x * u.z + Izz * u.x * u.z) / Iyy,
            (Ixx * u.x * u.y - Iyy * u.x * u.y) / Izz
        );

        return (dq, dOmega);
    }
}
