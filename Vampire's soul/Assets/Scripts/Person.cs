using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Moving))]

public abstract class Person : MonoBehaviour {

    public Image health_bar; // graphic health indicator 
    //(to implement in future)// protected Image lossOfHealth_bar; //graphic loss of health indicator

    protected float maxHP = 100; // the highest available level of health
    protected float currHP = 100; // current health

    //public abstract bool Action(); //formerly Move()
    protected abstract void Attack(); // execution of the set attack  *deprecated*

    public virtual void TakeDmg(float dmg) //through this method object receives damage
    {
        currHP -= dmg;
        if (currHP < 0) currHP = 0;
        if (currHP > maxHP) currHP = maxHP;
        health_bar.fillAmount = currHP / maxHP;
        if (currHP == 0) Die();
    }

    protected virtual void Die() // things that happen while the object dies
    {
        Destroy(gameObject);
    }

}
