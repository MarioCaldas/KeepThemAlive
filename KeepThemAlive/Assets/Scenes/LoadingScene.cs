using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour {

    float time = 7f;

    void Start () {
        NextScene();

    }
	
	// Update is called once per frame
	void Update () {

        time -= Time.deltaTime;
        
        if(time <= 0)
        {
            //NextScene();
        }

	}

    public void NextScene()
    {
        Debug.Log("oii");
        Application.LoadLevel(2);

    }
}
