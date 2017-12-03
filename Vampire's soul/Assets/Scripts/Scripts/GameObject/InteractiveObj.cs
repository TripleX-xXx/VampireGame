using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Should be used for all objects that are interactive, items, doors, buttons on the ground, etc
 */

public class InteractiveObj : BaseObj {

	void Reset()
    {
        ObjType = Type.interactive;
    }
    //If a enemy get on top, call this
    public virtual void EnemyGet()
    {

    }
    //If player get on top, call this
    public virtual void PlayerGet()
    {

    }
}
