using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensai : MonoBehaviour
{
    private bool isCoroutineExecuting = false;
    public Material material;
    public Material beforeTouch;
    public Material afterTouch;

    // Start is called before the first frame update
    void Start()
    {
        // Randomly set a start point
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        Vector3 randPosition = new Vector3(x, y, z);
        transform.position = 1.5f * randPosition.normalized;

        // TODO: want to have this be defined at start, but wasn't properly calling back
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Renderer>().material = afterTouch;
        StartCoroutine(ReturnToBeforeTouch());
    }

    IEnumerator ReturnToBeforeTouch()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material = beforeTouch;
    }
}
