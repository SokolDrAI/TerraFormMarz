using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : MonoBehaviour
{
    LineRenderer lineRenderer;
    float fadeTime = 0;
    const float fadeLength = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startColor = new Color(1, 0, 0, 0);
        lineRenderer.endColor = new Color(1, 0, 0, 0);
        lineRenderer.positionCount = 2;

    }

    public void Fire(Vector3 startPos, Vector3 endPos)
    {
        fadeTime = fadeLength;
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    // Update is called once per frame
    void Update()
    {
        if(fadeTime > 0)
        {
            fadeTime =Mathf.Clamp01(fadeTime- Time.deltaTime);
            lineRenderer.startColor = new Color(1, 0, 0, fadeTime / fadeLength);
            lineRenderer.endColor = new Color(1, 0, 0, fadeTime / fadeLength);
        }
        else
        {
            lineRenderer.startColor = new Color(1, 0, 0, 0);
            lineRenderer.endColor = new Color(1, 0, 0,0);
        }
    }
}
