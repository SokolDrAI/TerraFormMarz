using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidence")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public float avoidRadiusWeight = 1;
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
            Vector3 myPos = agent.transform.position;
            Vector3 otherPos = item.transform.position;

            myPos.y = 0;
            otherPos.y = 0;
            if(Vector3.SqrMagnitude(otherPos - myPos) < flock.SquareAvoidanceRadius * avoidRadiusWeight)
            {
                nAvoid++;
                avoidanecMove += (myPos - otherPos) ;
            }
        }

        if(nAvoid > 0)
        {
            avoidanecMove /= nAvoid;
        }

        return avoidanecMove;
    }
}
