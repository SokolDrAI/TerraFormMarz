﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour {

    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }




	// Use this for initialization
	void Start () {
        agentCollider = GetComponent<Collider>();
	}

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void MoveMe(Vector3 velocity)
    {
        transform.forward = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
