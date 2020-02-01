using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public float health { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        health = 1;   
    }

    public void Damage(float amount)
    {
        health -= amount;
    }

    void Update()
    {
        Gradient gradient = new Gradient();
        GradientColorKey r = new GradientColorKey(Color.red, 0);
        GradientColorKey y = new GradientColorKey(Color.yellow, .5f);
        GradientColorKey g = new GradientColorKey(Color.green, 1);
        GradientAlphaKey a = new GradientAlphaKey(1, 0);
        GradientColorKey[] color = new GradientColorKey[]{ r, y, g };
        GradientAlphaKey[] alpha = new GradientAlphaKey[] { a };

        gradient.SetKeys(color, alpha);
        GetComponent<Renderer>().material.color = gradient.Evaluate(health) ;

    }
}
