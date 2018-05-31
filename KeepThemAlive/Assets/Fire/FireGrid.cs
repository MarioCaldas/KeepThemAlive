using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireGrid : MonoBehaviour {

    public Transform player;

    public bool displayGridGizmos;

    public Vector2 gridWorldSize;
    public float nodeRadius;
    Node[,] grid;
    public LayerMask objectsLayer;

    public LayerMask flamesLayer;


    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public static int xSpread, ySpread, _xSpread, _ySpread;

    public Transform sphere;


    //public GameObject objTeste;

    float x, y;


    float time;

    bool itsEmpty, inInFlames, isObject;

    public GameObject objFlames;
    public GameObject objFlamesBig;


    void Start ()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        //sphere = Resources.Load("FireStart") as Transform;

        CreateGrid();

        x = 9;
        y = 9;



        time = 15;

        Debug.Log(sphere);

        sphere.transform.position = new Vector3(251, 0, 188);

        sphere.transform.localScale = new Vector3(1, 1, 1);

        Instantiate(sphere);

    }

    // Update is called once per frame
    void Update () {

        x += 0.1f;
        y += 0.1f;

        //sphere.transform.localScale = new Vector3(x, 0, y);

        time -= Time.deltaTime;
        

        if(time <= 0)
        {
            sphere.transform.localScale += new Vector3(18, 18, 18);

            InstantiateFire();
            //InstantiateFire();
            //CreateGrid();
            time = 25;
        }

    }

    public void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);

                grid[x, y] = new Node(false, inInFlames, true, worldPoint, x, y);



            }
        }
    }

    void InstantiateFire()
    {
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;


        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);




                if (grid[x, y].isInFlames == false && grid[x, y].flamable == true)
                {
                    if (inInFlames = (Physics.CheckSphere(worldPoint, nodeRadius, flamesLayer)))
                    {
                        if(isObject = (Physics.CheckSphere(worldPoint, nodeRadius, objectsLayer)))
                        {
                            Instantiate(objFlamesBig, worldPoint + Vector3.up * -20, Quaternion.identity);

                        }
                        else
                        {
                            GameObject obj = Instantiate(objFlames, worldPoint + Vector3.up * -20, Quaternion.identity);

                           

                            grid[x, y] = new Node(false, true, false, worldPoint, x, y);

                        }
       

                    }

                }


            }
        }

    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null && displayGridGizmos)
        {
            //Node SphereNode = NodeFromWorldPoint(sphere.position);

            foreach (Node n in grid)
            {

                if (n.isInFlames)
                {
                
                    
                    Gizmos.color = Color.blue;
                }

                //else if (n.isEmpty)
                //{
                //    Gizmos.color = Color.white;
                //}
                else
                {
                    Gizmos.color = Color.white;
                    //Gizmos.color = Color.red;

                }




                //if(SphereNode == n)
                //{
                //    Gizmos.color = Color.cyan;
                //}
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - .1f));
            }
        }
    }
}
