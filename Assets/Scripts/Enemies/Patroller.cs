using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Patroller : AbstractEnemyAi {

    public List<Transform> patrolPoints;
    private int patrolIndex;
    GoToTarget goToTarget;

	// Use this for initialization
	void Start () {
        patrolIndex = -1;
        goToTarget = GetComponent<GoToTarget>();
        Initialize();
        NextPatrolPoint();
    }
	
	// Update is called once per frame
	void Update () {        
        if (goToTarget.DestinationReached)
        {
            NextPatrolPoint();    
        }
        CheckKill();
        CheckKillShadows();
    }

    private void CheckKillShadows()
    {
        foreach (GameObject shadow in GameObject.FindGameObjectsWithTag("Shadow"))
        {
            Vector3 offset = transform.position - shadow.transform.position;
            offset.y = 0f;
            if (offset.magnitude <= killingRadius)
            {                
                TopDownCharacter tdc = shadow.GetComponent<TopDownCharacter>();
                if (tdc)
                {
                    tdc.OnDeath();
                }
                DestroyObject(shadow, 0.1f);
            }
        }
    }

    private void NextPatrolPoint()
    {        
        patrolIndex = (patrolIndex + 1) % patrolPoints.Count;
        //Debug.Log("Next patrol point (" + patrolIndex + ") = " + patrolPoints[patrolIndex].name + " @ " + patrolPoints[patrolIndex].position);
        goToTarget.Target = patrolPoints[patrolIndex].position;
    }
}
