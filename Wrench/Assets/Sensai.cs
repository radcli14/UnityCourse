using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensai : MonoBehaviour
{
    private bool isCoroutineExecuting = false;
    public Material material;
    public Material beforeTouch;
    public Material afterTouch;
    public Material handleTouch;

    private float ex = Random.Range(-1f, 1f);
    private float ey = Random.Range(-1f, 1f);
    private float ez = Random.Range(-1f, 1f);

    // Start is called before the first frame update
    void Start()
    {
        // Randomly set a start point
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        Vector3 randPosition = new Vector3(x, y, z);
        transform.position = 1.5f * randPosition.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        // Randomly set a rotation axis
        ex += Random.Range(-0.1f, 0.1f);
        ey += Random.Range(-0.1f, 0.1f);
        ez += Random.Range(-0.1f, 0.1f);
        Vector3 randAxis = new Vector3(ex, ey, ez);
        transform.RotateAround(new Vector3(0f, 0f, 0f), randAxis, 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Tee")
        {
            GetComponent<Renderer>().material = afterTouch;
        }
        else
        {
            GetComponent<Renderer>().material = handleTouch;
        }
        transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        StartCoroutine(ReturnToBeforeTouch());
    }

    IEnumerator ReturnToBeforeTouch()
    {
        yield return new WaitForSeconds(0.5f);
        GetComponent<Renderer>().material = beforeTouch;
        transform.localScale = new Vector3(0.025f, 0.025f, 0.025f);
    }
}
