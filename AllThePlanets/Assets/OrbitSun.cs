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

        // Get derivatives of states
        Vector3[] du = f(pos, vel);

        // Update position
        Vector3 dq = du[0];
        pos += dq * dt;

        // Update velocity
        Vector3 ds = du[1];
        vel += ds * dt;

        // Update transform
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

    /**
     * Runga-Kutta Fourth Order
     */
    /*float[] rk4(float[] u, float dt)
    {
        float[] k1 = f(u);
        float[] k2 = f(k1);
        float[] k3 = f(k2);
        float[] k4 = f(k3);
        return new float[6] {
            u[0] + 1f/6f * (k1[0] + 2f*k2[0] + 2f*k3[0] + k4[0]) * dt,
            u[1] + 1f/6f * (k1[1] + 2f*k2[1] + 2f*k3[1] + k4[1]) * dt,
            u[2] + 1f/6f * (k1[2] + 2f*k2[2] + 2f*k3[2] + k4[2]) * dt,
            u[3] + 1f/6f * (k1[3] + 2f*k2[3] + 2f*k3[3] + k4[3]) * dt,
            u[4] + 1f/6f * (k1[4] + 2f*k2[4] + 2f*k3[4] + k4[4]) * dt,
            u[5] + 1f/6f * (k1[5] + 2f*k2[5] + 2f*k3[5] + k4[5]) * dt,
        };
    }*/

    /**
     * TODO: function to add u + dt * f(u)
     */

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
