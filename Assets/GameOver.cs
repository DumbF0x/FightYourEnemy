using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System;
using System.Text;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CancelButton()
    {
        Ovire.gameActive = true;
        SceneManager.LoadScene("NewGame");
    }

    public void RestartButton()
    {
        Ovire.gameActive = true;
        SceneManager.LoadScene("scene1");
    }



    public void SaveButton(InputField input)
    {
        string name = input.text;

        int score = Ovire.time;


        string[] array = PlayerPrefsX.GetStringArray("highscore");
        string[] array2 = new string[array.Length + 1];
        array.CopyTo(array2, 0);
        array2[array2.Length - 1] = name + "|" + score.ToString();

        PlayerPrefsX.SetStringArray("highscore", array2);


        if (PlayerPrefsX.GetBool("sendOnServer"))
        {
            const int port = 1122;
            const string IP = "127.0.0.1";
            const int max_msg_size = 1024;


            Debug.Log("Odjemalec\n");

            bool active = true;

            Debug.Log("Vnesite ukaz: ");
            string ukaz = "D|" + name + "|" + score.ToString();

            try
            {
                TcpClient client = new TcpClient(IP, port);  //connect
                using (NetworkStream stream = client.GetStream()) //stream za izmenjavo sporocil
                {
                    try
                    {
                        byte[] buff = Encoding.UTF8.GetBytes(ukaz.ToCharArray(), 0, ukaz.Length); //spremenimo sporocilo v ustrezni format - byte[]
                        stream.Write(buff, 0, buff.Length); //posljemo ukaz strezniku
                        Debug.Log("Poslal sem ukaz: " + ukaz);
                    }
                    catch (Exception e)
                    {
                        Debug.Log("Napaka pri posiljanju!\n" + e.Message + "\n" + e.StackTrace);
                    }


                    string sporocilo = "";
                    try //poskusimo prebrati stream od klienta
                    {
                        byte[] buf = new byte[max_msg_size];
                        int count = stream.Read(buf, 0, buf.Length); //beremo stream v buffer, na offsetu 0 in dolžine našega bufferja
                        sporocilo = Encoding.UTF8.GetString(buf, 0, count);
                    }
                    catch (Exception e) //branje ni uspelo
                    {
                        Debug.Log("Napaka pri sprejemanju!\n" + e.Message + "\n" + e.StackTrace);
                    }

                    string odgovor = "";

                    if (sporocilo != "")
                    {
                        string[] protocol = sporocilo.Split('|'); //razredelimo sporocilo glede na dodeljen znak

                        switch (protocol[0])
                        {
                            case "D":
                                {
                                    odgovor = protocol[1];
                                    break;
                                }

                        }
                    }
                    Debug.Log("Odgovor streznika: " + odgovor);

                }
            }
            catch (Exception e)
            {
                Debug.Log("Napaka!\n" + e.Message + "\n" + e.StackTrace);
            }

            Ovire.gameActive = true;
            SceneManager.LoadScene("NewGame");
        }
    }
}
