using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {
    
    [SerializeField] public Transform platform;
    [SerializeField] public Transform start;
    [SerializeField] public Transform end;

    public Vector3 newPosition;
    private bool state;
    public float smooth;
    public float resetTime;
    

	// Use this for initialization
	void Start () {
        state = false;
        Reverse();
	}
	
	// Update is called once per frame
	void Update () {
        platform.position = Vector3.Lerp(platform.position, newPosition, smooth * Time.deltaTime);
	}

    void Reverse()
    {
        newPosition = (state) ? end.position : start.position;
        state = !state;

        Invoke("Reverse", resetTime);
    }
}
