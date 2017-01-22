using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerAudio : MonoBehaviour {

    AudioSource[] AudioOnPlay;
    public float FadeTime;

    void Start ()
    { 
        AudioOnPlay = GetComponents<AudioSource>();
        DontDestroyOnLoad(this);
    }

    public void AudioOnStart()
    {
        AudioOnPlay[0].Play();
        StartCoroutine(FadeOut());
    }

    public void AudioOnEnd()
    {
        AudioOnPlay[0].Play();
        Destroy(this.gameObject, 1.0f);
    }

    public IEnumerator FadeOut()
    {
        float startVolume = AudioOnPlay[1].volume;

        while (AudioOnPlay[1].volume > 0)
        {
            AudioOnPlay[1].volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        AudioOnPlay[1].Stop();
        Debug.LogWarning("SIPARIO");
        Destroy(this.gameObject, 1.0f);
        AudioOnPlay[1].volume = startVolume;
    }
}
