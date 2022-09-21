using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitSun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(1, 0, 0);
    }

    // Update is called once per frame
    float a = 0f;
    void Update()
    {
        a++;
        transform.position = new Vector3(Mathf.Cos(0.01f * a), 0, Mathf.Sin(0.01f * a));
    }
}
