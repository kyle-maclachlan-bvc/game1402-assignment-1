using UnityEngine;

public class PlatformDetector : MonoBehaviour
{
    private Transform _currentParent;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _currentParent = other.transform.parent;
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(_currentParent);
        }
    }
}
