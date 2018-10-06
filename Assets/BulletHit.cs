using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour {

    public GameObject BulletPrefab;
    public GameObject player;
    // Use this for initialization

    bool wait = false;
    int dovoljeno = 0;


    public Renderer rend;

    void Start()
    {
        rend = player.GetComponent<Renderer>();
        //Invoke("Spawn", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == true && Ovire.bullet == 1 && Time.timeScale != 0)
        {
            Ovire.bullet = 2;
            wait = true;
            Invoke("Spawn", 0);
            dovoljeno = 5;
        }
     
    }

    void TaskOnClick()
    {
        //Output this to console when the Button is clicked
        Debug.Log("You have clicked the button!");
    }

    void Spawn()
    {
        Vector3 center = rend.bounds.center;
        // x position between left & right border
        int x = (int)center.x;
       

        // y position between top & bottom border
        int y = (int)(player.transform.position.y + 0.5f);
        y = (int)(center.y + 0.5f);

        // Instantiate the food at (x, y)
        Instantiate(BulletPrefab, new Vector2(x, y), Quaternion.identity); // default rotation
        wait = false;
    }
}
