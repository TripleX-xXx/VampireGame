using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Load a stage using the string name
 */
public class LoadStage : MonoBehaviour {

    //Stage to load
    public string stageName;

    //Load the stage
	public void Load()
    {
        SceneManager.LoadScene(stageName);
    }
}
