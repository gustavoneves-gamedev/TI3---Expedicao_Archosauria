using UnityEngine;

public class Tablet : MonoBehaviour
{
    private Player player;    
    
    void Start()
    {
        player = GetComponent<Player>();
    }

}
