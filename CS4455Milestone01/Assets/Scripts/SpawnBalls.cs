using UnityEngine;
using System.Collections;

public class SpawnBalls : MonoBehaviour {
    public GameObject[] balls;
    public bool cont;
    private float time;

	// Use this for initialization
	void Start () {
        if (!cont)
        {
            for (float i = 5; i < 20; i += 2)
            {
                for (float j = -5; j < 5; j += 1.0f)
                {
                    for (float k = -5; k < 5; k += 1.0f)
                    {
                        Instantiate(balls[Random.Range(0, balls.Length)], transform.position + new Vector3(j, i, k), Quaternion.identity, transform);
                    }
                }

            }
        }

    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (cont && time > 0.5) {
            Instantiate(balls[Random.Range(0, balls.Length)], transform.position + new Vector3(0, 0, 0), Quaternion.identity, transform);
            time = 0;
        }
    }
}
