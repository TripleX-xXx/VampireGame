using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MG_Person : MonoBehaviour {

    public GameObject scriptHolding; //gameObject with moving script

    abstract public bool Move(); // method to move
}
