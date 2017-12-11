using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{


    public GameObject camerad; //focused camera
    private Rigidbody r; //player's rigidbody
    private Rigidbody cr; //camera's rigidbody
    private Vector3 p; //position of moving object
    public float cellSize = 0.5f; //grid cell size
    public Vector2 camPosition;
    public void SetPlayer(MG_Person go)
    {
        //player = go;
    }

    public void SetUp()
    {
        cr = camerad.GetComponent<Rigidbody>();
        //p = player.transform.position;
        //cr.MovePosition(p);
    }

    public enum Site
    {
        up,
        down,
        left,
        right,
        none
    };
    //Order need to be the same as SideChoices, is the position each side will have.
    public static IntVector2[] sideVector = {
        new IntVector2(0, 1),
        new IntVector2(0, -1),
        new IntVector2(-1, 0),
        new IntVector2(1, 0),
        new IntVector2(0, 0)
    };
    //Transform the enum SideChoices to its respective IntVector2 
    public static IntVector2 SideToVector(Site side)
    {
        return sideVector[(int)side];
    }

    /*
*  moves the object one cell in a given direction
*  return true if object could be moved
*  player - object to moved
*  site - page in which the object is to be moved
*/
    public bool Move(MG_Person player, Site site)
    {


        r = player.GetComponent<Rigidbody>();
        cr = camerad.GetComponent<Rigidbody>();
        p = player.transform.position;
        if (player.tag == "Player")
            cr.MovePosition(new Vector3(p.x + camPosition.x, p.y + camPosition.y, -10));

        Ray rayUp = new Ray(p, Vector3.up);
        Ray rayDown = new Ray(p, Vector3.down);
        Ray rayLeft = new Ray(p, Vector3.left);
        Ray rayRight = new Ray(p, Vector3.right);

        Debug.DrawRay(p, Vector3.up * cellSize, Color.green);
        Debug.DrawRay(p, Vector3.down * cellSize, Color.green);
        Debug.DrawRay(p, Vector3.left * cellSize, Color.green);
        Debug.DrawRay(p, Vector3.right * cellSize, Color.green);

        bool flagUp = true;
        bool flagDown = true;
        bool flagLeft = true;
        bool flagRight = true;

        if (Physics.Raycast(rayUp, cellSize)) flagUp = false;
        if (Physics.Raycast(rayDown, cellSize)) flagDown = false;
        if (Physics.Raycast(rayLeft, cellSize)) flagLeft = false;
        if (Physics.Raycast(rayRight, cellSize)) flagRight = false;

        //if(player.tag == "Player")Debug.Log("Up:"+flagUp+" Down:"+flagDown+" Left:"+flagLeft+" Right:"+flagRight);

        if (site == Site.left)
        {
            r.MoveRotation(Quaternion.Euler(new Vector3(0, 0, 90)));
            if (flagLeft)
            {
                r.MovePosition(new Vector3(p.x - cellSize, p.y));
                return true;
            }
        }
        else if (site == Site.right)
        {
            r.MoveRotation(Quaternion.Euler(new Vector3(0, 0, -90)));
            if (flagRight)
            {
                r.MovePosition(new Vector3(p.x + cellSize, p.y));
                return true;
            }
        }
        else if (site == Site.up)
        {
            r.MoveRotation(Quaternion.Euler(new Vector3(0, 0, 0)));
            if (flagUp)
            {
                r.MovePosition(new Vector3(p.x, p.y + cellSize));
                return true;
            }
        }
        else if (site == Site.down)
        {
            r.MoveRotation(Quaternion.Euler(new Vector3(0, 0, 180)));
            if (flagDown)
            {
                r.MovePosition(new Vector3(p.x, p.y - cellSize));
                return true;
            }
        }
        return false;

    }
}
