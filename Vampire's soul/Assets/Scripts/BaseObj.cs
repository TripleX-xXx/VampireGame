using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseObj : MonoBehaviour {
    //Types of object inside the game
    //Can add more as needed
    public enum Type
    {
        player,
        enemy,
        interactive,
        wall
    }

    //Type of the object
    public Type ObjType;
    //Keep the position of the graphic object
    protected Vector3 graphicsInitPos;
    //Graphical representation of the object, a sprite or 3D model
    public GameObject graphics;

    // Use this for initialization
    void Start () {
        //Get the Grid position of the item
        IntVector2 pos = Position();
        //Set the initial position of graphics (local)
        if (graphics != null)
            graphicsInitPos = graphics.transform.localPosition;
    }

  /*  public int DistanceFromObject(BaseObj obj)
    {
        if (obj == null)
            return 999;

        IntVector2 position = Position();
        IntVector2 playerPos = MG_Hero.playerInstance.Position();

        return Mathf.CeilToInt(Vector2.Distance(new Vector2(position.x, position.z), new Vector2(playerPos.x, playerPos.z)));
    }
    */
    //Position in the game Grid
    public IntVector2 Position()
    {
        //Position on the game grid will be the Unity position, but rounded to the nearest Int
        return new IntVector2(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z));
    }

    //Update the object position on grid (Ex: Obj Moved)
    public void UpdatePosition(IntVector2 newPos)
    {


        //Get the current pos
        Vector3 lastPos = transform.position;
        //Update the pos
        transform.position = new Vector3(newPos.x, transform.position.y, newPos.z);
        //Start the movement animation
        //........
    }


    }
