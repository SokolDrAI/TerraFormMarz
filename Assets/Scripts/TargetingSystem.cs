using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem : MonoBehaviour
{
    public Transform[] tower;
    public float speed = 1f;

    public Transform target;

    private void Start()
    {

    }

    // Use this for initialization
    void Update()
    {
        target = GetClosestEnemy(tower);
    }

    public Transform GetClosestEnemy(Transform[] tower)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = this.transform.position;
        foreach (Transform potentialTarget in tower)
        {
            if (potentialTarget == null) { continue; }

            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

}
