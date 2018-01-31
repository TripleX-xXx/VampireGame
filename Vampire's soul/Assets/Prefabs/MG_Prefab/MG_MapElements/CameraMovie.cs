using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovie : MonoBehaviour {

    public GameObject hud;
    public Camera[] cams;
    private int cam = 0;

    private void Start()
    {
        foreach(Camera c in cams)
        {
            c.enabled = false;
        }
        cams[0].enabled = true;
    }

    void Update () {
        if (Input.GetKeyDown("h")) hud.SetActive(!hud.activeSelf);
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            cams[cam].enabled = false;
            cam++;
            if (cam >= cams.Length) cam = 0;
            cams[cam].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            cams[cam].enabled = false;
            cam--;
            if (cam < 0) cam = cams.Length-1;
            cams[cam].enabled = true;
        }
    }
}
