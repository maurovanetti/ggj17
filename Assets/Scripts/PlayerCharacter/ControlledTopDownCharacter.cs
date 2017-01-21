using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class ControlledTopDownCharacter : TopDownCharacter
{
    public float m_speed = 10f;

    float m_horizontal;
    float m_vertical;    
    Rigidbody m_rigidBody;

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
    
    private void FlipPlayer()
    {
        Vector3 temp = tr.localScale;

        if (m_horizontal < 0)
            temp = new Vector3(-1,1,1);
        
        if(m_horizontal > 0 )
            temp = Vector3.one;

        tr.localScale = temp;
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

    void FixedUpdate()
    {
        if (!m_dying)
        {
            float delta = m_speed * Time.fixedDeltaTime;
            m_rigidBody.velocity = delta * (tr.right * m_horizontal  + tr.up * m_vertical);
        }
        else
        {
            m_rigidBody.velocity = Vector3.zero;
        }
    }    
}
