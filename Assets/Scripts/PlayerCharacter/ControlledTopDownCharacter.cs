using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class ControlledTopDownCharacter : TopDownCharacter
{
    public float m_speed = 10f;

    float m_horizontal;
    float m_vertical;    
    Rigidbody m_rigidBody;

    public GameObject m_candleHolder;

    void Start()
    {
        Initialize();
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    public void SetAxis(float horizontal, float vertical)
    {
        m_horizontal = horizontal;
        m_vertical = vertical;
    }

    protected override float HorizontalMovement()
    {
        return m_horizontal;
    }

    protected override float VerticalMovement()
    {
        return m_vertical;
    }

    protected override Vector3 Velocity()
    {
        return m_rigidBody.velocity;
    }

    public void HeadButt(Transform target)
    {
        if (spriteRenderer.flipX)
            spriteRenderer.flipX = false;

        string trigger = "hit";
        if (tr.position.x - target.position.x > 0)
            trigger += "lt";
        else
            trigger += "rt";
        m_animator.SetTrigger(trigger);
    }

    void FixedUpdate()
    {
        if (!m_dying)
        {
            float delta = m_speed * Time.fixedDeltaTime;
            m_rigidBody.velocity = delta * (tr.right * m_horizontal  + tr.forward * m_vertical).normalized;
            if (m_rigidBody.velocity != Vector3.zero)
            {
                m_candleHolder.transform.forward = m_rigidBody.velocity;
            }
        }
        else
        {
            m_rigidBody.velocity = Vector3.zero;
        }
    }    
}
