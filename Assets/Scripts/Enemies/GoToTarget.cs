using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToTarget : MonoBehaviour {
    
    public float walkingSpeed;
    public float runningSpeed;

    private NavMeshAgent agent;
    private float destinationReachedTolerance = 0.01f;
    public Vector3? _target;
    public Vector3? Target
    {
        private get
        {
            return _target;
        }
        set
        {
            MovingTarget = null;
            _target = value;
        }
    }
    public Transform MovingTarget
    {
        private get;
        set;
    }
    
    public bool DestinationReached
    {
        get
        {
            Debug.Log(this.name + " _target=" + _target  + " pathPending=" + agent.pathPending + " remainingDistance=" + agent.remainingDistance);
            if (_target != null && !agent.pathPending)
            {
                if (agent.remainingDistance != 0 || transform.position == _target) // fix for remainingDistance maybe-bug
                {
                    return agent.remainingDistance < destinationReachedTolerance;
                }
            }
            return false;
        }
    }

    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        Walk();
    }
	
	// Update is called once per frame
	void Update () {        
        if (MovingTarget)
        {
            _target = MovingTarget.position;
        }
        if (_target != null && agent.destination != _target)
        {
            agent.destination = (Vector3) Target;
        }        
	}

    public void Sprint()
    {
        agent.speed = runningSpeed;
    } 
    
    public void Walk()
    {
        agent.speed = walkingSpeed;
    }     
}
