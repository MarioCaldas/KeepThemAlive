using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour {

    public Transform school;

	void Awake ()
    {
        for (int i = 0; i < SceneData.ChangedObjList.Count; i++)
        {
            Instantiate(SceneData.ChangedObjList[i], SceneData.ChangedObjList[i].transform.localPosition, SceneData.ChangedObjList[i].transform.localRotation, school);
        }
	}
	
	
}
