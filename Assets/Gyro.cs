using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gyro : MonoBehaviour {

    private bool gyroscope_enabled;
    private Gyroscope gyro;
    Rigidbody2D igralec;
    public Text textGyro;

    // Use this for initialization
    void Start()
    {
        gyroscope_enabled = false;
        if (SystemInfo.supportsGyroscope)
        {
            gyro = Input.gyro;
            gyro.enabled = true;
            gyroscope_enabled = true;
        }
        igralec = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroscope_enabled)
        {
            float x, y, z;
            x = Mathf.Round(gyro.userAcceleration.x * 100f) / 100f;
            y = Mathf.Round(gyro.userAcceleration.y * 100f) / 100f;
            z = Mathf.Round(gyro.userAcceleration.z * 100f) / 100f;
            //igralec.velocity = new Vector2(igralec.velocity.x + y, igralec.velocity.y);

            textGyro.text = "X: " + z + "\n" + "Y: " + y + "\n" + "Z: " + z;
        }

    }
}
