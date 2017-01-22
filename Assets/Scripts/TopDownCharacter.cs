using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public abstract class TopDownCharacter : MonoBehaviour
{

    protected Animator m_animator;
    protected Transform tr;
    protected bool m_dying = false;
    protected AudioSource emitter;

    public List<AudioClip> stepSounds;
    public AudioClip voice; 

    public UnityEvent m_onDeathEvent;
    public SpriteRenderer spriteRenderer;

    int stepCount = 0;

    void Start()
    {
        Initialize();
    }

    protected IEnumerator PlayRandomSound()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(3.0f,16.5f));
            emitter.PlayOneShot(voice);
            yield return new WaitForSeconds(3.0f);
        }
    }

    public void StepSoundCyclePlay()
    {
        if(!emitter.isPlaying&&stepSounds.Count>0)
        {
            emitter.clip = stepSounds[stepCount];
            stepCount++;
            if (stepCount >= stepSounds.Count)
                stepCount = 0;
            emitter.Play();
        }
    }

    protected void Initialize()
    {
        tr = GetComponent<Transform>();
        m_animator = GetComponent<Animator>();
        emitter = GetComponent<AudioSource>();
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

        if (Mathf.Abs(VerticalMovement()) > Mathf.Abs(HorizontalMovement()))
        {
            if (VerticalMovement() > 0.09)
                m_animator.SetInteger("dir", 2);

            if (VerticalMovement() < -0.09)
                m_animator.SetInteger("dir", 3);
        }

        if (spriteRenderer && HorizontalMovement()!= 0)
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
        emitter.PlayOneShot(voice);
        if (this.CompareTag("Player"))
        {
            SceneManager.LoadScene("DieScene", LoadSceneMode.Single);
        }
    }
}
