using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollowPlayer : MonoBehaviour
{
    public Transform leader;
    public float followSharpness = 0.1f;
    public PlayerMovement player;
    Vector3 _followOffset;

    void Start()
    {
        // Cache the initial offset at time of load/spawn:
        _followOffset = transform.position - leader.position;
        player = leader.gameObject.GetComponent<PlayerMovement>();

    }

    void FixedUpdate()
    {
        // Apply that offset to get a target position.
        Vector3 targetPosition;

        // Keep our y position unchanged.
        //targetPosition.y = transform.position.y;

        // Smooth follow.    
        //transform.position += (targetPosition - transform.position) * followSharpness;
        switch (player.isFacingRight)
        {
            case false: 
                targetPosition = new Vector3(leader.position.x - 0.82f, leader.position.y, leader.position.z + 1);
                break;
            case true:
                targetPosition = new Vector3(leader.position.x + 0.41f, leader.position.y, leader.position.z + 1);
                break;
        }
        transform.position = targetPosition;
    }
}