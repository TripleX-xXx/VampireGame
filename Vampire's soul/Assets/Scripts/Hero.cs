using UnityEngine;

public class Hero : Person {

    private void Update()
    {
        if (Input.GetKeyDown("w")) Action(MG_Sides.Side.up);
        else if (Input.GetKeyDown("s")) Action(MG_Sides.Side.down);
        else if (Input.GetKeyDown("a")) Action(MG_Sides.Side.left);
        else if (Input.GetKeyDown("d")) Action(MG_Sides.Side.right);
        else if (Input.GetKeyDown("k")) Action(MG_Sides.Side.none); // skip round
        /* not done yet
        else if (Input.GetKeyDown("1")) SetAbilitie(1); // choose skill 1
        else if (Input.GetKeyDown("2")) SetAbilitie(2); // choose skill 2
        else if (Input.GetKeyDown("3")) SetAbilitie(3); // choose skill 3
        */
    }

    private bool flagRoundEnd = false;

    private void Action(MG_Sides.Side side) // Key (wsadk) event control
    {
        if (side == MG_Sides.Side.none) flagRoundEnd = true;
        else if (side == MG_Sides.EulerVectorToSide(transform.eulerAngles))
        {
            flagRoundEnd = true;
            if (/*coś == null czyli ruch*/true) flagRoundEnd = GetComponent<Moving>().Move(side);
            else
            {
                // wykonaj "takeDmg(coś())"
            }
        }
        else flagRoundEnd = GetComponent<Moving>().Move(side);

        if(flagRoundEnd)
        {
            flagRoundEnd = false;
            InitStep();
        }

    }

    private void SetAbilitie(int a) // set the selected skill. If it's the same then set movement mode
    {
        // to implemented
    }

    public void InitStep()
    {
        //Send it to the RoundSystem class to handle
        RoundSystem.UpdateStep();
    }

    protected override void Attack()
    {
        RaycastHit hit;
        Vector3 site = MG_Sides.SideToVector3(
                        MG_Sides.EulerVectorToSide(transform.eulerAngles)
                        );

        if (Physics.Raycast(transform.position, site, out hit, 1f))
        {
            if (hit.collider.tag == "Enemy")
            {
                hit.collider.GetComponent<Person>().TakeDmg(30);
                Debug.LogError("Atak");
            }
        }
    }

    protected override void Die() // things that happen while the object dies
    {
        Destroy(gameObject);
        //GameOver();
    }

}
