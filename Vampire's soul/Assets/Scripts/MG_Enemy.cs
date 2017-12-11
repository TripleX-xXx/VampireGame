using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Enemy : MG_Person
{

    //public Image health_bar;
    public MG_Hero hero;
    private Moving m;
    public BaseObj basse;
    //private int maxHP = 100;
    //public int hp = 100;

    //   Vector3 pos = GameObject.GetComponent<Transform>().position;
    //How far can find the player
    public int distanceToSeePlayer = 5;
    //How close it need to be to attack
    public int distanceToAttack = 1;

    void Reset()
    {
        basse.ObjType = BaseObj.Type.enemy;
    }

    private void Start()
    {
        m = scriptHolding.GetComponent<Moving>();
        //  StartCoroutine(MovementTest());
    }
    //Keep moving the NPC, for debugging
    public IEnumerator MovementTest()
    {
        m.Move(this, Moving.Site.up);
        yield return new WaitForSeconds(1f);
        StartCoroutine(MovementTest());
    }

    public override bool Move()
    {
        if (m.Move(this, Moving.Site.up)) ;
        else if (m.Move(this, Moving.Site.right)) ;
        else if (m.Move(this, Moving.Site.left)) ;
        else if (m.Move(this, Moving.Site.down)) ;
        return true;
    }

    public void Movement()
    {
        m.Move(this, Moving.Site.up);
    }

    public void OnnStep()
    {

        OnStep();

        //For each action it have, do a action

        //Movement();
        Move();

    }
    //Can it see the player?
    public bool CanSeePlayer()
    {
        Debug.Log(DistanceFromObject(hero));
        return DistanceFromObject(hero) <= distanceToSeePlayer /*&& !WallInPathToPlayer()*/;
    }

    public Moving.Site DirectionToObj(MG_Hero obj)
    {

        if (obj == null)
            return Moving.Site.none;

        IntVector2 position = Position();
        IntVector2 dir = position - MG_Hero.playerInstance.Position();


        //There will be two options to move, if can't go to opt1, will go to opt2
        Moving.Site opt1;
        Moving.Site opt2;

        //If the direction X is closer than Z, choose left or right for the first option, and up or down for the second option
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            if (dir.x < 0)
                opt1 = Moving.Site.right;
            else
                opt1 = Moving.Site.left;

            if (dir.z < 0)
                opt2 = Moving.Site.up;
            else
                opt2 = Moving.Site.down;
        }
        //Else, do the oposite
        else
        {
            if (dir.z < 0)
                opt1 = Moving.Site.up;
            else
                opt1 = Moving.Site.down;

            if (dir.x < 0)
                opt2 = Moving.Site.right;
            else
                opt2 = Moving.Site.left;
        }
        //If there is a obstacle for opt1, go to opt2.
        //If there is a obstacle of opt2, the Move Func will not let him move, so he will stay in the same place;
        //  if (!ObjectManager.CheckObstacle(position + Moving.SideToVector(opt1)))
        return opt1;
        //  else
        //      return opt2;

    }

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
        health_bar.fillAmount = (float)hp / maxHp;
    }

    protected override void Attack()
    {
        RaycastHit hit;
        Vector3 site = Vector3.up;
        Vector3 q = GetComponent<Transform>().eulerAngles;
        if (q.z > -0.1 && q.z < 0.1) site = Vector3.up;
        else if (q.z > 89 && q.z < 91) site = Vector3.left;
        else if ((q.z > -91 && q.z < -89) || (q.z > 269 && q.z < 271)) site = Vector3.right;
        else if (q.z > 179 && q.z < 181) site = Vector3.down;
        if (Physics.Raycast(transform.position, site, out hit, 1f)) {
            if (hit.collider.tag == "Player") hit.collider.gameObject.GetComponent<MG_Hero>().TakeDmg(10);
        }
    }

}
