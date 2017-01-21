using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetector : MonoBehaviour {

    public float detectionRadius;

    const string characterTag = "Player";
    GameObject character;
    private bool detected;

	// Use this for initialization
	void Start () {
        detected = false;
        character = GameObject.FindGameObjectWithTag(characterTag);
	}
	
	// Update is called once per frame
	void Update () {
        if (!detected && Vector3.Distance(transform.position, character.transform.position) <= detectionRadius)
        {
            GetComponent<GoToTarget>().Target = character.transform;
            detected = true;
        }
	}
}
