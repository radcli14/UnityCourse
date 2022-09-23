using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSun : MonoBehaviour
{
    public float initialSpeedX;
    public float initialSpeedY;
    public float initialSpeedZ;

    // Start is called before the first frame update
    float x;
    float y;
    float z;
    float dx;
    float dy;
    float dz;
    void Start()
    {
        // Initial states (position and velocity)
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
        dx = initialSpeedX;
        dy = initialSpeedY;
        dz = initialSpeedZ;
    }

    // Update is called once per frame
    float solarMass = 20f;  // TODO: Scale this so the orbits happen in some desired time on a 10x10-ish surface
    void Update()
    {
        float dt = Time.deltaTime;

        float[] u = new float[] { x, y, z, dx, dy, dz };
        //print(string.Join(", ", u));
        Vector3 q = new Vector3(x, y, z);
        Vector3 s = new Vector3(dx, dy, dz);
        Vector3[] du = f(q, s);

        /*float[] uNext = rk4(u, dt);
        print(string.Join(", ", uNext));
        */
        Vector3 dq = du[0];
        x += dt * dq.x;
        y += dt * dq.y;
        z += dt * dq.z;

        Vector3 ds = du[1];
        dx += dt * ds.x;
        dy += dt * ds.y;
        dz += dt * ds.z;

        /*x = uNext[0];
        y = uNext[1];
        z = uNext[2];
        dx = uNext[3];
        dy = uNext[4];
        dz = uNext[5];*/

        transform.position = new Vector3(x, y, z);
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
        //return new float[6] { s.x, s.y, s.z, ds.x, ds.y, ds.z };
        return new Vector3[] { s, ds };
    }
}
