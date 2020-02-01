using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/SameFlock")]
public class SameFlockFilter : ContextFilter
{
    public override List<TransformAgent> Filter(FlockAgent agent, List<TransformAgent> original)
    {
        List<TransformAgent> filtered = new List<TransformAgent>();

        foreach (TransformAgent item in original)
        {
            FlockAgent itemAgent = item.agent;
            if(itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
