using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTarget : MonoBehaviour {
    
    private NavMeshAgent agent;
    public Transform Target
    {
        private get;
        set;
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
