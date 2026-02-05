using Unity.VisualScripting;
using UnityEngine;

public class PlaySoundEffect : MonoBehaviour
{
    public static void Play(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;

        GameObject temp = new GameObject("CoinCollectAudio");
        AudioSource source = temp.AddComponent<AudioSource>();

        source.clip = clip;
        source.volume = volume;
        source.spatialBlend = 0f; // 2D sound
        source.Play();
        
        Object.Destroy(temp, clip.length);
    }
}
