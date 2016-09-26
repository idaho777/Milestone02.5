using UnityEngine;
using System.Collections;

public class SpinningPlatform : MonoBehaviour {

    public float spinX;
    public float spinY;
    public float spinZ;
    	
	// Update is called once per frame
	void Update () {
        transform.Rotate(spinX, spinY, spinZ);
	}
}
