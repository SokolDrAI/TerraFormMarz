using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behaviour/Scatter")]
public class Scatter : FilteredFlockBehaviour
{
    
    public float agentSmoothTime = 0.5f;

    public override Vector3 CalcualteMove(FlockAgent agent, List<TransformAgent> context, Flock flock)
    {

        if(flock.isScattering == true)
        {
            List<TransformAgent> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

            //If no neighbours, return no adjustment
            if (filteredContext.Count == 0)
            {
                return Vector3.zero;
            }

            //Add all points together and average
            Vector3 scatterMove = Vector3.zero;

            foreach (TransformAgent item in filteredContext)
            {
                scatterMove += item.transform.position;
            }
            scatterMove /= filteredContext.Count;
            //create offset from agent position
            scatterMove += agent.transform.position;
            scatterMove = Vector3.SmoothDamp(agent.transform.forward, scatterMove, ref agent.steerVelocity, agentSmoothTime);

            return scatterMove;
        }
        else
        {
            return Vector3.zero;
        }
     
    }
}
