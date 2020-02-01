using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidence")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{

    public override Vector3 CalcualteMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        //If no neighbours, return no adjustment
        if (context.Count == 0)
        {
            return Vector3.zero;
        }

        //Add all points together and average
        Vector3 avoidanecMove = Vector3.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in context)
        {
            if(Vector3.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanecMove += agent.transform.position - item.position;
            }
        }

        if(nAvoid > 0)
        {
            avoidanecMove /= nAvoid;
        }

        return avoidanecMove;
    }
}
