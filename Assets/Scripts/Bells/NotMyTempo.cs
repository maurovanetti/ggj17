using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class NotMyTempo : MonoBehaviour
{

    //public string m_userInputKey;
    //public AudioClip m_audioTrack;
    public float m_startOffset = 0.0f;
    public float m_step = 0.4f;
    public float m_saveTime = 0.2f;
    public AudioSource m_masterBellPlayer;
    public AudioClip m_succeedSound;
    public int m_maxCombCounter;
    public UnityEvent onComboSucceed;
    public UnityEvent onSucceedHit;

    float audioStartTime;
    float updateTime;
    float nextKey;
    AudioSource m_localAudioSource;
    string msg1, msg2;
    int comboCounter = 0;

    void Start()
    {
        m_localAudioSource = GetComponent<AudioSource>();
        audioStartTime = Time.unscaledTime;
        nextKey = audioStartTime + m_step;
        StartCoroutine(CheckTempo());
    }

    private bool KeyWasPressed()
    {
        return Input.GetButtonDown("Jump");
    }

    private IEnumerator CheckTempo()
    {
        while (true)
        {
            m_masterBellPlayer.loop = false;
            yield return new WaitForSecondsRealtime(m_step);
            nextKey = Time.unscaledTime + m_step;
        }
    }

    void Update()
    {
        updateTime = Time.unscaledTime;

        if (TimingIsCorrect())
        {
            if (KeyWasPressed())
            {
                msg1 = "Timing ok:" + updateTime + " key is: " + nextKey;
                m_masterBellPlayer.volume = 1;
                comboCounter++;
                onSucceedHit.Invoke();
                HandleHeadButt();
                m_masterBellPlayer.loop = true;
                if (!m_masterBellPlayer.isPlaying)
                    m_masterBellPlayer.Play();
            }
        }
        else
        {
            if (KeyWasPressed())
            {
                msg2 = "Timing wrong:" + updateTime + " key is: " + nextKey;
                HandleHeadButt();
                m_masterBellPlayer.loop = false;
                m_localAudioSource.Play();
                comboCounter = 0;
            }
        }

        if (comboCounter >= m_maxCombCounter)
        {
            m_masterBellPlayer.PlayOneShot(m_succeedSound);
            onComboSucceed.Invoke();
            comboCounter = 0;
        }
    }

    private void HandleHeadButt()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("Player");
        temp.GetComponent<ControlledTopDownCharacter>().HeadButt( gameObject.transform );
    }

    //void OnGUI()
    //{
    //    GUI.Box(new Rect(0, 0, Screen.width, 100), msg1);
    //    GUI.Box(new Rect(0, 100, Screen.width, 100), msg2);
    //}

    private bool TimingIsCorrect()
    {
        return updateTime <= nextKey + m_saveTime && updateTime >= nextKey - m_saveTime;
    }
}
