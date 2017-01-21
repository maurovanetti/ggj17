using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class CharacterDetector : AbstractEnemyAi {

    public float scareDuration;
    public float detectionRadius;
    public float oblivionRadius;
    private bool detected;
    private bool scared;
    private float endOfScare;
    private GoToTarget goToTarget;
    private UnityEvent deathEvent;

    // Use this for initialization
    void Start () {

        if (oblivionRadius <= detectionRadius)
        {
            Debug.LogError("oblivionRadius <= detectionRadius");
        }
        goToTarget = GetComponent<GoToTarget>();
        detected = false;
        scared = false;
        Initialize();
	}

    // Update is called once per frame
    void Update () {
        if (scared)
        {
            if (Time.time > endOfScare)
            {
                scared = false;
                detected = false;
                goToTarget.Walk();
            }            
        }
        else if (!detected)
        {
            if (CharacterDistance() <= detectionRadius)
            {
                goToTarget.MovingTarget = character.transform;
                detected = true;
            }

        }
        else
        {
            CheckKill();
            if (CharacterDistance() > oblivionRadius)
            {
                detected = false;
            }
        } 

	}

    public void ScareAway(Transform scarySource, float minFleeingDistance)
    {
        scared = true;
        endOfScare = Time.time + scareDuration;
        Vector3 offset = transform.position - scarySource.position;
        offset.y = 0f;
        offset.Normalize();
        goToTarget.Target = transform.position + (offset * minFleeingDistance * 1);
        goToTarget.Sprint();
    }
}
