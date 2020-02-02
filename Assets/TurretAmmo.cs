using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAmmo : MonoBehaviour
{
    const float maxHeight = 0f;
    const float minHeight = -1.58f;
    const int maxAmmo = 80;
    int currentAmmo = 0;
    float increment = (maxHeight- minHeight) / (float)maxAmmo;
    Flock[] flocks;
    float range = 30;
    int shotsAtOnce = 1;

    public Transform fireTarget;
    public Vector3 firePoint;
    float fireRate = .2f;
    float fireTimer = 0;

    const float gameplayMultiplier = 1.0f;

    FireBeam[] fireBeams;
    // Start is called before the first frame update
    void Start()
    {
        flocks = FindObjectsOfType<Flock>();
        fireBeams = GetComponentsInChildren<FireBeam>();
        if(fireTarget == null)
        {
            firePoint = transform.position + Vector3.up * 0.5f * transform.lossyScale.y;
        }
        else
        {
            firePoint = fireTarget.position;
        }
    }

    void Fire()
    {
        bool fired = false; ;
        for(int i = 0; i < shotsAtOnce; i++)
        {
            int randomStartIndex = Random.Range(0, flocks.Length);
            FlockAgent target = null;
            for(int j = 0; j < flocks.Length; j++)
            {

                int flockIndex = (j + randomStartIndex) % flocks.Length;
                target = flocks[flockIndex].GetAgentInRange(firePoint, range);
                if(target == null)
                {
                    continue;
                }
                else if (!fired)
                {
                    fired = true;
                    currentAmmo--;
                }
                fireBeams[i].Fire(firePoint, target.transform.position);
                flocks[flockIndex].KillAgent(target);
                
                break;
            }
            if (target == null)
                return;
        }
    }

    internal void Reload()
    {
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, minHeight + increment * currentAmmo, transform.localPosition.z);
        if(currentAmmo > 0)
        {
            fireTimer += Time.deltaTime * gameplayMultiplier;
            if(fireTimer > fireRate)
            {
                Fire();
                fireTimer -= fireRate;
            }
        }
    }
}
