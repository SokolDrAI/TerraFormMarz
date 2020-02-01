using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{

    public override Vector3 CalcualteMove(FlockAgent agent, List<TransformAgent> context, Flock flock)
    {
        //If no neighbours, maintaine current heading
        if (context.Count == 0)
        {
            return agent.transform.forward;
        }

        //Add all points together and average
        Vector3 alignmentMove = Vector3.zero;
        List<TransformAgent> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (TransformAgent item in context)
        {
            alignmentMove += (Vector3)item.transform.forward;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
