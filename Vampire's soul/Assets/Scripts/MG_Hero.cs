﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Hero : MG_Person {

    public Image health_bar; //graphic representation of the health bar

    private Moving m; //clase to moveing
    private int maxHP = 100; //max helth
    public int hp; //current helth

    private void Start()
    {
        m = scriptHolding.GetComponent<Moving>();
        hp = maxHP;
    }

    // method to take damage 
    public void TakeDmg(int dmg)
    {
        hp -= dmg;
        health_bar.fillAmount = (float)hp / maxHP;
    }

    bool flagMove = false; //flag indicating whether we made a move

    public override bool Move() {

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
    
    /*
    //Bebug method to test moving

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
    */
    
}
