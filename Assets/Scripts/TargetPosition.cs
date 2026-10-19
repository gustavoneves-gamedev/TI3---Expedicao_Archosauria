using UnityEngine;

public class TargetPosition : MonoBehaviour
{
    public Vector3 offset;
    
    
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = GameController.gameController.player.transform.position + offset;
    }
}
