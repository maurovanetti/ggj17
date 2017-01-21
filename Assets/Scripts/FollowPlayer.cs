using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    GameObject player;  //Public variable to store a reference to the player game object
    public float maxDelta; // in either direction
    public Vector3 offset;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {        
        offset = player.transform.position - transform.position;
        float deltaX = offset.x;
        float deltaZ = offset.z;
        if (Mathf.Abs(deltaX) > maxDelta || Mathf.Abs(deltaZ) > maxDelta)
        {
            transform.position += Mathf.Sign(deltaX) * Vector3.right;
            transform.position += Mathf.Sign(deltaZ) * Vector3.forward;
        }
    }
}
