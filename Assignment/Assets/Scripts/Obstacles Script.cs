using UnityEngine;
using System.Collections.Generic;

public class ObstacleScript : MonoBehaviour
{
    public GameObject obstaclePrefab; // The obstacle prefab
    public GameObject redOrbes;
    public GameObject BlueOrbes;
    public GameObject GreenOrbes;
    public int counter = 0;
   
    

    public Transform player;
    private List<GameObject> activeObjects = new List<GameObject>();

    private void Start()
    {
        for (int i = 10; i <=35; i+=5)
        {
            GenerateObstacles(i);

        }


    }

    private void Update()
    {
        if (Mathf.Floor(player.position.z+40) > counter)
        {
            print(player.position.z + 40 > counter);
            GenerateObstacles(player.position.z+40);
            deleteObjects();
 

        }
    }


    void GenerateObstacles(float z)
    {
        int rLane;
        int mLane;
        int lLane;

        rLane = Random.Range(3, 11);
        mLane = Random.Range(3, 11);
        lLane = Random.Range(3, 11);

        while ((rLane==5 || rLane == 6 || rLane == 7) && (mLane == 5 || mLane == 6 || mLane == 7) && (lLane == 5 || lLane == 6 || lLane == 7))

        {
            rLane = Random.Range(3, 11);
            mLane = Random.Range(3, 11);
            lLane = Random.Range(3, 11);


        }
        counter += 6;

        if (rLane <= 4)
        {
            activeObjects.Add(null);
        }
        else if (rLane == 5 || rLane == 6 || rLane==7)
        {
            GameObject Temp = Instantiate(obstaclePrefab, new Vector3(3.5f,0.5f,z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        else if (rLane ==8 )
        {
            GameObject Temp = Instantiate(redOrbes, new Vector3(3.5f, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        else if (rLane == 9)
        {
            GameObject Temp = Instantiate(BlueOrbes, new Vector3(3.5f, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        else if (rLane == 10)
        {
            GameObject Temp = Instantiate(GreenOrbes, new Vector3(3.5f, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }

        if (lLane <= 4)
        {
            activeObjects.Add(null);
        }

        else if (lLane == 5 || lLane == 6 || lLane == 7)
        {
            GameObject Temp = Instantiate(obstaclePrefab, new Vector3(-3.5f, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        else if (lLane == 8)
        {
            GameObject Temp = Instantiate(redOrbes, new Vector3(-3.5f, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        else if (lLane == 9)
        {
            GameObject Temp = Instantiate(BlueOrbes, new Vector3(-3.5f, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        else if (lLane == 10)
        {
            GameObject Temp = Instantiate(GreenOrbes, new Vector3(-3.5f, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        
        if(mLane <= 4)
        {
            activeObjects.Add(null);
        }
        else if (mLane == 5 || mLane == 6 || mLane == 7)
        {
            GameObject Temp = Instantiate(obstaclePrefab, new Vector3(0, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        else if (mLane == 8)
        {
            GameObject Temp = Instantiate(redOrbes, new Vector3(0, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        else if (mLane == 9)
        {
            GameObject Temp = Instantiate(BlueOrbes, new Vector3(0, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
        else if (mLane == 10)
        {
            GameObject Temp = Instantiate(GreenOrbes, new Vector3(0, 0.5f, z), transform.rotation); ;
            activeObjects.Add(Temp);
        }
         

    }
    private void deleteObjects()
    {
        if (activeObjects.Count>40)
        {
            for (int i = 0; i < 3; i++)
            {
                    Destroy(activeObjects[i]);
                    activeObjects.RemoveAt(i);
            }
        }
    }
}

