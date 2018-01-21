using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoundSystem {

    public static void UpdateStep()
    {
        Enemy[] objs = Object.FindObjectsOfType<Enemy>();
        Spikes[] trap = Object.FindObjectsOfType<Spikes>();
        Mud[] mud = Object.FindObjectsOfType<Mud>();

        Boss boss = null;
        boss = Object.FindObjectOfType<Boss>();
        if(boss != null)boss.OnnStep();

        Spawner[] spawner = null;
        spawner = Object.FindObjectsOfType<Spawner>();
        if (spawner != null)
            for (int i = 0; i < spawner.Length; i++)
            {
                spawner[i].OnnStep();
            }
        //Fist update the enemies, they can move and attack.
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].OnnStep();
        }
        for (int i = 0; i < trap.Length; i++)
        {
            trap[i].OnnStep();
        }
        for (int i = 0; i < mud.Length; i++)
        {
            mud[i].OnnStep();
        }

    }

}
