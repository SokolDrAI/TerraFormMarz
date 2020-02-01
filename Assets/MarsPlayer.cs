using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MarsPlayer : MonoBehaviour
{
    private Player player;

    // Start is called before the first frame update
    void OnEnable()
    {
        player = ReInput.players.GetPlayer(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3( player.GetAxis("Horizontal"),0, player.GetAxis("Vertical"));

        transform.transform.position += (Vector3)direction * Time.deltaTime * 30;
    }
}
