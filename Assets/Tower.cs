using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    public float repairValue { get; private set; }
    ParticleSystem particles;
    List<Tower> otherTowers;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        otherTowers = new List<Tower>(FindObjectsOfType<Tower>());
        otherTowers.Remove(this);
        repairValue = 1.2f/(otherTowers.Count+1);
    }


    public void Repair(float amount)
    {
        var newRepairValue = Mathf.Clamp01(repairValue + amount);
        var change = newRepairValue - repairValue;
        repairValue = newRepairValue;
        float otherTotal = 0;

        foreach(var tower in otherTowers)
        {
            otherTotal += tower.repairValue;
        }
        if (otherTotal == 0)
            return;
        foreach(var tower in otherTowers)
        {
            tower.repairValue -= change * (tower.repairValue / otherTotal);
        }

    }

    // Update is called once per frame
    void Update()
    {
        Color color = Color.Lerp(Color.black, Color.blue, repairValue);
        GetComponent<Renderer>().material.color = color;
        var emission = particles.emission;
        emission.enabled = repairValue >0.5f;
    }
}
