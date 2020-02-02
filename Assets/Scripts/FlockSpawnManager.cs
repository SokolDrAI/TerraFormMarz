using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSpawnManager : MonoBehaviour
{

    //Flock count
    public int numberOfFlocks;
    //Flocks
    public List<Flock> ourFlocks; 
    //Count of ships in each flocks
    public int[] howManyShipsInFlock;

    //Upper and lower limit of ships
    public int minShipCount;

    //number of ships to spawn
    public int numberOfShipsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        numberOfShipsToSpawn = 5;
        minShipCount = 45;
        //Get a count of each of flocks in scene
        numberOfFlocks = GameObject.FindGameObjectsWithTag("Flock").Length;

        //Get the object with flock on it add to list
        ourFlocks = new List<Flock>(FindObjectsOfType<Flock>());
        howManyShipsInFlock = new int[numberOfFlocks];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CountShips();
        }
    }

    public void CountShips()
    {
        for (int i = 0; i < ourFlocks.Count; i++)
        { 
            howManyShipsInFlock[i] = (ourFlocks[i].agents.Count);
            if(howManyShipsInFlock[i] < minShipCount)
            {
                for (int z = 0; z < numberOfShipsToSpawn; z++)
                {
                    ourFlocks[i].CreateNewShips();
                }
            }
            Debug.Log(howManyShipsInFlock[i]);
        }
        minShipCount -= 5;
        numberOfShipsToSpawn += 5;
    }
}
