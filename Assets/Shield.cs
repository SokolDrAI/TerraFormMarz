using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public float health { get; private set; }
    public TurretAmmo ammoTower;
    Renderer r;
    public Image part1;
    public Image part2;
    public Image part3;
    public Image part4;
    public Text victoryText;
    bool[] winCheck = new bool[4];
    // Start is called before the first frame update
    void Start()
    {
        ammoTower = GetComponentInChildren<TurretAmmo>();
        health = 1;
        r = GetComponent<Renderer>();

        if(part1 != null)
        {
            part1.color = new Color(1,1,1,.2f);
            part2.color = new Color(1, 1, 1, .2f);
            part3.color = new Color(1, 1, 1, .2f);
            part4.color = new Color(1, 1, 1, .2f);
            victoryText.color = Color.clear;

        }
    }

    public void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            victoryText.color = Color.red;
            victoryText.text = "YOU DIED!";
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if (this.r == null)
            return;
        Gradient gradient = new Gradient();
        GradientColorKey r = new GradientColorKey(Color.red, 0);
        GradientColorKey y = new GradientColorKey(Color.yellow, .5f);
        GradientColorKey g = new GradientColorKey(Color.green, 1);
        GradientAlphaKey a = new GradientAlphaKey(1, 0);
        GradientColorKey[] color = new GradientColorKey[]{ r, y, g };
        GradientAlphaKey[] alpha = new GradientAlphaKey[] { a };

        gradient.SetKeys(color, alpha);
        this.r.material.color = gradient.Evaluate(health) ;

    }

    internal void Equip(Buildable.ObjectType objectType)
    {
        switch(objectType)
        {
            case Buildable.ObjectType.Part1:
                part1.color = Color.white;
                winCheck[0] = true;
                break;
            case Buildable.ObjectType.Part2:
                part2.color = Color.white;
                winCheck[1] = true;
                break;
            case Buildable.ObjectType.Part3:
                part3.color = Color.white;
                winCheck[2] = true;
                break;
            case Buildable.ObjectType.Part4:
                part4.color = Color.white;
                winCheck[3] = true;
                break;
        }

        if(Array.TrueForAll(winCheck,t=>t))
        {
            victoryText.color = Color.white;
            victoryText.text = "A Winner Is You!";
        }
        //TODO: this logic
    }
}
