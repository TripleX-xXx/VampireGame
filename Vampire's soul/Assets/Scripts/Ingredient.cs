using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour {

    public bool flagDebug = false;
    public float cellSize = 1;

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position - Vector3.forward * (cellSize / 2), transform.forward, out hit, cellSize))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                if(hit.collider.GetComponent<Hero>().AddIngredient())
                {
                    Destroy(gameObject);
                }
            }
        }
        if (flagDebug) Debug.DrawRay(transform.position - Vector3.forward * (cellSize / 2), transform.forward * cellSize, Color.green);
    }

}
