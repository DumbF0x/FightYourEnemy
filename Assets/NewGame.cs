using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NewGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefsX.SetBool("sendOnServer", true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeScene(string scene)
    {
        if (scene == "quit")
            Application.Quit();
        else
            SceneManager.LoadScene(scene);
    }
}
