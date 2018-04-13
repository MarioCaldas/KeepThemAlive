using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour {

    private int cost;
    private string Tag;
    private GameObject Object;
    

    #region Get's
    public GameObject GetGO()
    {
        return Object;
    }

    public int GetCost()
    {
        return cost;
    }

    public string GetTag()
    {
        return Tag;
    }
    #endregion

    #region Set's
    public void SetGO(GameObject obj)
    {
        Object = obj;
    }

    public void SetCost(int price)
    {
        cost = price;
    }

    public void SetTag(string WordTag)
    {
        Tag = WordTag;
    }
    #endregion
}
