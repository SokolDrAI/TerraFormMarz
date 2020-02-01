using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behaviour/SteeredCohesion")]
public class SteeredCohessionBehaviour : FilteredFlockBehaviour {

    public float agentSmoothTime = 0.5f;

    public override Vector3 CalcualteMove(FlockAgent agent, List<TransformAgent> context, Flock flock)
    {
        List<TransformAgent> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        //If no neighbours, return no adjustment
        if (filteredContext.Count == 0)
        {
            return Vector3.zero;
        }

        //Add all points together and average
        Vector3 cohesionMove = Vector3.zero;

        foreach (TransformAgent item in filteredContext)
        {
            cohesionMove += item.transform.position;
        }
        cohesionMove /= filteredContext.Count;
        //create offset from agent position
        cohesionMove -= agent.transform.position;
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref agent.steerVelocity, agentSmoothTime);
        
        return cohesionMove;
    }
}
