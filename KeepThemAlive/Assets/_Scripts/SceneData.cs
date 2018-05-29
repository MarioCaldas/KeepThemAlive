using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneData : MonoBehaviour
{
    public GameObject envoriment;

    private void Start()
    {
        DontDestroyOnLoad(envoriment);
    }

}