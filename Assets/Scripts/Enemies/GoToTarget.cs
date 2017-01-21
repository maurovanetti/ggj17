using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTarget : MonoBehaviour {
    
    private NavMeshAgent agent;
    private float destinationReachedTolerance = 0.01f;
    public Transform Target
    {
        private get;
        set;
    }
    public bool DestinationReached
    {
        get
        {
            return agent.remainingDistance < destinationReachedTolerance;
        }
    }

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Target != null && agent.destination != Target.position)
        {
            agent.destination = Target.position;
        }        
	}
}
