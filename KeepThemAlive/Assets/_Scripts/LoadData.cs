using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour {

    public Transform school;

    private GameObject wreckedWall;


	void Awake ()
    {
        wreckedWall = Resources.Load("wreckedWallParent") as GameObject;

        for (int i = 0; i < SceneData.ChangedObjList.Count; i++)
        {
            for (int u = 0; u < school.childCount; u++)
            {
                if (SceneData.ChangedObjList[i] == school.transform.GetChild(u).name)
                {
                    Destroy(school.transform.GetChild(u).gameObject);
                }

               
            }

            
        }

        //for (int u = 0; u < school.childCount; u++)
        //{
        //    if (school.transform.GetChild(u).tag == "wall")
        //    {
                
        //        GameObject wWall = Instantiate(wreckedWall, school.transform.GetChild(u).position, school.transform.GetChild(u).rotation) as GameObject;
        //        wWall.transform.localScale = school.transform.GetChild(u).localScale;
        //    }
        //}
    }
	
	
}
