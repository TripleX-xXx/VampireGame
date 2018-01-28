using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class FollowObject : MonoBehaviour {

    public GameObject follow;
    public Vector3 camPosition;
    private Rigidbody r;
    private Vector3 p;


    void Awake() {
        r = GetComponent<Rigidbody>();
        r.useGravity = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (follow != null)
        {
            p = follow.transform.position;
            r.MovePosition(new Vector3(p.x + camPosition.x, p.y + camPosition.y, camPosition.z));
        }
    }
}
