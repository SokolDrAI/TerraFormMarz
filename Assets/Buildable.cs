using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    public enum ObjectType
    {
        Ammo,
        Part1,
        Part2,
        Part3,
        Part4
    }
    public ObjectType objectType = ObjectType.Ammo;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Time.deltaTime * 50, 0, Space.World);   
    }
}
