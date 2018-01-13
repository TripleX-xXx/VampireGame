using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RoundSystem {

    public static void UpdateStep()
    {
        Enemy[] objs = Object.FindObjectsOfType<Enemy>();
        Spikes[] trap = Object.FindObjectsOfType<Spikes>();
        //Fist update the enemies, they can move and attack.
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].OnnStep();
        }
        for (int i = 0; i < trap.Length; i++)
        {
            trap[i].OnnStep();
        }
    }

}
