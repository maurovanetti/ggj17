using System;
using System.Collections;
using UnityEngine;

public class NotMyTempo : MonoBehaviour {

    //public string m_userInputKey;
    //public AudioClip m_audioTrack;
    public float m_startOffset = 0.0f;
    public float m_step = 0.4545f;
    public float m_saveTime = 0.2f;

    float audioStartTime;
    AudioSource audioPlayer;
    float pressTime;
    float nextKey;

    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
        audioPlayer.Play();
        audioStartTime = Time.unscaledTime;
        nextKey = audioStartTime + m_step;
        Debug.Log("Start time:"+audioStartTime);
        StartCoroutine(CheckTempo());
    }

    private IEnumerator CheckTempo()
    {
        while (true)
        {
            //Debug.Log("Key at :" + nextKey);
            yield return new WaitForSecondsRealtime(m_step);
            nextKey = Time.unscaledTime + m_step; 
        }
    }

    void FixedUpdate () {
        //Debug.Log("Fixed: "+Time.fixedTime);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            pressTime = Time.unscaledTime;
            //Debug.Log("Input in: "+ pressTime);
            if (TimingIsCorrect())
                Debug.Log("Timing ok:" + pressTime + " key is" +nextKey);
            else
                Debug.Log("Timing wrong:" + pressTime + " key is" + nextKey);
        }
	}

    private bool TimingIsCorrect()
    {
       return pressTime <= nextKey + m_saveTime && pressTime >= nextKey - m_saveTime;
    }
}
