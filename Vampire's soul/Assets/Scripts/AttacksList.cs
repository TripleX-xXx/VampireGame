using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttacksList {

    public static float EnemyAttack1(Person person)
    {
        RaycastHit hit;
        Vector3 site = MG_Sides.SideToVector3(
                        MG_Sides.EulerVectorToSide(person.transform.eulerAngles)
                        );

        if (Physics.Raycast(person.transform.position, site, out hit, 1f))
        {
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Person>().TakeDmg(10);
            }
        }
        Debug.Log("EnemyAttack1");
        return 0;
    }

    public static float Attack1(Person person)
    {
        RaycastHit hit;
        Vector3 site = MG_Sides.SideToVector3(
                        MG_Sides.EulerVectorToSide(person.transform.eulerAngles)
                        );

        if (Physics.Raycast(person.transform.position, site, out hit, 1f))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Person>().TakeDmg(30);
                Debug.LogError("Atak");
            }
        }
        Debug.Log("Attack1");
        return 0;
    }

    public static float Attack2(Person person)
    {
        Debug.Log("Attack2");
        return 0;
    }

    public static float Attack3(Person person)
    {
        Debug.Log("Attack3");
        return 0;
    }

}
