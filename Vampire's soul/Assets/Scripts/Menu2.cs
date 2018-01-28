using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu2 : MonoBehaviour {

    public GameObject menu;
    public GameObject settings;

    // Use this for initialization
    void Start () {
        menu.SetActive(false);
        settings.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(settings.activeSelf) settings.SetActive(false);
            else menu.SetActive(!menu.activeSelf);
        }
    }

    public void ContinueButton()
    {
        menu.SetActive(!menu.activeSelf);
    }

    public void SettingsButton()
    {
        settings.SetActive(true);
    }

    public void SoundOnButton()
    {
        //ToDo
        Debug.Log("SoundOn");
    }

    public void SoundOffButton()
    {
        //ToDo
        Debug.Log("SoundOff");
    }

    public void MusicOnButton()
    {
        //ToDo
        Debug.Log("MusicOn");
    }

    public void MusicOffButton()
    {
        //ToDo
        Debug.Log("MusicOff");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
