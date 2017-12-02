using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Hero : MG_Person {

    public Image health_bar;

    private Moving m;
    private int maxHP = 100;
    public int hp = 100;

    private void Start()
    {
        m = scriptHolding.GetComponent<Moving>();
    }

    public void TakeDmg(int dmg)
    {
        hp -= dmg;
        health_bar.fillAmount = (float)hp / maxHP;
    }

    bool flagMove = false;

    public override bool Move() {
        //Set();
        //m.SetPlayer(this);
        //m.SetUp();
        while (!flagMove) {

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
        flagMove = !flagMove;
        return true;
    }
    
    private void Update()
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
            flagMove = !flagMove;
        }

    }
    
}
