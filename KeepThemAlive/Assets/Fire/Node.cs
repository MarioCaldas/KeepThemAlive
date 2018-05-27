using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node{

    //public bool isEmpty;
    public bool flamable;
    public bool objectBool;

    public bool isInFlames;

    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public Node parent;


    public Node(bool _isObj,bool _isInFlames, bool _flamable, Vector3 _worldPos, int _gridX, int _gridY)
    {
        objectBool = _isObj;
        isInFlames = _isInFlames;
        flamable = _flamable;
        //isEmpty = _isObj;
        worldPosition = _worldPos;
        gridX = _gridX;
        gridY = _gridY;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
