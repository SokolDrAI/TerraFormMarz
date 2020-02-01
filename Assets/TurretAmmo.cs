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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, minHeight + increment * currentAmmo, transform.localPosition.z);

        if(rechargeTower.repairValue > 0.5f && currentAmmo < maxAmmo)
        {
            rechargeCounter += Time.deltaTime * rechargeTower.repairValue * 1;
            if(rechargeCounter > 1)
            {
                rechargeCounter -= 1;
                currentAmmo++;
            }
        }
    }
}
