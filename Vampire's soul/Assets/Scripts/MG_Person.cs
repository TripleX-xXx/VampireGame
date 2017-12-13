using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MG_Person : MonoBehaviour
{

    public GameObject scriptHolding;
    public  MG_Hero playerInstance;
    public MG_Enemy enemyInstance;
    public float maxHP = 100;
    public float currHP = 100;

    abstract public bool Move();
    abstract protected void Attack();

    public IntVector2 Position()
    {
        //Position on the game grid will be the Unity position, but rounded to the nearest Int
        return new IntVector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y));
    }


    //Actions that the object will do on his in game turn (usually after the player move or attack)
    public virtual void OnStep()
    {

    }

    //Call the OnStep on all the BaseObj in the game
    public static void UpdateStep()
    {
        MG_Enemy[] objs = GameObject.FindObjectsOfType<MG_Enemy>();
        //Fist update the enemies, they can move and attack.
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].OnnStep();
        }
    }

    public void Die(MG_Person obj)
    {
            Destroy(obj.gameObject);
            //GameOver();
    }
}
