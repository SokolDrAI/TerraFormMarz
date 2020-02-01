using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behaviour/SteeredCohesion")]
public class SteeredCohessionBehaviour : FilteredFlockBehaviour {


    Vector3 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector3 CalcualteMove(FlockAgent agent, List<TransformAgent> context, Flock flock)
    {
        //If no neighbours, return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        //Add all points together and average
        Vector3 cohesionMove = Vector3.zero;
        List<TransformAgent> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (TransformAgent item in context)
        {
            cohesionMove += item.transform.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent position
        cohesionMove -= agent.transform.position;
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);

        return cohesionMove;
    }
}
