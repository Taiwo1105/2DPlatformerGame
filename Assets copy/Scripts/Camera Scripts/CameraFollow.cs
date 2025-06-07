using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float resetSpeed = 0.5f;
    public float cameraSpeed = 0.3f;

    public Bounds cameraBounds;

    private Transform target; // GameObject transform holding reference to the player.
    private float offsetZ;
    private Vector3 currentVelocity;

    private bool followsPlayer;

    void Awake()
    {
        BoxCollider2D myCol = GetComponent<BoxCollider2D>();
        myCol.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
        cameraBounds = myCol.bounds;
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            target = playerObj.transform;
            offsetZ = (transform.position - target.position).z;
            followsPlayer = true;
        }
        else
        {
            Debug.LogError("Player object not found. Make sure it is tagged 'Player'.");
            followsPlayer = false;
        }
    }

    void FixedUpdate()
    {
        if (followsPlayer && target != null)
        {
            Vector3 aheadTargetPos = target.position + Vector3.forward * offsetZ;

            if (aheadTargetPos.x < transform.position.x)
            {
                return;
            }

            Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPos,
                ref currentVelocity, cameraSpeed);

            transform.position = new Vector3(newCameraPosition.x, transform.position.y,
                newCameraPosition.z);
        }
    }

    // Call this when the player respawns to reset the camera follow target
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        offsetZ = (transform.position - target.position).z;
        followsPlayer = true;
    }
}
