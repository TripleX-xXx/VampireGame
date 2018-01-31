using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    //Who will be spawned
    public Enemy enemyObj;
    //How much turns it sleeps
    private int sleeptime = 5;

    public void OnnStep()
    {
        if (ActiveCheck()) sleeptime--;
        else
        {
            sleeptime = 5;
            enemyObj.hero = Object.FindObjectOfType<Hero>();
            enemyObj.tag = "Enemy";
            Instantiate(enemyObj, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), enemyObj.transform.rotation);
        }
    }

    //Check if someone standing on that position and check if it is time to spawn new enemy
    private bool ActiveCheck()
    {
        Enemy[] objs = Object.FindObjectsOfType<Enemy>();
        Hero player = Object.FindObjectOfType<Hero>();
        Boss boss = Object.FindObjectOfType<Boss>();
        
        if (boss == null) sleeptime = 5;
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].transform.position == new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z) || player.transform.position == new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z))
            {
                sleeptime = 5;
                return true;
            }
        }
        if (sleeptime > 0)
            return true;
        else return false;
    }

}
