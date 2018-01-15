using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttacksList {

    public static bool debugFlag = false;
    public static float cellSide = 1f;

    // list of private method

    private static Vector3 getSideVector(Person person)
    {
        return MG_Sides.SideToVector3(
                        MG_Sides.EulerVectorToSide(person.GetComponent<Rigidbody>().rotation.eulerAngles)
                        );
    }
    
    // list of Attacks/Abilities

    public static float EnemyAttack1(Person person)
    {
        RaycastHit hit;
        Vector3 site = getSideVector(person);

        if (Physics.Raycast(person.transform.position, site, out hit, cellSide))
        {
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Person>().TakeDmg(5);
            }
        }
        if (debugFlag) Debug.Log("EnemyAttack1");
        return 0;
    }

    public static float Attack1(Person person) // simple attack
    {
        RaycastHit hit;
        Vector3 site = getSideVector(person);

        if (Physics.Raycast(person.transform.position, site, out hit, cellSide))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Person>().TakeDmg(40);
            }
        }
        if (debugFlag) Debug.Log("Attack1");
        return 0;
    }

    public static float Attack2(Person person) // Wave
    {
        float dmg = 15;
        Vector3 center = person.transform.position + getSideVector(person)* cellSide * 1.5f;
        Collider[] targets = Physics.OverlapBox(center, new Vector3(cellSide / 10 * 9, cellSide / 10 * 9, 0));
        foreach (Collider hit in targets)
        {
            if(hit.tag == "Enemy")
            {
                hit.GetComponent<Person>().TakeDmg(20);
                dmg -= 20;
            }
        }
        if (debugFlag) Debug.Log("Attack2 Dmg:"+dmg);
        return dmg;
    }

    public static float Attack3(Person person) // blink
    {
        RaycastHit hit;
        Vector3 site = getSideVector(person);
        int var = 6;
        if (Physics.Raycast(person.transform.position, site, out hit, cellSide*5))
        {
            Vector3 distans = hit.collider.transform.position - person.transform.position;

            if (System.Math.Abs(distans.x) > cellSide / 2)
            {
                var = (int)System.Math.Abs(distans.x / cellSide);
            }
            else if(System.Math.Abs(distans.y) > cellSide / 2)
            {
                var = (int)System.Math.Abs(distans.y / cellSide);
            }

        }
        person.GetComponent<Rigidbody>().MovePosition(person.transform.position + site * (var - 1) * cellSide);
        if (debugFlag) Debug.Log("Attack3");
        return 5;
    }

}
