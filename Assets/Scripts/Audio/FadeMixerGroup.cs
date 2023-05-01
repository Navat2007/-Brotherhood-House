using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
public static class FadeMixerGroup {
    public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
    {
        float currentTime = 0;
        audioMixer.GetFloat(exposedParam, out var currentVolume);
        currentVolume = Mathf.Pow(10, currentVolume / 20);
        float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1);
        
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float newVol = Mathf.Lerp(currentVolume, targetValue, currentTime / duration);
            audioMixer.SetFloat(exposedParam, Mathf.Log10(newVol) * 20);
            
            yield return null;
        }
    }
}