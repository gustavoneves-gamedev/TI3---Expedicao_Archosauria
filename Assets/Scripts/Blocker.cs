using UnityEngine;

public class Blocker : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        

        if (other.CompareTag("Player") && GameController.gameController.hasKey)
        {
            //GameController.gameController.hasKey = true;
            Destroy(gameObject);
        }
        
    }

}
