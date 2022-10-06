using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject marble;
    List<MarbleBehavior> marbles;

    // Start is called before the first frame update
    void Start()
    {
        var foundMarbles = FindObjectsOfType<MarbleBehavior>();
        marbles = new List<MarbleBehavior>(foundMarbles);
    }

    // Update is called once per frame
    void Update()
    {

    }

    float elapsed = 5f;
    private void FixedUpdate()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= 5f)
        {
            // Spawn a new marble
            var newMarble = Instantiate(marble);
            var behavior = newMarble.gameObject.GetComponent<MarbleBehavior>();
            marbles.Add(behavior);
            //marbles.Add(newMarble);

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
