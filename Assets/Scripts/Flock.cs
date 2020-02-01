using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();

    static Dictionary<int, TransformAgent> agentCache = new Dictionary<int, TransformAgent>();
    public FlockBehaviour behaviour;

    public Vector3 targetPosition;
    public float targetRadius = 25;

    [Range(10, 500)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 20f)]
    public float neighbourRadious = 15f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    public bool isScattering = false;
    public float scatterTimer = 2f;
    
    // Use this for initialization
    void Awake ()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadious * neighbourRadious;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                FindPos(),
                Quaternion.Euler(Vector3.forward * Random.Range(0,360f)),
                transform
                );
            newAgent.name = "Agent" + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
	    foreach(FlockAgent agent in agents)
        {
            List<TransformAgent> context = GetNearbyObjects(agent);
            Vector3 move = behaviour.CalcualteMove(agent, context, this);

            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.MoveMe(move);
        }
        if (isScattering)
        {
            scatterTimer -= Time.deltaTime;
            if (scatterTimer < 0)
            {
                isScattering = false;
                scatterTimer = 2f;
            }
        }


    }

    public Vector3 FindPos()
    {
        Vector3 pos = Random.insideUnitCircle;
        pos = new Vector3(pos.x, 0, pos.y); // Lay the circle down flat on the ground
        pos = pos.normalized * (AgentDensity + pos.magnitude * (startingCount - AgentDensity));
        return pos;
    }

    List<TransformAgent> GetNearbyObjects(FlockAgent agent)
    {
        List<TransformAgent> context = new List<TransformAgent>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadious);

        foreach(Collider c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                if(!agentCache.ContainsKey(c.GetInstanceID()))
                {
                    agentCache[c.GetInstanceID()] = new TransformAgent() { transform = c.transform, agent = c.GetComponent<FlockAgent>() };
                }
                context.Add(agentCache[c.GetInstanceID()]);
            }
        }
        return context;
    }
}
