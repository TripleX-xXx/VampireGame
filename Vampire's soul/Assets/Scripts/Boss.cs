using System;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Person
{

    public bool flagDebug = false;
    public Hero hero;
    public Image health_bar; // graphic health indicator
    public GameObject enemyObject;
    public Animator animator;

    //How far can find the player
    public int distanceToSeePlayer = 5;
    //How close it need to be to attack
    public int distanceToAttack = 1;
    private MG_AudioManager audioManager;


    private int count = 0;

    private void Start()
    {
        maxHP = 250;
        currHP = 250;

        this.tag = "Enemy";
        hero = UnityEngine.Object.FindObjectOfType<Hero>();
        audioManager = FindObjectOfType<MG_AudioManager>();
        audioManager.Boss();
    }

    //Enemy's turn to do action
    public void OnnStep()
    {
        if(count == 3)
        {
            count = 0;
        }
        count++;

        Chase();
    }

    private void Chase()
    {
            if (count < 3 && distanceToSeePlayer >= DistanceFromObject(hero))
            {
                if (flagDebug) Debug.LogError("SmallBAM");
                AttackE(hero, 5);
                animator.SetTrigger("Attack1");
            } else if(count == 3 && distanceToAttack >= DistanceFromObject(hero))
            {
                if (flagDebug) Debug.LogError("BigBAM");
                AttackE(hero, 20);
                animator.SetTrigger("Attack2");
            }  
    }


    // Autorotate to player position
    public void RotateToPlayer()
    {
        Vector3 dir = transform.position - hero.transform.position;
        float angle = Mathf.Round(Vector3.Angle(dir, transform.up));

        if (angle == 90f)
        {
            Quaternion rotation = Quaternion.LookRotation(transform.position - hero.transform.position, transform.TransformDirection(Vector3.forward));
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
    }

    //Calculate position on grid between 2 objectss
    public int DistanceFromObject(Person obj)
    {

        IntVector2 position = Position();
        IntVector2 playerPos = obj.Position();

        return Mathf.CeilToInt(Vector3.Distance(new Vector3(position.x, position.z), new Vector2(playerPos.x, playerPos.z)));
    }

    public void AttackE(Hero P, float dmg)
    {
        audioManager.Play("Spell");
        P.TakeDmg(dmg);
    }

    protected override void Attack(MG_Sides.Side side)
    {
        RaycastHit hit;
        Vector3 site = MG_Sides.SideToVector3(
                        MG_Sides.EulerVectorToSide(transform.eulerAngles)
                        );

        if (Physics.Raycast(transform.position, site, out hit, 1f))
        {
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Person>().TakeDmg(30);
                if (flagDebug) Debug.Log("Atak");
            }
        }
    }

    public override void TakeDmg(float dmg)
    {
        animator.SetTrigger("GetDmg");
        currHP -= dmg;
        if (currHP < 0) currHP = 0;
        if (currHP > maxHP) currHP = maxHP;
        health_bar.fillAmount = currHP / maxHP;
        if (currHP == 0) Die();
    }

    protected override void Die() // things that happen while the object dies
    {

        Destroy(enemyObject);
    }

    void Update()
    {
        if (flagDebug)
        {
            Debug.DrawRay(transform.position, transform.up * distanceToSeePlayer, Color.green);
            Debug.DrawRay(transform.position, (transform.up + transform.right).normalized * distanceToSeePlayer, Color.green);
            Debug.DrawRay(transform.position, (transform.up - transform.right).normalized * distanceToSeePlayer, Color.green);
        }
    }
}

