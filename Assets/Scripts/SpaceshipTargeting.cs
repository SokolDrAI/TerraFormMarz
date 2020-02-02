using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipTargeting : MonoBehaviour
{

    private LineRenderer lineRenderer;
    private Transform myPosition;
    public List<Shield> tower;// list of towers
    public List<Transform> towertransforms;
    public Transform target;
    float damageAmount = 0.0015f;
    // Start is called before the first frame update
    void OnEnable()
    {
        lineRenderer = GetComponent<LineRenderer>();
        tower = new List<Shield>(FindObjectsOfType<Shield>());
        tower.RemoveAll(s => s.ammoTower != null);
        for (int i = 0; i < tower.Count; i++)
        {
            towertransforms.Add(tower[i].gameObject.transform);
        }
    }

    private void Update()
    {
        target = GetClosestEnemy(towertransforms);
        //where tower damage should be
        lineRenderer.enabled = target != null;
        if (target != null)
        {
            Shield s = target.GetComponent<Shield>();
            if (s != null)
                s.Damage(damageAmount * Time.deltaTime);


            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, target.transform.position);
            
        }
    }

    public Transform GetClosestEnemy(List<Transform> tower)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = 200;
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
