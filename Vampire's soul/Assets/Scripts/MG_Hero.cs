using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Hero : MG_Person
{

    private Moving m;
    public MG_Enemy enemy;
    public int atkRange = 1;

    //Position of player on grid
    public IntVector2 posit;

    private void Start()
    {
        m = scriptHolding.GetComponent<Moving>();
        //  pos = this.GetComponent<Transform>().position;

        
    }

    public void TakeDmg(float dmg)
    {
       //hp -= dmg;
        currHP -= dmg;
        health_bar.fillAmount = (float)currHP / maxHP;
        if (currHP <= 0) Die();
    }

    bool flagMove = false;

    public override bool Move()
    {
        //Set();
        //m.SetPlayer(this);
        //m.SetUp();
        while (!flagMove)
        {

            if (Input.GetKeyDown("a"))
            {
                flagMove = m.Move(this, Moving.Site.left);
                
            }
            else if (Input.GetKeyDown("d"))
            {
                flagMove = m.Move(this, Moving.Site.right);
            }
            else if (Input.GetKeyDown("w"))
            {
                flagMove = m.Move(this, Moving.Site.up);
            }
            else if (Input.GetKeyDown("s"))
            {
                flagMove = m.Move(this, Moving.Site.down);
            }

            if (flagMove)
            {
                TakeDmg(1);
            }

        }
        InitStep();
        flagMove = !flagMove;
        return true;
    }

    //Attack enemy and check if hp==0 destroy
    public void AttackP(MG_Enemy e, float dmg)
    {
        e.hp = e.hp - dmg;
        if (e.hp <= 0)
        {
            Die();
        }
    }

    //Let enemy make turn
    public void InitStep()
    {
        //Send it to the MG_Person class to handle
        UpdateStep();
    }

    private bool flagR = false;

    private void Update()
    {

                if (Input.GetKeyDown("a"))
                {
                    flagMove = m.Move(this, Moving.Site.left);
                    //InitStep();
                    //flagR = true;
                }
                else if (Input.GetKeyDown("d"))
                {
                    flagMove = m.Move(this, Moving.Site.right);
                //InitStep();
                //flagR = true;
            }
                else if (Input.GetKeyDown("w"))
                {
                    flagMove = m.Move(this, Moving.Site.up);
                //InitStep();
                //flagR = true;
            }
                else if (Input.GetKeyDown("s"))
                {
                    flagMove = m.Move(this, Moving.Site.down);
                //InitStep();
                //flagR = true;
            } 
            
            
                if (Input.GetKeyDown("f"))
                {
                  Attack();
                //InitStep();
                flagR = true;
                }
        
        
             if (Input.GetKeyDown("k"))
        {
            flagR = true;
            //flagMove = m.Move(this, Moving.Site.none);
            //InitStep();
        }
        //Give and update position of player
        posit = Position();
        if (flagMove) 
        {
            TakeDmg(1);
            flagMove = !flagMove;
            flagR = true;
        }
        if (flagR) {
            InitStep();
            flagR = !flagR;
        }

    }

    //Calculate position on grid between 2 objectss
    public int DistanceFromObject(MG_Person obj)
    {
        if (obj == null)
            return 999;

        //Your position
        IntVector2 position = Position();
        //targer position
        IntVector2 enemyPos = enemy.Position();
      //  Debug.Log(position);
       // Debug.Log(enemyPos);

        //Calculating in int distance between two cells
        return Mathf.CeilToInt(Vector3.Distance(new Vector3(position.x, position.z), new Vector2(enemyPos.x, enemyPos.z)));
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
        if (Physics.Raycast(transform.position, site, out hit, 1f))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<MG_Enemy>().TakeDmg(30);
                Debug.LogError("Atak");
            }
        }
    }
}
