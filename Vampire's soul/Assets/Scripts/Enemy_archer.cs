using System;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_archer : Person {

    public bool flagDebug = false;
    public Hero hero;
    public Image health_bar; // graphic health indicator
    public GameObject enemyObject;
    public Transform drop;
    public Animator anim;

    //How far can find the player
    public int distanceToSeePlayer = 5;
    //How close it need to be to attack
    public int distanceToAttack = 3;

    public bool chase = false;

    private void Awake()
    {
        this.tag = "Enemy";
        hero = UnityEngine.Object.FindObjectOfType<Hero>();
    }

    //Enemy's turn to do action
    public void OnnStep()
    {
        CheckForPlayer();
        if (stun > 0) stun--;   
        Chase();

    }



    private void Chase()
    {
        if (chase)
        {
            if (distanceToAttack >= DistanceFromObject(hero))
            {
                RotateToPlayer();
                anim.SetTrigger("IdleToShootBow");
                AttacksList.EnemyAttack1(this);
            }
            else
            {
                if (stun == 0)
                GetComponent<Moving>().Move(DirectionToObj(hero));
            }
        }
    }
	
	public void CheckForPlayer(){
		
		RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, distanceToSeePlayer))
            if (hit.collider.gameObject.tag == "Player")
                chase = true;
        if (Physics.Raycast(transform.position, (transform.up + transform.right).normalized, out hit, distanceToSeePlayer))
            if (hit.collider.gameObject.tag == "Player")
                chase = true;
        if (Physics.Raycast(transform.position, (transform.up - transform.right).normalized, out hit, distanceToSeePlayer))
            if (hit.collider.gameObject.tag == "Player")
                chase = true;
			
	}

	// Autorotate to player position
	public void RotateToPlayer()
    {
        Vector3 dir = transform.position - hero.transform.position;
        float angle = Mathf.Round(Vector3.Angle(dir, transform.up));

        if (angle == 90f)
        {
            Quaternion rotation = Quaternion.LookRotation(transform.position - hero.transform.position, transform.TransformDirection(Vector3.forward));
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
    }

    //Calculate position on grid between 2 objectss
    public int DistanceFromObject(Person obj)
    {

        IntVector2 position = Position();
        IntVector2 playerPos = obj.Position();

        return Mathf.CeilToInt(Vector3.Distance(new Vector3(position.x, position.z), new Vector2(playerPos.x, playerPos.z)));
    }

    //Choose side where to go to get closer to enemy
    public MG_Sides.Side DirectionToObj(Hero obj)
    {
        
        if (obj == null)
            return MG_Sides.Side.none;

        IntVector2 position = Position();
        IntVector2 dir = position - obj.posit;


        //There will be two options to move, if can't go to opt1, will go to opt2
        MG_Sides.Side opt1;
        MG_Sides.Side opt2;

        //If the direction X is closer than Z, choose left or right for the first option, and up or down for the second option
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            if (dir.x < 0)
                opt1 = MG_Sides.Side.right;
            else
                opt1 = MG_Sides.Side.left;

            if (dir.z < 0)
                opt2 = MG_Sides.Side.up;
            else
                opt2 = MG_Sides.Side.down;
        }
        //Else, do the oposite
        else
        {
            if (dir.z < 0)
                opt1 = MG_Sides.Side.up;
            else
                opt1 = MG_Sides.Side.down;

            if (dir.x < 0)
                opt2 = MG_Sides.Side.right;
            else
                opt2 = MG_Sides.Side.left;
        }
        //If there is a obstacle for opt1, go to opt2.
        //If there is a obstacle of opt2, the Move Func will not let him move, so he will stay in the same place;
        if (GetComponent<Moving>().GetFreeSides()[(int)opt1] )
        {
            if (flagDebug) Debug.Log(opt1);
            return opt1;
        }
        else if (GetComponent<Moving>().GetFreeSides()[(int)opt2])
        {
            if (flagDebug) Debug.Log(opt2);
            return opt2;
        }
        else return MG_Sides.Side.none;


    }


    protected override void Attack(MG_Sides.Side side)
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
                if (flagDebug) Debug.Log("Atak");
            }
        }
    }

    public override void TakeDmg(float dmg)
    {
        currHP -= dmg;
        if (currHP < 0) currHP = 0;
        if (currHP > maxHP) currHP = maxHP;
        health_bar.fillAmount = currHP / maxHP;
        if (currHP == 0) Die();
    }

    protected override void Die() // things that happen while the object dies
    {
        Instantiate(drop, transform.position, Quaternion.identity);
        Destroy(enemyObject);
    }

   
}
