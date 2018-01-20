using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour {
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<Hero>().InitStep();
        }
        else
            other.GetComponent<Enemy>().chase = false;
    }
    */
    public bool flagDebug = false;
    public float cellSize = 1;

    public void OnnStep()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position - Vector3.forward * (cellSize / 2), transform.forward, out hit, cellSize))
        {
            if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.GetComponent<Person>().Stune(2,false);
            }
        }
    }

    void Update()
    {
        if (flagDebug) Debug.DrawRay(transform.position - Vector3.forward * (cellSize / 2), transform.forward * cellSize, Color.green);
    }
}

