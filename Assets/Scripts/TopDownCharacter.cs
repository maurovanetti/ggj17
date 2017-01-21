using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public abstract class TopDownCharacter : MonoBehaviour
{

    protected Animator m_animator;
    protected Transform tr;
    protected bool m_dying = false;

    public UnityEvent m_onDeathEvent;

    void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        tr = GetComponent<Transform>();
        m_animator = GetComponent<Animator>();
        m_animator.SetInteger("dir", 0);
    }

    void Update()
    {
        if (!m_dying)
            AnimateCharacter();
    }

    private void AnimateCharacter()
    {
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {        
        if (HorizontalMovement() != 0)
            m_animator.SetInteger("dir", 1);

        if (VerticalMovement() > 0)
            m_animator.SetInteger("dir", 2);

        if (VerticalMovement() < 0)
            m_animator.SetInteger("dir", 3);

        if (Velocity() == Vector3.zero)
            m_animator.SetBool("moving", false);
        else
            m_animator.SetBool("moving", true);

    }

    protected abstract float HorizontalMovement();
    protected abstract float VerticalMovement();
    protected abstract Vector3 Velocity();

    public void OnDeath()
    {
        m_onDeathEvent.Invoke();
        m_animator.SetBool("dying", true);
        m_dying = true;
    }
}
