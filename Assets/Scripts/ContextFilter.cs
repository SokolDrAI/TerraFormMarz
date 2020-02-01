using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContextFilter : ScriptableObject
{
    public abstract List<TransformAgent> Filter(FlockAgent agent, List<TransformAgent> original);
}
