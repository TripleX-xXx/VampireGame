using UnityEngine;

public class Spikes : MonoBehaviour {

    public bool flagDebug = false;
    public float cellSize = 1;

    public void OnnStep()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position-Vector3.forward* (cellSize/2), transform.forward, out hit, cellSize))
        {
            if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.GetComponent<Person>().TakeDmg(10);
            }
        }
    }

    void Update()
    {
        if(flagDebug) Debug.DrawRay(transform.position - Vector3.forward * (cellSize / 2), transform.forward * cellSize, Color.green);
    }

}
