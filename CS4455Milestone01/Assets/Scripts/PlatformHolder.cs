using UnityEngine;
using System.Collections;

public class PlatformHolder : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            col.transform.parent = this.transform;
            col.transform.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.transform.tag == "Player")
        {
            col.transform.parent = null;
            col.transform.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
