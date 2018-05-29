using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {

    public Text PessText;
    public GameObject BClock;
    public GameObject RClock;
    public Text Btext;
    public Text Rtext;
    public float StartTimer;
    private float Min, Sec;
    public static int TotalPess = 10;
    public static int PessSalvas = 0;


    void Start()
    {
        Min = 0;
        Sec = StartTimer;
    }

    void Update()
    {
        TimeCount();
        PessText.text = "Pessoas Salvas: " + PessSalvas + "/" + TotalPess;
        Win();
    }

    void Win()
    {
        if (PessSalvas == 2)
        {
            print("YOU WIN");
        }

    }

    void TimeCount()
    {
        if (StartTimer > 0)
        {
            StartTimer -= Time.deltaTime;
            Min = ((int)StartTimer / 60);
            Sec = (StartTimer % 60);
            Btext.text = Min.ToString("f0") + ":" + Sec.ToString("f0");
            Rtext.text = Min.ToString("f0") + ":" + Sec.ToString("f0");

            if (StartTimer <= 30)
            {
                // pisca pisca
                if ((int)Sec % 2 == 0)
                {
                    BClock.SetActive(false);
                    Btext.gameObject.SetActive(false);
                    RClock.SetActive(true);
                    Rtext.gameObject.SetActive(true);
                }
                else
                {
                    BClock.SetActive(true);
                    Btext.gameObject.SetActive(true);
                    RClock.SetActive(false);
                    Rtext.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            // Acabou o tempo!!!
        }
    }
}
