using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Obstacle")]
public class TagFilter : ContextFilter
{
    public HashSet<string> tags = new HashSet<string> { "Player","Obstacles"};

    public override List<TransformAgent> Filter(FlockAgent agent, List<TransformAgent> original)
    {
        List<TransformAgent> filtered = new List<TransformAgent>();

        foreach (TransformAgent item in original)
        {
            if (tags.Contains(item.tag))
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
