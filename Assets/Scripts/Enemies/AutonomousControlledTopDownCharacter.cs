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
        StartCoroutine(PlayRandomSound());
    }

    protected override float HorizontalMovement()
    {
        return agent.velocity.z;
    }

    protected override float VerticalMovement()
    {
        return agent.velocity.y;
    }

    protected override Vector3 Velocity()
    {
        return agent.velocity;
    }    
}
