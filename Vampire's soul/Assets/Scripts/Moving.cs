using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{

    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(player.transform.position, new Vector3(0, 0.17f, 0), Color.green);
        Debug.DrawRay(player.transform.position, new Vector3(0, -0.17f, 0), Color.green);
        Debug.DrawRay(player.transform.position, new Vector3(0.17f, 0, 0), Color.green);
        Debug.DrawRay(player.transform.position, new Vector3(-0.17f, 0, 0), Color.green);

        bool flagUp = true;
        bool flagDown = true;
        bool flagLeft = true;
        bool flagRight = true;

        if (Physics2D.Raycast(player.transform.position, Vector2.up, 0.17f))
        {
            print("up");
            //flagUp = false;
        }
        if (Physics2D.Raycast(player.transform.position, Vector2.down, 0.17f))
        {
            print("down");
            //flagDown = false;
        }
        if (Physics2D.Raycast(player.transform.position, Vector2.left, 0.17f))
        {
            print("left");
            //flagLeft = false;
        }
        if (Physics2D.Raycast(player.transform.position, Vector2.right, 0.17f))
        {
            print("right");
            //flagRight = false;
        }


        Vector3 p = player.transform.position;
        Rigidbody2D r = player.GetComponent<Rigidbody2D>();

        if (Input.GetKeyDown("a") && flagLeft)
        {
            r.MovePosition(new Vector2(p.x - 0.32f, p.y));
            print("a");
        }
        if (Input.GetKeyDown("d") && flagRight)
        {
            r.MovePosition(new Vector2(p.x + 0.32f, p.y));
            print("d");
        }
        if (Input.GetKeyDown("w") && flagUp)
        {
            r.MovePosition(new Vector2(p.x, p.y + 0.32f));
            print("w");
        }
        if (Input.GetKeyDown("s") && flagDown)
        {
            r.MovePosition(new Vector2(p.x, p.y - 0.32f));
            print("s");
        }
    }
}
