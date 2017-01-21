using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

    GameObject player;       //Public variable to store a reference to the player game object
    public float maxOffsetMagnitude;
    public Vector3 offset;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        offset = transform.position - player.transform.position;
        offset.y = 0;
        if (offset.magnitude > maxOffsetMagnitude)
        {
            transform.position += offset / 2;
        }
    }
}
