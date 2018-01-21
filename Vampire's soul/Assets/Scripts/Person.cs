using UnityEngine;

[RequireComponent(typeof(Moving))]

public abstract class Person : MonoBehaviour {

    //public Image health_bar; // graphic health indicator 
    //(to implement in future)// protected Image lossOfHealth_bar; //graphic loss of health indicator

    protected float maxHP = 100; // the highest available level of health
    protected float currHP = 100; // current health
    protected int stun = 0; // how long person can't move

    //public abstract bool Action(); //formerly Move()
    protected abstract void Attack(MG_Sides.Side side); // execution of the set attack

    public virtual void TakeDmg(float dmg) //through this method object receives damage
    {
        currHP -= dmg;
        if (currHP < 0) currHP = 0;
        if (currHP > maxHP) currHP = maxHP;
        //health_bar.fillAmount = currHP / maxHP;
        if (currHP == 0) Die();
    }

    protected virtual void Die() // things that happen while the object dies
    {
        Destroy(gameObject);
    }

    public IntVector2 Position()
    {
        //Position on the game grid will be the Unity position, but rounded to the nearest Int
        return new IntVector2(Mathf.RoundToInt(this.GetComponent<Rigidbody>().position.x), Mathf.RoundToInt(this.GetComponent<Rigidbody>().position.y));
    }


    public virtual void Stune(int time, bool forced) //Stun service time-how long person will be stune forced if true add time to existed stun
    {
        if (forced) stun += time;
        else if (stun <= 0) stun = time;
    }
}
