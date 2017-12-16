using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Moving : MonoBehaviour {

    public bool flagDebug = false; // on/off debug mode

    public float cellSize = 1f; // grid cell size
    private Rigidbody r; // rigidbody of moving object
    private Vector3 p; // position of moving object

    private bool[] freeSidesTab = { /*Up*/true, /*Down*/true, /*Left*/true, /*Right*/true };

    void Awake()
    {
        r = GetComponent<Rigidbody>();
    }

    public bool Move(MG_Sides.Side side) // turns and move towards 'side' (return true if it can end the move otherwise only rotation)
    {
        if (side == MG_Sides.Side.none) return true;
        SetFreeSides();

        if (flagDebug)
        {
            Debug.DrawRay(p, Vector3.up * cellSize, Color.green);
            Debug.DrawRay(p, Vector3.down * cellSize, Color.green);
            Debug.DrawRay(p, Vector3.left * cellSize, Color.green);
            Debug.DrawRay(p, Vector3.right * cellSize, Color.green);
            //Debug.Log("UP:"+freeSidesTab[0] + " DOWN:" + freeSidesTab[1] + " LEFT:" + freeSidesTab[2] + " RIGHT:" + freeSidesTab[3]);
        }

        if (side == MG_Sides.EulerVectorToSide(transform.eulerAngles)) {;}
        else if (side == MG_Sides.Side.up)
        {
            r.MoveRotation(Quaternion.Euler(new Vector3(0, 0, 0)));
            if (!freeSidesTab[0]) return false;
            else return true;
        }
        else if (side == MG_Sides.Side.down)
        {
            r.MoveRotation(Quaternion.Euler(new Vector3(0, 0, 180)));
            if (!freeSidesTab[1]) return false;
            else return true;
        }
        else if (side == MG_Sides.Side.left)
        {
            r.MoveRotation(Quaternion.Euler(new Vector3(0, 0, 90)));
            if (!freeSidesTab[2]) return false;
            else return true;
        }
        else if (side == MG_Sides.Side.right)
        {
            r.MoveRotation(Quaternion.Euler(new Vector3(0, 0, -90)));
            if (!freeSidesTab[3]) return false;
            else return true;
        }

        if(freeSidesTab[(int)side]) r.MovePosition(p + MG_Sides.SideToVector3(side) * cellSize);
        return true;

    }

    private void SetFreeSides() // updates freeSidesTab
    {
        p = GetComponent<Transform>().position;

        Ray RayUp = new Ray(p, Vector3.up);
        Ray RayDown = new Ray(p, Vector3.down);
        Ray RayLeft = new Ray(p, Vector3.left);
        Ray RayRight = new Ray(p, Vector3.right);

        freeSidesTab[0] = true;
        freeSidesTab[1] = true;
        freeSidesTab[2] = true;
        freeSidesTab[3] = true;

        if (Physics.Raycast(RayUp, cellSize)) freeSidesTab[0] = false;    // up
        if (Physics.Raycast(RayDown, cellSize)) freeSidesTab[1] = false;  // down
        if (Physics.Raycast(RayLeft, cellSize)) freeSidesTab[2] = false;  // left
        if (Physics.Raycast(RayRight, cellSize)) freeSidesTab[3] = false; // right
    }

    public bool[] GetFreeSides() // returns which sides are free
    {
        SetFreeSides();
        return freeSidesTab;
    }

}
