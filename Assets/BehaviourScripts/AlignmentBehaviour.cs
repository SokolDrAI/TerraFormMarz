using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{

    public override Vector3 CalcualteMove(FlockAgent agent, List<TransformAgent> context, Flock flock)
    {
        List<TransformAgent> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        //If no neighbours, maintaine current heading
        if (filteredContext.Count == 0)
        {
            return agent.transform.forward;
        }

        //Add all points together and average
        Vector3 alignmentMove = Vector3.zero;

        foreach (TransformAgent item in filteredContext)
        {
            alignmentMove += (Vector3)item.transform.forward;
        }
        alignmentMove /= filteredContext.Count;
        
        return alignmentMove;
    }
}
