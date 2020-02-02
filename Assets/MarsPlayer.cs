using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MarsPlayer : MonoBehaviour
{
    private Player player;
    private Buildable currentBuildable;
    private Rigidbody _rigidbody;
    private LineRenderer lineRenderer;
    private GameObject car;
    Tower currentTower;
    ResourceCollection resourceTarget;


    public Buildable[] buildables;
    float resourceFilledAmount = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        player = ReInput.players.GetPlayer(0);
        _rigidbody = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();
        car = transform.GetChild(0).gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {

        Buildable buildable = collision.gameObject.GetComponent<Buildable>();
        if (buildable != null)
        {
            if(currentBuildable != null)
            {
                Destroy(currentBuildable.gameObject);
            }
            currentBuildable = buildable;
            currentBuildable.transform.parent = transform;
            currentBuildable.transform.localPosition = Vector3.up * 2;
            currentBuildable.transform.localScale = Vector3.one * 0.5f;
        }
        

        if(currentBuildable != null && currentBuildable.objectType == Buildable.ObjectType.Ammo)
        {
            TurretAmmo ammo = collision.gameObject.GetComponentInChildren<TurretAmmo>();
            if (ammo != null)
            {
                Destroy(currentBuildable.gameObject);
                currentBuildable = null;
                ammo.Reload();
            }            
        }
        else if(currentBuildable != null)
        {
            Shield shield = collision.gameObject.GetComponentInChildren<Shield>();
            if(shield != null && shield.ammoTower ==  null)
            {
                shield.Equip(currentBuildable.objectType);
                Destroy(currentBuildable.gameObject);
                currentBuildable = null;
            }
        }

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
        Vector3 BackDirection = new Vector3(player.GetAxis("Horizontal"), 0, player.GetAxis("Vertical")*-1);
        _rigidbody.velocity = direction * 50;

        car.transform.LookAt(BackDirection);

        if (currentTower != null)
        {
            if (player.GetButton("Interact"))
            {
                currentTower.Boost(resourceFilledAmount);
                resourceFilledAmount = 0;
            }
            else if (player.GetButton("Build0"))
            {
                currentTower.Enqueue(buildables[0]);
            }
            else if (player.GetButton("Build1"))
            {
                currentTower.Enqueue(buildables[1]);
            }
            else if (player.GetButton("Build2"))
            {
                currentTower.Enqueue(buildables[2]);
            }
            else if (player.GetButton("Build3"))
            {
                currentTower.Enqueue(buildables[3]);
            }
            else if (player.GetButton("Build4"))
            {
                currentTower.Enqueue(buildables[4]);
            }
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
       

        lineRenderer.enabled = (currentTower != null) || (resourceTarget != null);
        if(lineRenderer.enabled)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, currentTower != null? currentTower.transform.position : new Vector3(Mathf.RoundToInt( transform.position.x/15) * 15 ,transform.position.y, resourceTarget.transform.position.z));
        }
        
    }
}
