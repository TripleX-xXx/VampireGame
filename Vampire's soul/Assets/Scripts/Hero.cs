using UnityEngine;
using UnityEngine.UI;

public class Hero : Person {

    //Position of player on grid
    public IntVector2 posit;
    public CanvasManager canvasMenager;

    private delegate float AttackType(Person p);
    AttackType attack = AttacksList.Attack1;

    private void Update()
    {
        if (Input.GetKeyDown("w")) Action(MG_Sides.Side.up);
        else if (Input.GetKeyDown("s")) Action(MG_Sides.Side.down);
        else if (Input.GetKeyDown("a")) Action(MG_Sides.Side.left);
        else if (Input.GetKeyDown("d")) Action(MG_Sides.Side.right);
        else if (Input.GetKeyDown("k")) Action(MG_Sides.Side.none); // skip round
        else if (Input.GetKeyDown("1")) SetAbilitie(3); // choose skill 1
        else if (Input.GetKeyDown("2")) SetAbilitie(2); // choose skill 2
        else if (Input.GetKeyDown("3")) SetAbilitie(1); // choose skill 3
        else if (Input.GetKeyDown("f")) Attack(); // Attack

    }

    private bool flagRoundEnd = false;

    private void Action(MG_Sides.Side side) // Key (wsadk) event control
    {
        if (side == MG_Sides.Side.none) return;
        else if (side == MG_Sides.EulerVectorToSide(transform.eulerAngles))
        {
            flagRoundEnd = GetComponent<Moving>().Move(side);
        }
        else
        {
            flagRoundEnd = false;
            GetComponent<Moving>().Move(side);
        }

        if(flagRoundEnd)
        {
            posit = Position();
            flagRoundEnd = false;
            TakeDmg(1);
            InitStep();
            canvasMenager.NextTour();
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
            
        }
        else if (a == 2)
        {
            if (canvasMenager.SelectBlink())
            {
                selectetAbilitie = a;
                attack = AttacksList.Attack3;
            }
            else SetAbilitie(1);
        }
        else if (a == 3)
        {
            if (canvasMenager.SelectWave())
            {
                selectetAbilitie = a;
                attack = AttacksList.Attack2;
            }
            else SetAbilitie(1);
        }
    }

    public void InitStep()
    {
        //Send it to the RoundSystem class to handle
        RoundSystem.UpdateStep();
    }

    protected override void Attack()
    {
        TakeDmg(attack(this));
        if (selectetAbilitie == 1) canvasMenager.UseBite();
        if (selectetAbilitie == 2) canvasMenager.UseBlink();
        if (selectetAbilitie == 3) canvasMenager.UseWave();
        SetAbilitie(selectetAbilitie);

        posit = Position();
        InitStep();
        canvasMenager.NextTour();

    }

    public override void TakeDmg(float dmg)
    {
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

}
