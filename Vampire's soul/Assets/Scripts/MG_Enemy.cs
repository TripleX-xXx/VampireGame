using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Enemy : MG_Person
{

    //public Image health_bar;
    public MG_Hero hero;
    private Moving m;


    //private float maxHP = 100;
    public float hp = 100;

    //How far can find the player
    public int distanceToSeePlayer = 5;
    //How close it need to be to attack
    public int distanceToAttack = 1;

    //Position of enemy
    public IntVector2 e_posit;


    private void Start()
    {
        m = scriptHolding.GetComponent<Moving>();
        enemyInstance = this;
        //Give initial position
        e_posit = Position();
        //  StartCoroutine(MovementTest());
    }
    //Keep moving the NPC, for debugging
    public IEnumerator MovementTest()
    {
        m.Move(this, Moving.Site.up);
        yield return new WaitForSeconds(1f);
        StartCoroutine(MovementTest());
    }
    //bool flagMove = false;

    public override bool Move()
    {
        //Set();
        //m.SetPlayer(this);
        //m.SetUp();
        if (m.Move(this, Moving.Site.left)) ;
        else if (m.Move(this, Moving.Site.right)) ;
        else if (m.Move(this, Moving.Site.up)) ;
        else if (m.Move(this, Moving.Site.down)) ;
        return true;
    }
    public void AttackE(MG_Hero P, float dmg)
    {
        P.hp = P.hp - dmg;
        if (P.hp <= 0)
        {
            Die(P);
        }
    }


    //Enemy's turn to do action
    public void OnnStep()
    {
        OnStep();
        //Update position 
        e_posit = Position();
        //For each action it have, do a action
        if (CanSeePlayer())
        {
            //If close to player
            if (distanceToAttack == DistanceFromObject(hero))
            {
                AttackE(hero, 1); 
                //Attack();
            } else
                //if you see but far away
                m.Move(this, DirectionToObj(hero));
        }

        

    }
    //Can it see the player?
    public bool CanSeePlayer()
    {
        Debug.Log(DistanceFromObject(hero));
        return DistanceFromObject(hero) <= distanceToSeePlayer;
    }


    //Choose side where to go to get closer to enemy
    public Moving.Site DirectionToObj(MG_Hero obj)
    {

        if (obj == null)
            return Moving.Site.none;

        IntVector2 position = Position();
        IntVector2 dir = position - obj.posit;


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

    //Calculate position on grid between 2 objectss
    public int DistanceFromObject(MG_Person obj)
    {

        IntVector2 position = Position();
        IntVector2 playerPos = obj.Position();
      //  Debug.Log(position);
       // Debug.Log(playerPos);
        return Mathf.CeilToInt(Vector3.Distance(new Vector3(position.x, position.z), new Vector2(playerPos.x, playerPos.z)));
    }

    public void TakeDmg(float dmg)
    {
        hp -= dmg;
        if (this.hp <= 0)
        {
            Die(this);
        }
        //    health_bar.fillAmount = (float)hp / maxHP;
    }

    protected override void Attack()
    {
        RaycastHit hit;
        Vector3 site = Vector3.up;
        Vector3 q = GetComponent<Transform>().eulerAngles;
        //You watch in front 
        if (q.z > -0.1 && q.z < 0.1) site = Vector3.up;
        else if (q.z > 89 && q.z < 91) site = Vector3.left;
        else if ((q.z > -91 && q.z < -89) || (q.z > 269 && q.z < 271)) site = Vector3.right;
        else if (q.z > 179 && q.z < 181) site = Vector3.down;
        if (Physics.Raycast(transform.position, site, out hit, 1f))
        {
            if (hit.collider.tag == "Player") hit.collider.gameObject.GetComponent<MG_Hero>().TakeDmg(10);
        }
    }
}
