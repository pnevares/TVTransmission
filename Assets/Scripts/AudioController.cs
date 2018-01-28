using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	public AudioSource audioSource0;
	public AudioSource audioSource1;
    public AudioClip snow;
    public AudioClip ticking;
    public AudioClip signal;
    public float fadeDuration;

    private void Start() {
        // play static on loop
        audioSource0.clip = snow;
        audioSource0.Play();
    }

    public void Success() {
        StartCoroutine(Crossfade(ticking));
    }

    public void CitySuccess() {
        StartCoroutine(Crossfade(signal));
    }

    private IEnumerator Crossfade(AudioClip to) {
        float originalVolume = audioSource0.volume;
        audioSource1.volume = 0f;
        audioSource1.clip = to;
        audioSource1.Play();

        while(audioSource0.volume > 0) {
            audioSource0.volume -= originalVolume * Time.deltaTime / fadeDuration;
            audioSource1.volume += originalVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        yield break;
    }
}