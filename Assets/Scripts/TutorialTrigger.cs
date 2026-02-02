using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [TextArea(2, 4)] [SerializeField] private string tutorialMessage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
        GameManager.Instance.ShowTutorialMessage(tutorialMessage);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        GameManager.Instance.HideTutorialMessage();
    }
}
