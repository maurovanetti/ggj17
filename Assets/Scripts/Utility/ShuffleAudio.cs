using UnityEngine;
using System.Collections.Generic;

public class ShuffleAudio : MonoBehaviour
{

    public bool forceFirst = true;
    public List<AudioClip> m_clips;
    public int startLoop = 3;

    AudioSource emitter;

    void Start()
    {
        emitter = GetComponent<AudioSource>();
        if (m_clips.Count > 0)
        {
            if (forceFirst)
            {
                emitter.clip = m_clips[0];
            }
            else
            {
                emitter.clip = m_clips[Random.Range(0, m_clips.Count)];
            }
        }

    }

    void Update()
    {
        if (startLoop > 0)
        {
            if (!emitter.isPlaying)
            {
                emitter.Stop();
                emitter.Play();
                startLoop--;
            }
        }
        else
        {
            if (!emitter.isPlaying)
            {
                int index = Random.Range(0, m_clips.Count);
                emitter.clip = m_clips[index];
                GameObject[] glist = GameObject.FindGameObjectsWithTag("Bell");
                NotMyTempo temp;
                if (index == 1)
                {
                    for (int i = 0; i < glist.Length; i++)
                    {
                        temp = glist[i].GetComponent<NotMyTempo>();
                        temp.m_step = 0.5f;
                        temp.m_saveTime = 0.3f;
                    }
                }
                else
                {
                    for (int i = 0; i < glist.Length; i++)
                    {
                        temp = glist[i].GetComponent<NotMyTempo>();
                        temp.m_step = 0.4f;
                        temp.m_saveTime = 0.25f;
                    }
                }
                emitter.Play();
            }
        }
    }
}
