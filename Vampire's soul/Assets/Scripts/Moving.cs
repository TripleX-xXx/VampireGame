using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    public GameObject camerad;
    private Rigidbody2D cr;
    private Vector3 p;

    public void SetPlayer(MG_Person go) {
        //player = go;
    }

    public void SetUp()
    {
        cr = camerad.GetComponent<Rigidbody2D>();
        //p = player.transform.position;
        //cr.MovePosition(p);
    }

    public enum Site {left = 1, right = 2, up = 3, down = 4};

    public bool Move(MG_Person player,Site site)
    {
        cr = camerad.GetComponent<Rigidbody2D>();
        p = player.transform.position;
        cr.MovePosition(p);

        Vector3 up = new Vector3(p.x, p.y + 0.17f, p.z);
        Vector3 down = new Vector3(p.x, p.y - 0.17f, p.z);
        Vector3 left = new Vector3(p.x - 0.17f, p.y, p.z);
        Vector3 right = new Vector3(p.x + 0.17f, p.y, p.z);

        Debug.DrawRay(up, new Vector3(0, 0.17f, 0), Color.green);
        Debug.DrawRay(down, new Vector3(0, -0.17f, 0), Color.green);
        Debug.DrawRay(right, new Vector3(0.17f, 0, 0), Color.green);
        Debug.DrawRay(left, new Vector3(-0.17f, 0, 0), Color.green);

        bool flagUp = true;
        bool flagDown = true;
        bool flagLeft = true;
        bool flagRight = true;

        if (Physics2D.Raycast(up, Vector2.up, 0.17f))
        {
            //print("up");
            flagUp = false;
        }
        if (Physics2D.Raycast(down, Vector2.down, 0.17f))
        {
            //print("down");
            flagDown = false;
        }
        if (Physics2D.Raycast(left, Vector2.left, 0.17f))
        {
            //print("left");
            flagLeft = false;
        }
        if (Physics2D.Raycast(right, Vector2.right, 0.17f))
        {
            //print("right");
            flagRight = false;
        }

        Rigidbody2D r = player.GetComponent<Rigidbody2D>();

        if (site == Site.left)
        {
            r.MoveRotation(90);
            if (flagLeft)
            {
                r.MovePosition(new Vector2(p.x - 0.32f, p.y));
                return true;
            }
        }
       else if (site == Site.right)
        {
            r.MoveRotation(-90);
            if (flagRight)
            {
                r.MovePosition(new Vector2(p.x + 0.32f, p.y));
                return true;
            }
        }
       else if (site == Site.up)
        {
            r.MoveRotation(0);
            if (flagUp)
            {
                r.MovePosition(new Vector2(p.x, p.y + 0.32f));
                return true;
            }
        }
        else if (site == Site.down)
        {
            r.MoveRotation(180);
            if (flagDown)
            {
                r.MovePosition(new Vector2(p.x, p.y - 0.32f));
                return true;
            }
        }
        return false;

    }
}
