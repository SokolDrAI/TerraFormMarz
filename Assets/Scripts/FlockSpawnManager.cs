using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSpawnManager : MonoBehaviour
{
    //list of possible targets to go to
    public Transform[] possibleTargetsToMovetoo;

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

    float spawnTimer = spawnRate;
    const float spawnRate = 1;

    float bigWaveTimer = 0;
    const float bigWaveRate = 4;

    // Start is called before the first frame update
    void Start()
    {
        numberOfShipsToSpawn = 3;
        minShipCount = 45;
        //Get a count of each of flocks in scene
        numberOfFlocks = GameObject.FindGameObjectsWithTag("Flock").Length;

        //Get the object with flock on it add to list
        ourFlocks = new List<Flock>(FindObjectsOfType<Flock>());
        howManyShipsInFlock = new int[numberOfFlocks];
        changeTarget();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        bigWaveTimer += Time.deltaTime;
        if (spawnTimer < 0 )
        {
            
            CountShips();
            spawnTimer = spawnRate;
        }
    }

    public void CountShips()
    {
        float amountToSpawn =  numberOfShipsToSpawn;
        if(bigWaveTimer > bigWaveRate)
        {
            bigWaveTimer -= bigWaveRate;
            amountToSpawn *= 3;
            changeTarget();
        }
        for (int i = 0; i < ourFlocks.Count; i++)
        { 
            howManyShipsInFlock[i] = (ourFlocks[i].agents.Count);

            for (int z = 0; z < amountToSpawn; z++)
            {
                ourFlocks[i].CreateNewShips();
            }
            
        }
    }

    public void changeTarget()
    {
        
        foreach(var flock in ourFlocks)
        {
            int SelectrandomTarget = Random.Range(0, possibleTargetsToMovetoo.Length);
            for (int i = 0; i < possibleTargetsToMovetoo.Length; i++)
            {
                if (SelectrandomTarget == i)
                {
                    flock.targetPosition = possibleTargetsToMovetoo[i].transform.position;
                }
            }
        }

    }
}
