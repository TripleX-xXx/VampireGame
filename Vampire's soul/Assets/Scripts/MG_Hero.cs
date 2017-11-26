using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Hero : MonoBehaviour{

    public Image health_bar;
    public GameObject scriptHolding;

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

    bool Move() {
        //ToDo
        return true;
    }

    bool flagMove = false;

    private void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            flagMove = m.Move(Moving.Site.left);
        }
        else if (Input.GetKeyDown("d"))
        {
            flagMove = m.Move(Moving.Site.right);
        }
        else if (Input.GetKeyDown("w"))
        {
            flagMove = m.Move(Moving.Site.up);
        }
        else if (Input.GetKeyDown("s"))
        {
            flagMove = m.Move(Moving.Site.down);
        }

        if (flagMove)
        {
            TakeDmg(1);
            flagMove = !flagMove;
        }

    }

}
