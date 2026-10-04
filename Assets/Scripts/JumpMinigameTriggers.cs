using UnityEngine;

public class JumpMinigameTriggers : MonoBehaviour
{
    //public bool wasTriggered;

    [SerializeField] private int triggerType = 0;

    public GameObject otherTrigger;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Começar minigame");

            if (triggerType == 0)
            {
                GameController.gameController.isInJumpMinigame = true;                
                gameObject.SetActive(false);
            }
            else if (triggerType == 1)
            {

                GameController.gameController.BeginJumpMinigame();                
                otherTrigger.SetActive(true);
                otherTrigger.GetComponent<Collider>().isTrigger = false;
                gameObject.SetActive(false);
                GameController.gameController.uiController.ShowJumpMinigameControls();
                GameController.gameController.player.ResetForward();


            }
            else if (triggerType == 3)
            {
                GameController.gameController.isInJumpMinigame = false;
                GameController.gameController.EndJumpMinigame();
                otherTrigger.SetActive(true);
                otherTrigger.GetComponent<Collider>().isTrigger = false;
                gameObject.SetActive(false);                
                GameController.gameController.player.canMove = false;
            }
            

        }
    }
}
