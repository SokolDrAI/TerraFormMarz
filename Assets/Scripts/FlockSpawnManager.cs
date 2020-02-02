using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockSpawnManager : MonoBehaviour
{
    public int numberOfFlocks;
    public List<Flock> ourFlocks;
    public int[] howManyShipsInEachFlock;

    public int maxShipCount;
    public int minShipCount;

    public int numberOfShipsToSpawn;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
