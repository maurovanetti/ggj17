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
        patrolIndex = 0;
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
    }

    private void NextPatrolPoint()
    {
        patrolIndex = patrolIndex++ % patrolPoints.Count;
        goToTarget.Target = patrolPoints[patrolIndex];
    }
}
