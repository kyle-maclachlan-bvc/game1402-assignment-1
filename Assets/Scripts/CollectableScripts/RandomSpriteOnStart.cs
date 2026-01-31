using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteOnStart : MonoBehaviour
{
    public Sprite[] possibleSprites;

    void Start()
    {
        if (possibleSprites == null || possibleSprites.Length == 0)
        {
            Debug.LogWarning("No sprites assigned to RandomSpriteOnStart on " + gameObject.name);
            return;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = possibleSprites[Random.Range(0, possibleSprites.Length)];
    }
}
