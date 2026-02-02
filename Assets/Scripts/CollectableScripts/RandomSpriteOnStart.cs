using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Fruit))]

public class RandomSpriteOnStart : MonoBehaviour
{
    [Header("Sprites match Fruit Types by Index")]
    public Sprite[] possibleSprites;
    public string[] fruitTypes;

    void Start()
    {
        if (possibleSprites == null || possibleSprites.Length == 0 || fruitTypes.Length == 0)
        {
            Debug.LogWarning("Missing Sprites or fruit data on" + gameObject.name);
            return;
        }

        if (possibleSprites.Length != fruitTypes.Length)
        {
            Debug.LogWarning("Sprite and Fruit Type arrays must be the same length!");
            return;
        }
        
        int index = Random.Range(0, possibleSprites.Length);

        SpriteRenderer render = GetComponent<SpriteRenderer>();
        render.sprite = possibleSprites[index];

        Fruit fruit = GetComponent<Fruit>();
        fruit.SetFruitType(fruitTypes[index]);
    }
}
