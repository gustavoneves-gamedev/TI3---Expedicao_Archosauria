using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private MinigameTrigger minigame;
    [SerializeField] private int checkpointCode;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.gameController.SaveGame(transform.position);

            if (minigame != null) minigame.EndMinigame();

            if (checkpointCode == 1) GameController.gameController.isTutorialCompleted = true;

        }        
    }

}
