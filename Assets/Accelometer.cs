using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Accelometer : MonoBehaviour {

    Rigidbody2D igralec;
    public Text gameOver;

    float x, y;
	// Use this for initialization
	void Start () {
        igralec = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Ovire.gameActive == true)
        {
            x = Input.acceleration.x * 0.5f;
            //y = Input.acceleration.y * 0.5f;
            igralec.velocity = new Vector2(igralec.velocity.x + x, igralec.velocity.y);
        }
        else
        {
            gameOver.text = "GAME OVER";
            if (Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ovira")
        {
            Ovire.gameActive = false;
            gameOver.text = "GAME OVER";
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "ovira") 
        {
            

            if(Ovire.gameActive == true)
                coll.isTrigger = false;
            Ovire.gameActive = false;
            gameOver.text = "GAME OVER";  
        }
    }
}
