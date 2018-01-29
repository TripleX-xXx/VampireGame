using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    public GameObject about;
    private MG_AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<MG_AudioManager>();
        menu.SetActive(true);
        settings.SetActive(false);
        about.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            menu.SetActive(true);
            settings.SetActive(false);
            about.SetActive(false);
        }
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void SettingButton()
    {
        menu.SetActive(false);
        settings.SetActive(true);
        about.SetActive(false);
    }

    public void AboutButton()
    {
        menu.SetActive(false);
        settings.SetActive(false);
        about.SetActive(true);
    }

    public void FullScreenButton()
    {
        Screen.fullScreen = !Screen.fullScreen;
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

}
