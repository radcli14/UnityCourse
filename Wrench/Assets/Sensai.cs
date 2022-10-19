using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensai : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Randomly set a start point
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        Vector3 randPosition = new Vector3(x, y, z);
        //Vector3 normPosition 
        transform.position = 4f * randPosition.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
