using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Obstacle")]
public class AvoidObstcale : ContextFilter
{


    public override List<TransformAgent> Filter(FlockAgent agent, List<TransformAgent> original)
    {
        List<TransformAgent> filtered = new List<TransformAgent>();

        foreach (TransformAgent item in original)
        {
            if (item.transform.tag.Equals("Obstacle"))
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
