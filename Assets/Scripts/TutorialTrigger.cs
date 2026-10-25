using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private int tutorialNumber;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            GameController.gameController.uiController.ShowTutorial(tutorialNumber);
            GameController.gameController.uiController.FreeCursor(true);

        }
    }
}
