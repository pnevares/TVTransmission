using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public AudioSource audioSource;
    public AudioClip snow;
    public AudioClip ticking;
    public AudioClip signal;
    public float fadeDuration;

    private void Start() {
        // play static on loop
        audioSource.clip = snow;
        audioSource.Play();
    }

    public void Success() {
        StartCoroutine(Crossfade(ticking));
    }

    public void CitySuccess() {
        StartCoroutine(Crossfade(signal));
    }

    private IEnumerator Crossfade(AudioClip to) {
        float originalVolume = audioSource.volume;

        while(audioSource.volume > 0) {
            audioSource.volume -= originalVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.clip = to;
        audioSource.Play();

        while(audioSource.volume < originalVolume) {
            audioSource.volume += originalVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }

        yield break;
    }
}