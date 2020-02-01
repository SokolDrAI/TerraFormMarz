using UnityEngine;

public class Tower : MonoBehaviour
{
    public float repairValue { get; private set; }
    ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }


    public void Repair(float amount)
    {
        repairValue = Mathf.Clamp01(repairValue + amount);
    }

    // Update is called once per frame
    void Update()
    {
        Color color = Color.Lerp(Color.red, Color.green, repairValue);
        GetComponent<Renderer>().material.color = color;
        var emission = particles.emission;
        emission.enabled = repairValue == 1;
    }
}
