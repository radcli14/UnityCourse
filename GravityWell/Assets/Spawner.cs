using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject marble;
    List<GameObject> marbles = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the first marble
        var newMarble = Instantiate(marble);
        marbles.Add(newMarble);
    }

    // Update is called once per frame
    void Update()
    {

    }

    float respawnTime = 3.14159f / 2f;
    float elapsed = 0f;
    private void FixedUpdate()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= respawnTime)
        {
            // Spawn a new marble
            var newMarble = Instantiate(marble);
            marbles.Add(newMarble);

            // Reset the clock
            elapsed = 0f;
        }

        // Check if marbles have fallen, if they have, DESTROY!
        marbles.ForEach(marble =>
            {
                if (marble.transform.position.y < 0)
                {
                    Destroy(marble);
                    marbles.Remove(marble);
                }
            }
        );
    }
}
