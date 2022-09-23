using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSun : MonoBehaviour
{
    public float initialSpeedX;
    public float initialSpeedY;
    public float initialSpeedZ;

    // Start is called before the first frame update
    Vector3 pos;
    Vector3 vel;
    void Start()
    {
        // Initial states (position and velocity)
        pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        vel = new Vector3(initialSpeedX, initialSpeedY, initialSpeedZ);
    }

    // Update is called once per frame
    float solarMass = 20f;  // TODO: Scale this so the orbits happen in some desired time on a 10x10-ish surface
    void Update()
    {
        float dt = Time.deltaTime;

        // Call Runga-Kutta Fourth Order alogorithm
        Vector3[] newStates = rk4(pos, vel, dt);
        pos = newStates[0];
        vel = newStates[1];

        // Update transform
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    /**
     * Runga-Kutta Fourth Order
     */
    Vector3[] rk4(Vector3 q, Vector3 s, float dt)
    {
        Vector3[] k1 = f(q, s);
        Vector3[] k2 = f(q + 0.5f*dt*k1[0], s + 0.5f*dt*k1[1]);
        Vector3[] k3 = f(q + 0.5f*dt*k2[0], s + 0.5f*dt*k2[1]);
        Vector3[] k4 = f(q + dt*k3[0], s + dt*k3[1]);
        return new Vector3[] {
            q + 1f/6f * (k1[0] + 2f*k2[0] + 2f*k3[0] + k4[0]) * dt,
            s + 1f/6f * (k1[1] + 2f*k2[1] + 2f*k3[1] + k4[1]) * dt
        };
    }

    /**
     * Equation of motion
     */
    Vector3[] f(Vector3 q, Vector3 s)
    {
        // Radius away from the sun
        float r = q.magnitude;

        // Accelerations in each direction
        Vector3 ds = -q.normalized * solarMass / r / r;

        // Derivatives of state vector
        return new Vector3[] { s, ds };
    }
}
