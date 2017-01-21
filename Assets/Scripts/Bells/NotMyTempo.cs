using System;
using System.Collections;
using UnityEngine;

public class NotMyTempo : MonoBehaviour
{

    //public string m_userInputKey;
    //public AudioClip m_audioTrack;
    public float m_startOffset = 0.0f;
    public float m_step = 0.4545f;
    public float m_saveTime = 0.2f;
    public AudioSource m_masterAudioPlayer;
    public AudioSource m_masterBellPlayer;

    float audioStartTime;
    float pressTime;
    float nextKey;
    AudioSource m_localAudioSource;
    string msg1, msg2;
    int comboCounter;

    void Start()
    {
        m_localAudioSource = GetComponent<AudioSource>();
        audioStartTime = Time.unscaledTime;
        m_masterBellPlayer.mute = true;
        nextKey = audioStartTime + m_step;
        Debug.Log("Start time:" + audioStartTime);
        StartCoroutine(CheckTempo());
    }

    private bool Combo()
    {
        return comboCounter == 3;
    }

    private bool KeyWasPressed()
    {
        return Input.GetButtonDown("Jump");
    }

    private IEnumerator CheckTempo()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(m_step);
            nextKey = Time.unscaledTime + m_step;
        }
    }

    void FixedUpdate()
    {
        if (KeyWasPressed())
        {
            pressTime = Time.unscaledTime;
            if (TimingIsCorrect())
            {
                msg1 = "Timing ok:" + pressTime + " key is: " + nextKey;
                comboCounter++;
                m_masterBellPlayer.mute = false;
            }
            else
            {
                msg2 = "Timing wrong:" + pressTime + " key is: " + nextKey;
                m_masterBellPlayer.mute = true;
                m_localAudioSource.Play();
                comboCounter = 0;
            }
        }
        else
        {
            StartCoroutine(MuteCountDown());
        }
    }

    private IEnumerator MuteCountDown()
    {
        yield return new WaitForSecondsRealtime(m_step);
        m_masterBellPlayer.mute = true
            ;
    }

    void OnGUI()
    {
        GUI.Box(new Rect(0, 0, Screen.width, 100), msg1);
        GUI.Box(new Rect(0, 100, Screen.width, 100), msg2);
    }

    private bool TimingIsCorrect()
    {
        return pressTime <= nextKey + m_saveTime && pressTime >= nextKey - m_saveTime;
    }
}
