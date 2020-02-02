using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/SameFlock")]
public class SameFlockFilter : ContextFilter
{
    static List<TransformAgent> filtered = new List<TransformAgent>();
    public override List<TransformAgent> Filter(FlockAgent agent, List<TransformAgent> original)
    {
        filtered.Clear();

        foreach (TransformAgent item in original)
        {
            FlockAgent itemAgent = item.agent;
            if (itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
            {
                filtered.Add(item);
            }
        }

        return filtered;
    }
}
