using UnityEngine;

public class Spikes : MonoBehaviour {

    public bool flagDebug = false;
    public float cellSize = 1;
    public Animator animator;

    public void OnnStep()
    {
        RaycastHit hit;
        if(Physics.BoxCast(transform.position+Vector3.forward*(cellSize/2), new Vector3(cellSize / 2, cellSize / 2, 0), -Vector3.forward, out hit, Quaternion.identity, cellSize))
        //if (Physics.Raycast(transform.position-Vector3.forward* (cellSize/2), transform.forward, out hit, cellSize))
        {
            if (hit.collider.gameObject.tag == "Player" || hit.collider.gameObject.tag == "Enemy")
            {
                animator.SetTrigger("Attack");
                hit.collider.GetComponent<Person>().TakeDmg(20);
            }
        }
    }

    void Update()
    {
        if(flagDebug) Debug.DrawRay(transform.position - Vector3.forward * (cellSize / 2), transform.forward * cellSize, Color.green);
    }

}
