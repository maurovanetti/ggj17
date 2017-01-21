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
    }

    void Update()
    {
        if (!m_dying)
            AnimateCharacter();
    }

    private void AnimateCharacter()
    {
        FlipPlayer();
        UpdateAnimator();
    }

    private void FlipPlayer()
    {
        Vector3 temp = tr.localScale;
        if (HorizontalMovement() < 0)
        {
            temp = new Vector3(-1, 1, 1);
        }
        else if (HorizontalMovement() > 0)
        {
            temp = Vector3.one;
        }
        tr.localScale = temp;
    }

    private void UpdateAnimator()
    {
        return;
        m_animator.SetInteger("dir", 0);

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

    private void OnDeath()
    {
        m_onDeathEvent.Invoke();
        m_animator.SetBool("dying", true);
        m_dying = true;
    }
}
