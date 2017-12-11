using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Hero : MG_Person
{

    private Moving m;
    public GameObject posi;
    public BaseObj basse;
    public Vector3 pos;

    private void Start()
    {
        m = scriptHolding.GetComponent<Moving>();
        playerInstance = this;
        pos = this.GetComponent<Transform>().position;
    }

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
        health_bar.fillAmount = (float)hp / maxHp;
    }

    bool flagMove = false;
    bool flagRoundEnd = false;

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

    public void InitStep()
    {
        UpdateStep();
    }

    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            flagMove = m.Move(this, Moving.Site.left);
            //InitStep();
            flagRoundEnd = true;
        }
        else if (Input.GetKeyDown("d"))
        {
            flagMove = m.Move(this, Moving.Site.right);
            //InitStep();
            flagRoundEnd = true;
        }
        else if (Input.GetKeyDown("w"))
        {
            flagMove = m.Move(this, Moving.Site.up);
            //InitStep();
            flagRoundEnd = true;
        }
        else if (Input.GetKeyDown("s"))
        {
            flagMove = m.Move(this, Moving.Site.down);
            //InitStep();
            flagRoundEnd = true;
        }
        else if (Input.GetKeyDown("q"))
        {
            Attack();
            flagRoundEnd = true;
        }
       
        if (flagMove)
        {
            TakeDmg(1);
            flagMove = !flagMove;
        }
        if (flagRoundEnd)
        {
            flagRoundEnd = !flagRoundEnd;
            InitStep();
        }
    }

    //attacking the enemy in front of you (press q to attack)
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
                hit.collider.GetComponent<MG_Enemy>().TakeDmg(10);
                Debug.LogError("Atak");
            }
        }
    }

}
