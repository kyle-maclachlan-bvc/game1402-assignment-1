using UnityEngine;

public class PlayerSteps : MonoBehaviour
{
    //[SerializeField] private Vector2 footstepEffectOffset;
    [SerializeField] private ParticleSystem footstepEffect; // The visual effect
    [SerializeField] private string targetTag = "Ground";

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            Debug.Log("Trigger Particles");
            footstepEffect.Play();
        }
    }
}
