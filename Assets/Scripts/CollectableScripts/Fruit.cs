using UnityEngine;

public class Fruit : MonoBehaviour, ICollectable
{
    [SerializeField] private string _fruitType;
    
    public string FruitType => _fruitType;

public void OnCollect(GameObject collector)
    {
        GameManager.Instance.AddFruit(_fruitType);
        Destroy(gameObject);
    }

    public void SetFruitType(string fruitType)
    {
        _fruitType = fruitType;
    }
}
