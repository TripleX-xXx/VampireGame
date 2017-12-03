 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MG_Enemy : MG_Person {

    //public Image health_bar;

    private Moving m;
    //private int maxHP = 100;
    //public int hp = 100;

    private void Start()
    {
        m = scriptHolding.GetComponent<Moving>();
    }

    //bool flagMove = false;
    
    //simple AI
    public override bool Move() {
        if (m.Move(this, Moving.Site.left)) ;
        else if (m.Move(this, Moving.Site.right)) ;
        else if (m.Move(this, Moving.Site.up)) ;
        else if (m.Move(this, Moving.Site.down)) ;
        return true;
    }
}
