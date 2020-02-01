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
    }


    private void OnTriggerExit(Collider other)
    {
        Tower otherTower = other.GetComponent<Tower>();
        if (otherTower == currentTower)
        {
            currentTower = null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = new Vector3( player.GetAxis("Horizontal"),0, player.GetAxis("Vertical"));
        _rigidbody.velocity = direction * 20;

        if(player.GetButton("Interact") && currentTower != null)
        {
            currentTower.Repair(Time.fixedDeltaTime * 0.2f);
        }
       
    }

    private void Update()
    {
        lineRenderer.enabled = currentTower != null && currentTower.repairValue < 1;
        if(lineRenderer.enabled)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentTower.transform.position);
        }
        
    }
}
