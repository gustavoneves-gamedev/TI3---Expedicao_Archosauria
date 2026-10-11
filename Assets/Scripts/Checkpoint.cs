using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private MinigameTrigger minigame;    
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameController.gameController.SaveGame(transform.position);

            if (minigame != null) minigame.EndMinigame();

        }        
    }

}
