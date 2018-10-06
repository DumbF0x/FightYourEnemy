using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ovire : MonoBehaviour {

    public GameObject ovirePrefab;
    // Use this for initialization
    public Transform borderTop;
    public Transform borderBottom;
    public Transform borderLeft;
    public Transform borderRight;
    public Slider slider;
    public GameObject ozadje;
    public Text text;
    public Text text2;

    static public int time = 0;
    float povecevanje = 2;
    float povecevanjeOvir = 1;
    static public bool gameActive = true;

    static public int bullet = 1;

    bool fastMode = false;
    int fastModeLeft = 5;

    float tmpPovecevanje = 0;

    public Text stanjeIgre;
    Sprite sprite;


    void Start () {
        time = 0;
        povecevanje = 2;
        povecevanjeOvir = 1;
        gameActive = true;
        bullet = 1;
        fastMode = false;
        fastModeLeft = 5;
        tmpPovecevanje = 0;

        Invoke("Spawn", povecevanje);
        sprite = Sprite.Create(PhoneCamera.snap, new Rect(0, 0, PhoneCamera.snap.width, PhoneCamera.snap.height), new Vector2(0.5f, 0.5f), 1000);
        ovirePrefab.GetComponent<SpriteRenderer>().sprite = sprite;
        ovirePrefab.GetComponent<BoxCollider2D>().offset = new Vector2(0,0);
        ovirePrefab.GetComponent<BoxCollider2D>().size = new Vector3(sprite.bounds.size.x / transform.lossyScale.x, sprite.bounds.size.y / transform.lossyScale.y, sprite.bounds.size.z / transform.lossyScale.z);

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("NewGame");
        }
    }

    public void pause()
    {
        if(Time.timeScale != 0)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }

    void Spawn()
    {
        if(time % 10 == 0 && time > 0)
        {
            fastMode = true;
            tmpPovecevanje = povecevanje;
        }


        time++;
        if(bullet == 2)
        {
            slider.value = 0f;
            bullet = 0;
        }
        else if(bullet == 0)
        {
            if (slider.value == 1f)
                bullet = 1;
            else
                slider.value += 0.1f;
        }

        if(fastMode == true)
        {
            if(fastModeLeft == 0)
            {
                ozadje.GetComponent<SpriteRenderer>().color = Color.white;
                fastMode = false;
                fastModeLeft = 5;
                povecevanje = tmpPovecevanje;
                
            }
            else
            {
                fastModeLeft--;
                ozadje.GetComponent<SpriteRenderer>().color = Color.red;
                povecevanje = tmpPovecevanje * 0.5f;
            }
        }

        if(fastMode == false)
            povecevanje *= 0.99f;

        povecevanjeOvir *= 0.99f;

        float scale1 = (float)Random.Range(1f, 3f/povecevanje);
        ovirePrefab.transform.localScale = new Vector3(scale1, scale1, 0);

        // x position between left & right border
        int x = (int)Random.Range(borderLeft.position.x + 0.5f, borderRight.position.x - 0.5f);

        // y position between top & bottom border
        int y = (int)borderTop.position.y;

        // Instantiate the food at (x, y)
        Instantiate(ovirePrefab, new Vector2(x, y), Quaternion.identity); // default rotation

        stanjeIgre.text = "Time: " + time + "\n" + "Speed: " + povecevanje;
        if(gameActive)
            Invoke("Spawn", povecevanje);
    }
}
