using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidence")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{

    public override Vector3 CalcualteMove(FlockAgent agent, List<TransformAgent> context, Flock flock)
    {
        List<TransformAgent> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        //If no neighbours, return no adjustment
        if (filteredContext.Count == 0)
        {
            return Vector3.zero;
        }

        //Add all points together and average
        Vector3 avoidanecMove = Vector3.zero;
        int nAvoid = 0;
        foreach (TransformAgent item in filteredContext)
        {
            if(Vector3.SqrMagnitude(item.transform.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanecMove += agent.transform.position - item.transform.position;
            }
        }

        if(nAvoid > 0)
        {
            avoidanecMove /= nAvoid;
        }

        return avoidanecMove;
    }
}
