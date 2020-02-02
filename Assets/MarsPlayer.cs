using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MarsPlayer : MonoBehaviour
{
    private Player player;
    private Rigidbody _rigidbody;
    private LineRenderer lineRenderer;
    Tower currentTower;
    ResourceCollection resourceTarget;

    float resourceFilledAmount = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        player = ReInput.players.GetPlayer(0);
        _rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Tower otherTower = other.GetComponent<Tower>();
        if (otherTower !=null)
        {
            currentTower = otherTower;
        }

        ResourceCollection currentResource = other.GetComponent<ResourceCollection>();
        if (currentResource != null)
        {
            resourceTarget = currentResource;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Tower otherTower = other.GetComponent<Tower>();
        if (otherTower == currentTower)
        {
            currentTower = null;
        }

        ResourceCollection currentResource = other.GetComponent<ResourceCollection>();
        if (currentResource == resourceTarget)
        {
            resourceTarget = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = new Vector3( player.GetAxis("Horizontal"),0, player.GetAxis("Vertical"));
        _rigidbody.velocity = direction * 40;

        if(player.GetButton("Interact") && currentTower != null)
        {
            currentTower.Repair(Time.fixedDeltaTime * 0.2f);
        }
       
    }

    private void Update()
    {
        if(resourceTarget != null)
        {
            resourceFilledAmount = Mathf.Clamp01(Time.deltaTime / 2 + resourceFilledAmount);
        }
        Color color = Color.Lerp(Color.gray, Color.green, resourceFilledAmount);
        GetComponent<Renderer>().material.color = color;
       

        lineRenderer.enabled = (currentTower != null && currentTower.repairValue < 1) || (resourceTarget != null);
        if(lineRenderer.enabled)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentTower != null? currentTower.transform.position : new Vector3(Mathf.RoundToInt( transform.position.x/15) * 15 ,transform.position.y, resourceTarget.transform.position.z));
        }
        
    }
}
