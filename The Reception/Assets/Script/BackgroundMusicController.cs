using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private AudioSource BgSource;

    private void Start()
    {
        BgSource = GetComponent<AudioSource>();
        BgSource.volume = 0;
        StartCoroutine(Fade(true, BgSource, 2f, 0.15f));
        // StartCoroutine(Fade(true, BgSource, 2f, 0f));
    }

    private void Update()
    {
        if (!BgSource.isPlaying)
        {
            BgSource.Play();
            StartCoroutine(Fade(true, BgSource, 2f, 0.15f));
            StartCoroutine(Fade(true, BgSource, 2f, 0f));
        }
    }

    public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume)
    {
        if (!fadeIn)
        {
            double lengthOfSource = (double)BgSource.clip.samples / BgSource.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(lengthOfSource - duration));
        }

        float time = 0f;
        float startVolume = BgSource.volume;
        while (time < duration)
        {
            string fadeSituation = fadeIn ? "fadeIn" : "fadeOut";
            Debug.Log($"{fadeSituation}");
            time += Time.deltaTime;
            BgSource.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
        }
        yield break;
    }
}
