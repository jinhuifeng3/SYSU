using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPatrol : MonoBehaviour {

    public GameObject patrol;
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.name == "Patrol")
        {
            patrol = collider.gameObject;
        }
    }
}
