using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohessionBehaviour : FlockBehaviour {


    public override Vector3 CalcualteMove(FlockAgent agent, List<TransformAgent> context, Flock flock)
    {
        //If no neighbours, return no adjustment
        if(context.Count == 0)
        {
            return Vector3.zero;
        }

        //Add all points together and average
        Vector3 cohesionMove = Vector3.zero;

        foreach (TransformAgent item in context)
        {
            cohesionMove += item.transform.position;
        }
        cohesionMove /= context.Count;

        //create offset from agent position
        cohesionMove -= agent.transform.position;

        return cohesionMove;
    }

}
