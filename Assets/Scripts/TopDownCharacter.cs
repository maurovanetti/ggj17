using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class TopDownCharacter : MonoBehaviour {

    float m_horizontal;
    float m_vertical;
    Rigidbody m_rigidBody;
    Animator m_animator;
    Transform tr;
    bool m_dying = false;
    
    public float m_speed = 10f;
    public UnityEvent m_onDeathEvent;
    
	void Start ()
    {
        tr = GetComponent<Transform>();
        m_rigidBody = gameObject.GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
    }
    
    void Update ()
    {
        if(!m_dying)
            AnimateCharacter();
    }
    
    public void SetAxis(float horizontal, float vertical)
    {
        m_horizontal = horizontal;
        m_vertical = vertical;
    }

    private void AnimateCharacter()
    {
        FlipPlayer();
        UpdateAnimator();
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

    private void UpdateAnimator()
    {
        m_animator.SetInteger("dir", 0);

        if (m_horizontal != 0)
            m_animator.SetInteger("dir", 1);

        if (m_vertical > 0)
            m_animator.SetInteger("dir", 2);

        if (m_vertical < 0)
            m_animator.SetInteger("dir", 3);

        if (m_rigidBody.velocity == Vector3.zero)
            m_animator.SetBool("moving", false);
        else
            m_animator.SetBool("moving", true);
    }

    void FixedUpdate()
    {
        if (!m_dying)
            m_rigidBody.velocity = tr.right * m_horizontal * m_speed * Time.fixedDeltaTime + tr.up * m_vertical * m_speed * Time.fixedDeltaTime;
        else
            m_rigidBody.velocity = Vector3.zero;
    }

    private void OnDeath()
    {
        m_onDeathEvent.Invoke();
        m_animator.SetBool("dying", true);
        m_dying = true;
    }
}
