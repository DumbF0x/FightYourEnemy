using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhoneCamera : MonoBehaviour {
    private bool camera_available;
    private WebCamTexture phoneCamera;
    //private Texture defaulBack;

    public RawImage background;
    public AspectRatioFitter fitter;
    static public Texture2D snap = null;

    int counter = 0;

    // Use this for initialization
    void Start () {
        //defaulBack = background.texture;
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            camera_available = false;
            return;
        }
         for (int i = 0; i < devices.Length; i++)
            {
                if (!devices[i].isFrontFacing)
                    phoneCamera = new WebCamTexture(devices[i].name, Screen.width, Screen.height);

            }

        if (phoneCamera == null)
            return;

        phoneCamera.Play();
        background.texture = phoneCamera;

        camera_available = true;

	}
	
	// Update is called once per frame
	void Update () {
        if (!camera_available)
            return;
        fitter.aspectRatio = (float)phoneCamera.width / (float)phoneCamera.height;

        float scale = phoneCamera.videoVerticallyMirrored ? -1f : 1f;
        background.rectTransform.localScale = new Vector3(1f, scale, 1f);

        background.rectTransform.localEulerAngles = new Vector3(0, 0, -phoneCamera.videoRotationAngle);

        if (Input.GetMouseButtonDown(0) == true)
        {
            snap = new Texture2D(phoneCamera.width, phoneCamera.height);
            snap.SetPixels(phoneCamera.GetPixels());
            snap.Apply();

            //System.IO.File.WriteAllBytes("./photoEnemy" + counter.ToString() + ".png", snap.EncodeToPNG());
            //++counter;

            phoneCamera.Stop();

            SceneManager.LoadScene("scene1");
        }
    }

    

}
