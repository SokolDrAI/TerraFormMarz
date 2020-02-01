using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/StayInradiusBehaviour")]
public class StayInradiusBehaviour : FlockBehaviour
{
    public Vector3 center;
    public float radius = 15f;

    public override Vector3 CalcualteMove(FlockAgent agent, List<TransformAgent> context, Flock flock)
    {
        Vector3 centerOffset = flock.targetPosition - agent.transform.position;
        float t = centerOffset.magnitude / flock.targetRadius;

        if(t < 0.9f)
        {
            return Vector3.zero;
        }

        return centerOffset * t * t;
    }
}
