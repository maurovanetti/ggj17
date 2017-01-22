using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDie : MonoBehaviour {

    AudioSource AudioOnPlay;
    public float FadeTime;

    void Start()
    {
        AudioOnPlay = GetComponent<AudioSource>();
        //DontDestroyOnLoad(this);
    }

    public void AudioOnEnd()
    {
        AudioOnPlay.Play();
        Destroy(this.gameObject, 1.0f);
        //StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        float startVolume = AudioOnPlay.volume;

        while (AudioOnPlay.volume > 0)
        {
            AudioOnPlay.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        AudioOnPlay.Stop();
        Destroy(this.gameObject, 1.0f);
        AudioOnPlay.volume = startVolume;
    }
}
