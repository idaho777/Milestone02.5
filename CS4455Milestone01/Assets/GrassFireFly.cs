using UnityEngine;
using System.Collections;

public class GrassFireFly : MonoBehaviour {


    public ParticleSystem particles;
    private bool isIn;

    void Start()
    {
        particles.Stop();
        isIn = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            particles.transform.position = col.transform.position;
            particles.transform.SetParent(col.transform);
            particles.Play();
            print("here");
        }
    }

    //void OnTriggerStay(Collider col)
    //{
    //    print("Stay");
    //}

    void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            particles.transform.SetParent(null);
            particles.Stop();
            print("exit");
        }
    }
}
