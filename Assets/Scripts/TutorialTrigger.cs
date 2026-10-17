using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    [SerializeField] private int tutorialNumber;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.gameController.uiController.ShowTutorial(tutorialNumber);
            gameObject.SetActive(false);

        }
    }
}
