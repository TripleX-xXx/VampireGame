using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu2 : MonoBehaviour {

    public GameObject o;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            o.active = !o.active;
        }
    }

    public void ContinueButton()
    {
        o.active = !o.active;
    }

    public void SettingsButton()
    {
        //To do
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
