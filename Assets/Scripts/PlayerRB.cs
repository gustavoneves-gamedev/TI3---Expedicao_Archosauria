using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerRB : MonoBehaviour
{
    Rigidbody rb;

    public bool isMoving;
    public bool canMove = true;


    [Header("WALK")]
    public float rotateSpeed = 130f;
    public float walkSpeed = 10f;
    public float jumpHeight = 2.5f;
    private Vector3 dir;
    [SerializeField] private float vel = 12f;

    [Header("JUMP")]
    [SerializeField] private bool possibleJump;
    //public float jumpForce = 5f;
    public Vector3 jumpForce;

    [Header("LOOK")]
    public float mouseSensitivity = 2f;

    [Header("CLIMB")]
    //public float climbSpeedV = 5f;
    //public float climbSpeedH = 5f;
    //public float obstacleMaxDistance = 0.6f;
    //public float climbLock = 0.3f;
    //private LayerMask surfaceLayer;
    //[SerializeField] private bool isClimbing;
    //private float climbLockTimer;

    [Header("INVESTIGATE")]
    public bool isInvestigating;
    public int currentClue;
    public Transform clueFocus;
    public Clue clue;

    public bool isInJumpMinigame;
    private float idleTime = 5f;
    [SerializeField] private CameraController mainCamera;
    
    public Vector3 gravityForce;

    public Tablet tablet;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        tablet = GetComponent<Tablet>();
        GameController.gameController.player = this;
       // surfaceLayer = LayerMask.GetMask("Ground" + "Obstacle" + "Wall");
    }

    void Update()
    {
        if (!GameController.gameController.isPlaying || GameController.gameController.isPaused || !canMove || isInvestigating)
        {
            dir = Vector3.zero;
            return;
        }

        //possibleJump = Physics.Raycast(transform.position, Vector3.down, 1.15f, ~0, QueryTriggerInteraction.Ignore);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Vector3 jumpImpulse = new Vector3(0f, jumpForce, 0f);

            // Aplica a força instantânea
            rb.AddForce(jumpForce, ForceMode.Impulse);
        }

        Look();
       

    }

    void FixedUpdate()
    {
        Movement();
       
    }

    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
    }

    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        dir = (transform.forward * v + transform.right * h).normalized;
        isMoving = dir.sqrMagnitude > 0.01f;        
            
        rb.AddForce(gravityForce, ForceMode.Force);


        Vector3 velocidadeAlvo = dir * vel;
        rb.linearVelocity = new Vector3(velocidadeAlvo.x, rb.linearVelocity.y, velocidadeAlvo.z);
    }

    #region Investigation

    //public void GetCurrentInvestigation()
    //{

    //}

    public void EnterInvestigationMode()
    {
        isInvestigating = true;
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        mainCamera.InvestigationCamera(clueFocus);
        clue.ChooseMenu();
    }

    //public void OpenTablet()
    //{
    //    tablet.ClueHelper(currentClue);
    //}

    public void ExitInvestigationMode()
    {
        isInvestigating = false;
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        mainCamera.NormalCamera();
    }

    #endregion


    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Save"))
        //{
        //    GameController.gameController.SaveGame(other.gameObject.transform.position);
        //}

        if (other.CompareTag("Key"))
        {
            GameController.gameController.hasKey = true;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Hole"))
        {
            Debug.Log("a");
           
            transform.position = GameController.gameController.lastSave;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SphereTrap")
        {
            Debug.Log("c");
            //cc.enabled = false;
            transform.position = GameController.gameController.lastSave;
            //cc.enabled = true;
        }


    }
    /*public bool PossibleJump()
    {
        return Physics.Raycast(transform.position + (-transform.up * 1f), -transform.up, 0.2f, surfaceLayer);
    }*/
}