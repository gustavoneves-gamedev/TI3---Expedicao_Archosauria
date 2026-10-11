using UnityEngine;

public class Traps : MonoBehaviour
{
   // private Vector3 startPosition;
    //public float hitForce;
    //public float delay;
    private Rigidbody rb;

    [SerializeField] private float initialSpeed = 10f;
    //public Traps[] sphereTraps = new Traps[5];

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //startPosition = transform.position;

        //rb.AddLinearVelocity(transform.forward * initialSpeed);
        rb.linearVelocity = -transform.forward * initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Hole"))
        //{
        //    Debug.Log("b");
        //    gameObject.SetActive(false);
        //    transform.position = startPosition;
        //}
        //if (other.CompareTag("Player"))
        //{
        //    foreach (var st in sphereTraps)
        //    {
        //        st.HitDelay();
        //    }
        //}
    }

    //public void Hit()
    //{
    //    if (rb == null) return;
    //    rb.AddForce(transform.forward * hitForce);
    //}
    //public void HitDelay()
    //{
    //    Invoke("Hit", delay);
    //}
}
