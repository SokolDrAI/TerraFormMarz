using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSpawnManager : MonoBehaviour
{

    //Flock count
    public int numberOfFlocks;
    //Flocks
    public List<GameObject> ourFlocks; 
    //Count of ships in each flocks
    public int howManyShipsInFlock;

    //Upper and lower limit of ships
    public int maxShipCount;
    public int minShipCount;

    //number of ships to spawn
    public int numberOfShipsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        //Get a count of each of flocks in scene
        numberOfFlocks = GameObject.FindGameObjectsWithTag("Flock").Length;

        ourFlocks.AddRange(GameObject.FindGameObjectsWithTag("Flock"));
        Debug.Log(ourFlocks);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
