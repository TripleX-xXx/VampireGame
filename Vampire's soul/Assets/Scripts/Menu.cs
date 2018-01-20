using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    public bool menuOnStart = true;
    private Canvas manuUI;

    void Start()
    {

        manuUI = (Canvas)GetComponent<Canvas>();
        Cursor.visible = manuUI.enabled;
        Cursor.lockState = CursorLockMode.Confined;
        if(!menuOnStart)manuUI.enabled = !manuUI.enabled;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            manuUI.enabled = !manuUI.enabled;
            Cursor.visible = manuUI.enabled;
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

    public void CountinueButton()
    {
        manuUI.enabled = !manuUI.enabled;
        Cursor.visible = manuUI.enabled;
    }

}
