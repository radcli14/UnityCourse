using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baller : MonoBehaviour
{
    public GameObject Sensai;
    List<GameObject> sensais = new List<GameObject>();
    int nSensais = 500;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < nSensais; i++)
        {
            GameObject sensai = Instantiate(Sensai);
            sensais.Add(sensai);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
