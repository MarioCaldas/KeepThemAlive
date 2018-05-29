using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScenes : MonoBehaviour {

    int sceneCount;


	// Use this for initialization
	void Start ()
    {

        sceneCount = 0;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void NextScene(int levelIndex)
    {
        sceneCount++;
        Debug.Log("oii");
        Application.LoadLevel(1);

    }
}
