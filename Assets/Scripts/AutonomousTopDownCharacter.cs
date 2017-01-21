using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class AutonomousControlledTopDownCharacter : TopDownCharacter
{
    private NavMeshAgent agent;

    void Start()
    {
        Initialize();
        agent = GetComponent<NavMeshAgent>();
    }

    protected override float HorizontalMovement()
    {
        return agent.nextPosition.x - tr.position.x;
    }

    protected override float VerticalMovement()
    {
        return agent.nextPosition.y - tr.position.y; ;
    }

    protected override Vector3 Velocity()
    {
        return agent.velocity;
    }    
}
