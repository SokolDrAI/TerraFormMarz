using UnityEngine;
using System.Collections.Generic;
using System;

public class Tower : MonoBehaviour
{
    public float buildValue { get; private set; }
    bool isBuilding = false;
    float boostTime = 0;
    Buildable objectToBuild;
    ParticleSystem particles;
    List<Tower> otherTowers;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }


    public void Boost(float amount)
    {
        boostTime += amount;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Renderer>().material.color = isBuilding ? Color.blue : Color.black;
        var emission = particles.emission;
        emission.enabled = boostTime > 0;

        if(isBuilding)
        {
            if(boostTime > 0)
            {
                boostTime = Mathf.Clamp01(boostTime - Time.deltaTime);
                buildValue += Time.deltaTime * 2;
            }
            else
            {
                buildValue += Time.deltaTime;
            }

            if(buildValue > 5)
            {
                buildValue = 0;
                isBuilding = false;
                GameObject.Instantiate(objectToBuild, transform.position + transform.forward * 15, Quaternion.identity);
            }
        }


    }

    internal void Enqueue(Buildable buildObject)
    {
        if (isBuilding)
            return;
        isBuilding = true;
        this.objectToBuild = buildObject;

    }
}
