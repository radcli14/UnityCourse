using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensai : MonoBehaviour
{
    // Materials
    public Material beforeTouch;
    public Material teeTouch;
    public Material handleTouch;

    // Rotation axis
    private float ex;
    private float ey;
    private float ez;

    // Interpolation settings
    private float waitTime = 0.25f;
    private Vector3 bigScale = new Vector3(0.25f, 0.25f, 0.25f);
    private Vector3 smallScale = new Vector3(0.025f, 0.025f, 0.025f);

    // Start is called before the first frame update
    void Start()
    {
        // Randomly set a start point
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        float z = Random.Range(-1f, 1f);
        Vector3 randPosition = new Vector3(x, y, z);
        transform.position = 1.5f * randPosition.normalized;
        transform.localScale = smallScale;

        // Initial setting for the vector used for the rotation axis
        ex = Random.Range(-1f, 1f);
        ey = Random.Range(-1f, 1f);
        ez = Random.Range(-1f, 1f);
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

    /**
    * Function that is triggered when this sensai collides with the wrench
    */
    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Tee")
        {
            GetComponent<Renderer>().material = teeTouch;
        }
        else
        {
            GetComponent<Renderer>().material = handleTouch;
        }
        StartCoroutine(ReturnToBeforeTouch());
    }

    /**
    * Coroutine to gradually shrink the sensai to its original size
    */
    IEnumerator ReturnToBeforeTouch()
    {
        float t = 0f;
        while (t < waitTime)
        {
            t += Time.deltaTime;
            transform.localScale = Vector3.Lerp(bigScale, smallScale, t);
            yield return null;
        }
        yield return new WaitForSeconds(waitTime);
        GetComponent<Renderer>().material = beforeTouch;
        transform.localScale = smallScale;
    }
}
