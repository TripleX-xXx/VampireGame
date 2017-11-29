using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG_Round : MonoBehaviour {

    public MG_Person[] tab;

    private int i = 0;
    private bool flag = false;

    private void Play() {
        while (true) {
            foreach (MG_Person a in tab) {
                if(a.Move());
            }
        }
    }

    private void Update()
    {
        Debug.Log("dzialam");
        flag = tab[i].Move();
        if (flag) i++;
        if (i == tab.Length) i = 0;
        
    }

}
