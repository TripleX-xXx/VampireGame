using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Keep rotation an object
 */
public class Rotate : MonoBehaviour {

    //Will rotate this value each frame
    public Vector3 rotation;

	
	void Update () {
        //Rotate the object
        transform.Rotate(rotation);
	}
}
