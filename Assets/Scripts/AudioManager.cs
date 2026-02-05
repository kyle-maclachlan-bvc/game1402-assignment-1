using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip coinPickup;  // Coin Sound Effect

    [Header("Settings")]
    [Range(0f, 1f)]
    [SerializeField] private float sfxVolume = 1f;

    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;  //2D Audio
    }
    
    // Public SFX Methods
    public void PlayCoinPickup()
    {
        PlaySFX(coinPickup);
    }
    
    // Internal Helper
    private void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip, sfxVolume);
    }
    
    
}
