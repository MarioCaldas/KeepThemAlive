using UnityEngine;

public class GenerateWreckage : MonoBehaviour
{
    public GameObject rock;
    public GameObject brick;
    public GameObject damagedPlank;

    public float minCubeSize;
    public float maxCubeSize;
    public float minCapSize;
    public float maxCapSize;
    public Texture2D noiseImage;
    public float zoneSize;
    public float objsDensity;
    GameObject newGameObj;

    private float baseDensity = 5.0f;


    void Awake()
    {
        Generate();
    }

    public void Generate()
    {
        for (int y = 0; y < zoneSize; y++)
        {
            for (int x = 0; x < zoneSize; x++)
            {
                float probability = noiseImage.GetPixel(x, y).r / (baseDensity / objsDensity);

                if (CanPlace(probability))
                {
                    float cubeSize = Random.Range(minCubeSize, maxCubeSize);

                    float capSize = Random.Range(minCapSize, maxCapSize);

                    RandomObject(Random.Range(1, 4));

                    //if (newGameObj.tag != "Stone")
                    //{
                        newGameObj.transform.localScale = Vector3.one * cubeSize;
                        newGameObj.transform.position = new Vector3(x + 180, 40, y + 155);
                        newGameObj.transform.rotation = Quaternion.Euler(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180)) ;

                        newGameObj.transform.parent = transform;


                    //}
                    //else
                    //{
                    //    newGameObj.transform.localScale = Vector3.one * rockSize;
                    //    newGameObj.transform.position = new Vector3(x, 0.55f, y);
                    //    newGameObj.transform.rotation = Random.rotation;
                    //    newGameObj.transform.parent = transform;
                    //}
                }
            }
        }
    }
    public void RandomObject(int random)
    {
        if (random == 1)
        {
            newGameObj = Instantiate(rock);
        }
        if (random == 2)
        {
            newGameObj = Instantiate(brick);
        }
        if (random == 3)
        {
            newGameObj = Instantiate(damagedPlank);
        }

    }

    public bool CanPlace(float chance)
    {
        if (Random.Range(0.0f, 1.0f) <= chance)
        {
            return true;
        }
        return false;
    }
}
