using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu2 : MonoBehaviour {

    public GameObject menu;
    public GameObject settings;
    private MG_AudioManager audioManager;

    // Use this for initialization
    void Start ()
    {
        audioManager = FindObjectOfType<MG_AudioManager>();
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
        audioManager.Sounds(true);
        //Debug.Log("SoundOn");
    }

    public void SoundOffButton()
    {
        audioManager.Sounds(false);
        //Debug.Log("SoundOff");
    }

    public void MusicOnButton()
    {
        audioManager.Music(true);
        //Debug.Log("MusicOn");
    }

    public void MusicOffButton()
    {
        audioManager.Music(false);
        //Debug.Log("MusicOff");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
