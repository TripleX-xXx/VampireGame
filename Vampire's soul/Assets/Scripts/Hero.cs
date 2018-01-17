using UnityEngine;
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

    private void Start()
    {
        moving = GetComponent<Moving>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("w")) Action(MG_Sides.Side.up); // go Up
        else if (Input.GetKeyDown("s")) Action(MG_Sides.Side.down); // go Down
        else if (Input.GetKeyDown("a")) Action(MG_Sides.Side.left); // go Left
        else if (Input.GetKeyDown("d")) Action(MG_Sides.Side.right); // go Right
        else if (Input.GetKeyDown("k")) Action(MG_Sides.Side.none); // skip round
        else if (Input.GetKeyDown("1") || Input.GetKeyDown(KeyCode.Keypad1)) SetAbilitie(3); // choose skill 1
        else if (Input.GetKeyDown("2") || Input.GetKeyDown(KeyCode.Keypad2)) SetAbilitie(2); // choose skill 2
        else if (Input.GetKeyDown("3") || Input.GetKeyDown(KeyCode.Keypad3)) SetAbilitie(1); // choose skill 3
        else if (Input.GetKeyDown(KeyCode.UpArrow)) Attack(MG_Sides.Side.up); // Attack Up
        else if (Input.GetKeyDown(KeyCode.DownArrow)) Attack(MG_Sides.Side.down); // Attack Down
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) Attack(MG_Sides.Side.left); // Attack Left
        else if (Input.GetKeyDown(KeyCode.RightArrow)) Attack(MG_Sides.Side.right); // Attack Right
        else if (Input.GetKeyDown("p")) DrinkPotion(); // Drink Potion

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
        posit = Position();
        //Send it to the RoundSystem class to handle
        RoundSystem.UpdateStep();
        canvasMenager.NextTour();
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
            return true;
        }
        return false;
    }

    private void DrinkPotion()
    {
        if(potion > 0)
        {
            TakeDmg(-30);
            potion--;
        }
    }

}
