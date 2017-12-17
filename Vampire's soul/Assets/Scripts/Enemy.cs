using UnityEngine;

public class Enemy : Person {

    public Hero hero;

    //How far can find the player
    public int distanceToSeePlayer = 5;
    //How close it need to be to attack
    public int distanceToAttack = 1;


    //Enemy's turn to do action
    public void OnnStep()
    {
      //  GetComponent<Moving>().Move(MG_Sides.Side.up); // for debug
                                                       // place for connecting IA
                                                       //For each action it have, do a action
        if (CanSeePlayer())
        {
            //If close to player
            if (distanceToAttack == DistanceFromObject(hero))
            {
                //AttackE(hero, 1); 
                Attack();
            }
            else
                //if you see but far away
                GetComponent<Moving>().Move(DirectionToObj(hero)); 
        }
    }

    //Can it see the player?
    public bool CanSeePlayer()
    {
        Debug.Log(DistanceFromObject(hero));
        return DistanceFromObject(hero) <= distanceToSeePlayer;
    }

    //Calculate position on grid between 2 objectss
    public int DistanceFromObject(Person obj)
    {

        IntVector2 position = Position();
        IntVector2 playerPos = obj.Position();

        return Mathf.CeilToInt(Vector3.Distance(new Vector3(position.x, position.z), new Vector2(playerPos.x, playerPos.z)));
    }

    //Choose side where to go to get closer to enemy
    public MG_Sides.Side DirectionToObj(Hero obj)
    {

        if (obj == null)
            return MG_Sides.Side.none;

        IntVector2 position = Position();
        IntVector2 dir = position - obj.posit;


        //There will be two options to move, if can't go to opt1, will go to opt2
        MG_Sides.Side opt1;
        MG_Sides.Side opt2;

        //If the direction X is closer than Z, choose left or right for the first option, and up or down for the second option
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
        {
            if (dir.x < 0)
                opt1 = MG_Sides.Side.right;
            else
                opt1 = MG_Sides.Side.left;

            if (dir.z < 0)
                opt2 = MG_Sides.Side.up;
            else
                opt2 = MG_Sides.Side.down;
        }
        //Else, do the oposite
        else
        {
            if (dir.z < 0)
                opt1 = MG_Sides.Side.up;
            else
                opt1 = MG_Sides.Side.down;

            if (dir.x < 0)
                opt2 = MG_Sides.Side.right;
            else
                opt2 = MG_Sides.Side.left;
        }
        //If there is a obstacle for opt1, go to opt2.
        //If there is a obstacle of opt2, the Move Func will not let him move, so he will stay in the same place;
        if (GetComponent<Moving>().GetFreeSides()[(int)opt1])
        {
            Debug.LogError(opt1);
            return opt1;
        }
        else if (GetComponent<Moving>().GetFreeSides()[(int)opt2])
        {
            Debug.LogError(opt2);
            return opt2;
        }
        else return MG_Sides.Side.none;


    }


    protected override void Attack()
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
                Debug.LogError("Atak");
            }
        }
    }

}
