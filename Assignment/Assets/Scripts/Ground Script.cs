using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour
{
    public GameObject groundPreFab;
    public float zIncrement = 10.0f; // Set the amount to increment the Z position
    public float groundLength = 10.0f;
    public int maxPrefabs = 2; // Set the maximum number of prefabs to keep
    private List<GameObject> prefabs = new List<GameObject>();

    public Transform Player;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < maxPrefabs; i++)
        {
            generateGround();
        }
        print("Ground Generated");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.position.z>zIncrement - (maxPrefabs * groundLength))
            {
            generateGround();
            deleteGround();
        }
    }
    public void generateGround()
    {
            GameObject temp = Instantiate(groundPreFab, transform.forward * zIncrement, transform.rotation);
            prefabs.Add(temp);
            zIncrement += groundLength;
        
    }

    private void deleteGround()
    {
        Destroy(prefabs[0]);
        prefabs.RemoveAt(0);
    }
}
