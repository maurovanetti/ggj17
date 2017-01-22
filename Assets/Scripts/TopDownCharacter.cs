﻿using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public abstract class TopDownCharacter : MonoBehaviour
{

    protected Animator m_animator;
    protected Transform tr;
    protected bool m_dying = false;

    public UnityEvent m_onDeathEvent;
    public SpriteRenderer spriteRenderer;

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
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        m_animator.SetInteger("dir", 0);

        if (HorizontalMovement() != 0)
            m_animator.SetInteger("dir", 1);

        if (VerticalMovement() > 0.09)
            m_animator.SetInteger("dir", 2);

        if (VerticalMovement() < -0.09)
            m_animator.SetInteger("dir", 3);

        if(spriteRenderer && HorizontalMovement()!= 0)
        {
            spriteRenderer.flipX = HorizontalMovement() > 0;
        }

        if (Velocity().magnitude <= 0.09 && Velocity().magnitude >= -0.09)
            m_animator.SetBool("moving", false);
        else
            m_animator.SetBool("moving", true);

    }

    protected abstract float HorizontalMovement();
    protected abstract float VerticalMovement();
    protected abstract Vector3 Velocity();

    public void OnDeath()
    {
        Debug.Log(this.name + " dying");
        m_onDeathEvent.Invoke();
        m_animator.SetBool("dying", true);
        m_dying = true;
    }
}
