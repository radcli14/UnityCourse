using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    GameObject[] attractors;
    Rigidbody rb;
    Vector3 lastForce = new Vector3(0, 0, 0);
    float G = 500f;
    public float initialSpeedX;
    public float initialSpeedY;
    public float initialSpeedZ;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigid body associated with this sphere
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(initialSpeedX, initialSpeedY, initialSpeedZ);

        // Get an array of all of the objects that were given the "Attracts" tag
        var foundAttractors = GameObject.FindGameObjectsWithTag("Attracts");
        
        // Eliminate this one from the list
        attractors = new GameObject[foundAttractors.Length - 1];
        int i, j;
        j = 0;
        for (i = 0; i < foundAttractors.Length; i++)
        {
            if (foundAttractors[i] != this.gameObject)
            {
                attractors[j] = foundAttractors[i];
                j++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.AddForce(-lastForce);

        Vector3 newForce = new Vector3(0, 0, 0);
        int i;
        for (i = 0; i < attractors.Length; i++)
        {
            Vector3 r = attractors[i].transform.position - transform.position;
            float rmag = r.magnitude;
            Rigidbody Rb = attractors[i].GetComponent<Rigidbody>();
            newForce += r * G * Rb.mass * rb.mass / rmag / rmag;
        }

        rb.AddForce(newForce);
        lastForce = newForce;
    }
}
