using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

struct Score
{
    public string name { get; set; }
    public int points { get; set; }
};


public class HighScore : MonoBehaviour {

    public Text text;
    public Text text2;
    public Toggle toggle;
	// Use this for initialization
	void Start () {
        string[] array = PlayerPrefsX.GetStringArray("highscore");
        toggle.enabled = PlayerPrefsX.GetBool("sendOnServer");

        List<Score> seznam = new List<Score>();

        for (int i = 0; i < array.Length; i++)
        {
            string[] deli = array[i].Split('|');
            Score s = new Score();
            s.name = deli[0];
            s.points = Int32.Parse(deli[1]);
            seznam.Add(s);
        }
        

        List<Score> SortedList = seznam.OrderByDescending(o => o.points).ToList();

        for(int i=0; i<SortedList.Count && i<5; i++)
        {
            text.text += (i + 1).ToString() + ".  " + SortedList[i].name + "\n";
            text2.text += SortedList[i].points.ToString() + "\n";
        }

    }
	
	// Update is called once per frame
	void Update () {
        PlayerPrefsX.SetBool("sendOnServer", toggle.enabled);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("NewGame");
        }
    }
}
