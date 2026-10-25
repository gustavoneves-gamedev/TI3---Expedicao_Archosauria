using UnityEngine;

public class FloatingRock : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField] private float initialSpeed = 5f;
    public float speedMultiplier = 1f;

    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rb.linearVelocity = -transform.forward * initialSpeed * speedMultiplier;
        Destroy(gameObject, 30f);
    }

    private void FixedUpdate()
    {
        
    }

}
