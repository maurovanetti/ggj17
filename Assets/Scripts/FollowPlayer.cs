using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    GameObject player;  //Public variable to store a reference to the player game object
    public float maxDelta; // in either direction
    public float shiftingSpeed;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        Vector3 offset;
        offset = player.transform.position - transform.position;
        offset.y = 0f;
        float deltaX = offset.x;
        float deltaZ = offset.z;
        if (Mathf.Abs(deltaX) > maxDelta || Mathf.Abs(deltaZ) > maxDelta)
        {
            offset.Normalize();
            transform.position += offset * shiftingSpeed;
        }
    }
}
