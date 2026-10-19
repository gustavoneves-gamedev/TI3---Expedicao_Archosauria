using UnityEngine;

public class Blocker : MonoBehaviour
{
    [SerializeField] private GameObject blocker;
    [SerializeField] private int blockCode;
    private bool hasShow;

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && GameController.gameController.hasKey)
        {
            //GameController.gameController.hasKey = true;
            Destroy(blocker);
        }
        else if (other.CompareTag("Player") && blockCode == 0 && !hasShow)
        {
            //GameController.gameController.hasKey = true;
            GameController.gameController.uiController.ShowTutorial(3);
            hasShow = true;
        }

    }

}
