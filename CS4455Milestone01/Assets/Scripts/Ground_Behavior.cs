using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ground_Behavior : MonoBehaviour {

    public List<GroundType> grounds = new List<GroundType>();
    public X_Bot_Controller controller;
    public string currentGroundType;

	// Use this for initialization
	void Start () {
        setGroundType(grounds[0]);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "dirt_floor")
            setGroundType(grounds[0]);
        else if (hit.transform.tag == "hard_floor")
            setGroundType(grounds[1]);
        else if (hit.transform.tag == "sand_floor")
            setGroundType(grounds[2]);
        else if (hit.transform.tag == "metal_floor")
            setGroundType(grounds[3]);
        else if (hit.transform.tag == "wood_floor")
            setGroundType(grounds[4]);
        else
            setGroundType(grounds[1]);
    }

    public void setGroundType(GroundType ground)
    {
        if (currentGroundType != ground.name)
        {
            controller.footstepSounds = ground.footstepSounds;
            controller.footStepParticles = ground.particles;
            currentGroundType = ground.name;
        }
    }
}

[System.Serializable]
public class GroundType
{
    public string name;

    public AudioClip[] footstepSounds;
    public ParticleSystem particles;
}