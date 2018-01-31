using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public bool flagDebug = false;
    public float cellSize = 1;
    public int scene = 0;

    private MG_AudioManager audioManager;

    public void OnnStep()
    {
        RaycastHit hit;
        if (Physics.BoxCast(transform.position + Vector3.forward * (cellSize / 2), new Vector3(cellSize / 2, cellSize / 2, 0), -Vector3.forward, out hit, Quaternion.identity, cellSize))
        //if (Physics.Raycast(transform.position - Vector3.forward * (cellSize / 2), transform.forward, out hit, cellSize))
        {
            if (hit.collider.gameObject.tag == "Player")
            {
                audioManager.Play("NextLevel");
                audioManager.SaveScore();
                SceneManager.LoadScene(scene);
            }
        }
    }

    private void Start()
    {
        audioManager = FindObjectOfType<MG_AudioManager>();
    }

    void Update()
    {
        if (flagDebug) Debug.DrawRay(transform.position - Vector3.forward * (cellSize / 2), transform.forward * cellSize, Color.green);
    }
}
