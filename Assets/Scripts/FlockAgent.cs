using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour {

    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }

    public bool scatterCheck = false;
    public Vector3 steerVelocity = Vector3.zero;
    public LayerMask agentLayer;

    // Use this for initialization
    void Start () {
        agentCollider = GetComponent<Collider>();
        agentLayer = this.gameObject.layer;

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
