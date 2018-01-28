﻿using UnityEngine;
using UnityEngine.UI;

public class Hero : Person {

    //Position of player on grid
    public IntVector2 posit;
    public CanvasManager canvasMenager;
    public Animator animator;

    private delegate float AttackType(Person p);
    private AttackType attack = AttacksList.Attack1;
    private Moving moving;
    private int ingredient = 0; // the amount of ingredients for the potion
    private int potion = 0; // the amount of potions
    private int maxPotions = 1; // maximum number of potions
    private GameObject go;

    private void Start()
    {
        moving = GetComponent<Moving>();
        //go = PlaneCreator.CreatePlane(3, 2);
    }

    private void Update()
    {
        if (stun == 0)
        {
            if (Input.GetKeyDown("w")) Action(MG_Sides.Side.up); // go Up
            else if (Input.GetKeyDown("s")) Action(MG_Sides.Side.down); // go Down
            else if (Input.GetKeyDown("a")) Action(MG_Sides.Side.left); // go Left
            else if (Input.GetKeyDown("d")) Action(MG_Sides.Side.right); // go Right
        } else
        {
            if (Input.GetKeyDown("w")) Action(MG_Sides.Side.none); // go Up
            else if (Input.GetKeyDown("s")) Action(MG_Sides.Side.none); // go Down
            else if (Input.GetKeyDown("a")) Action(MG_Sides.Side.none); // go Left
            else if (Input.GetKeyDown("d")) Action(MG_Sides.Side.none); // go Right
        }
        if (Input.GetKeyDown("k")) Action(MG_Sides.Side.none); // skip round
        else if (Input.GetKeyDown("1") || Input.GetKeyDown(KeyCode.Keypad1)) SetAbilitie(3); // choose skill 1
        else if (Input.GetKeyDown("2") || Input.GetKeyDown(KeyCode.Keypad2)) SetAbilitie(2); // choose skill 2
        else if (Input.GetKeyDown("3") || Input.GetKeyDown(KeyCode.Keypad3)) SetAbilitie(1); // choose skill 3
        else if (Input.GetKeyDown(KeyCode.UpArrow)) Attack(MG_Sides.Side.up); // Attack Up
        else if (Input.GetKeyDown(KeyCode.DownArrow)) Attack(MG_Sides.Side.down); // Attack Down
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) Attack(MG_Sides.Side.left); // Attack Left
        else if (Input.GetKeyDown(KeyCode.RightArrow)) Attack(MG_Sides.Side.right); // Attack Right
        else if (Input.GetKeyDown("e")) DrinkPotion(); // Drink Potion

    }

    private bool flagRoundEnd = false;

    private void Action(MG_Sides.Side side) // Key (wsadk) event control
    {

        if (side == MG_Sides.Side.none) flagRoundEnd = true;
        else flagRoundEnd = moving.Move(side);

        if (flagRoundEnd)
        {
            flagRoundEnd = false;
            TakeDmg(1);
            InitStep();
        }

    }

    int selectetAbilitie = 1;

    private void SetAbilitie(int a) // set the selected skill. If it's the same then set movement mode
    {
        if (a == 1) // Defoult Attack must have coolDown == 0
        {
            selectetAbilitie = a;
            attack = AttacksList.Attack1;
            canvasMenager.SelectBite();
            canvasMenager.SetHealthBarGold(currHP / maxHP);
            
        }
        else if (a == 2)
        {
            if (canvasMenager.SelectBlink())
            {
                selectetAbilitie = a;
                attack = AttacksList.Attack3;
                canvasMenager.SetHealthBarGold((currHP-5) / maxHP);
            }
            else SetAbilitie(1);
        }
        else if (a == 3)
        {
            if (canvasMenager.SelectWave())
            {
                selectetAbilitie = a;
                attack = AttacksList.Attack2;
                canvasMenager.SetHealthBarGold((currHP - 15) / maxHP);
                go = PlaneCreator.CreatePlane(3, 2);
                go.transform.position = this.transform.position + AttacksList.getSideVector(this) * 1f * 1.5f;
            }
            else
            {
                SetAbilitie(1);
                Destroy(go);
            }
        }
    }

    public void InitStep()
    {
        posit = Position();
        //Send it to the RoundSystem class to handle
        RoundSystem.UpdateStep();
        canvasMenager.NextTour();
        if (stun > 0) stun--;
    }

    protected override void Attack(MG_Sides.Side side)
    {
        moving.Rotate(side);
        animator.SetTrigger("Attack2");
        TakeDmg(attack(this));
        if (selectetAbilitie == 1) canvasMenager.UseBite();
        if (selectetAbilitie == 2) canvasMenager.UseBlink();
        if (selectetAbilitie == 3) canvasMenager.UseWave();
        SetAbilitie(selectetAbilitie);

        InitStep();

    }

    public override void TakeDmg(float dmg)
    {
        if (dmg > 1) animator.SetTrigger("GetHit");
        currHP -= dmg;
        if (currHP < 0) currHP = 0;
        if (currHP > maxHP) currHP = maxHP;
        canvasMenager.SetHealthBar(currHP / maxHP);
        if (currHP == 0) Die();
    }

    protected override void Die()
    {
        Destroy(gameObject);
        //GameOver();
    }

    public bool AddIngredient()
    {
        if(potion < maxPotions)
        {
            if (++ingredient >= 3)
            {
                ingredient = 0;
                potion++;
                // add graphic representation
            }
            canvasMenager.AddIngredient();
            return true;
        }
        return false;
    }

    private void DrinkPotion()
    {
        if(potion > 0)
        {
            TakeDmg(-30);
            canvasMenager.UsePotion();
            potion--;
        }
    }


}
