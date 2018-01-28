using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public GameObject menu;
    public GameObject settings;
    public GameObject about;

    public GameObject soundOn;
    public GameObject soundOff;
    public GameObject musicOn;
    public GameObject musicOff;


    void Start()
    {
        menu.SetActive(true);
        settings.SetActive(false);
        about.SetActive(false);

        soundOn.SetActive(true);
        soundOff.SetActive(false);
        musicOn.SetActive(true);
        musicOff.SetActive(false);
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
        //ToDo
        soundOff.SetActive(false);
        soundOn.SetActive(true);
        Debug.Log("SoundOn");
    }

    public void SoundOffButton()
    {
        //ToDo
        soundOn.SetActive(false);
        soundOff.SetActive(true);
        Debug.Log("SoundOff");
    }

    public void MusicOnButton()
    {
        //ToDo
        musicOff.SetActive(false);
        musicOn.SetActive(true);
    }

    public void MusicOffButton()
    {
        //ToDo
        musicOn.SetActive(false);
        musicOff.SetActive(true);
    }

}
