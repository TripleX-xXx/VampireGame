using UnityEngine;

public class Enemy : Person {

    //Enemy's turn to do action
    public void OnnStep()
    {
        GetComponent<Moving>().Move(MG_Sides.Side.up); // for debug
        // place for connecting IA
    }

    protected override void Attack()
    {
        RaycastHit hit;
        Vector3 site = MG_Sides.SideToVector3(
                        MG_Sides.EulerVectorToSide(transform.eulerAngles)
                        );

        if (Physics.Raycast(transform.position, site, out hit, 1f))
        {
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Person>().TakeDmg(30);
                Debug.LogError("Atak");
            }
        }
    }

}
