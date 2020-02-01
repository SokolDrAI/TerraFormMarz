using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAmmo : MonoBehaviour
{
    const float maxHeight = 0f;
    const float minHeight = -1.58f;
    public Tower rechargeTower;
    private float rechargeCounter = 0;
    const int maxAmmo = 20;
    int currentAmmo = 10;
    float increment = (maxHeight- minHeight) / (float)maxAmmo;
    Flock[] flocks;
    float range = 100;

    float fireRate = 1.2f;
    float fireTimer = 0;

    float gameplayMultiplier = 1.0f;

    FireBeam[] fireBeams;
    // Start is called before the first frame update
    void Start()
    {
        flocks = FindObjectsOfType<Flock>();
        fireBeams = GetComponentsInChildren<FireBeam>();
    }

    void Fire()
    {
        bool fired = false; ;
        for(int i = 0; i < flocks.Length; i++)
        {
            int randomStartIndex = Random.Range(0, flocks.Length);
            FlockAgent target = null;
            for(int j = 0; j < flocks.Length; j++)
            {

                int flockIndex = (j + randomStartIndex) % flocks.Length;
                target = flocks[flockIndex].GetAgentInRange(transform.position, range);
                if(target == null)
                {
                    continue;
                }
                else if (!fired)
                {
                    fired = true;
                    currentAmmo--;
                }
                fireBeams[i].Fire(transform.position + Vector3.up * 0.5f * transform.lossyScale.y, target.transform.position);
                flocks[flockIndex].KillAgent(target);
                
                break;
            }
            if (target == null)
                return;
        }
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
        if(rechargeTower.repairValue > 0.5f && currentAmmo < maxAmmo)
        {
            rechargeCounter += Time.deltaTime * rechargeTower.repairValue * 2 * gameplayMultiplier;
            if(rechargeCounter > 1)
            {
                rechargeCounter -= 1;
                currentAmmo++;
            }
        }
    }
}
