using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text PessText;
    public GameObject BClock;
    public GameObject RClock;
    public Text Btext;
    public Text Rtext;
    public float StartTimer;
    private float Min, Sec;
    public static int TotalPess = 0;
    private int PessSalvas;
    

    void Start ()
    {
        PessSalvas = 0;
        Min = 0;
        Sec = StartTimer;
    }
	
	void Update ()
    {
        Debug.Log("Peps: " + TotalPess);
        TimeCount();
        PessText.text = "Pessoas Salvas: " + PessSalvas +"/" + TotalPess;
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
                // piscapisca
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
