using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class PlayerRB : MonoBehaviour
{
    Rigidbody rb;

    public bool isMoving;
    public bool canMove = true;

    public GameObject defaltCamera;
    public GameObject parkourCamera;


    [Header("WALK")]
    //public float rotateSpeed = 130f;
    public float walkSpeed = 12f;
    public float jumpHeight = 2.5f; // usso aq é do charackter controller não é mais usado deixei ai pq tem uns negocio q vc fez q tao chamando ele se tirar vai dar erro
    private Vector3 dir;
    

    [Header("JUMP")]
    [SerializeField] private bool possibleJump;
    //public float jumpForce = 5f;
    public Vector3 jumpForce;

    [Header("LOOK")]
    public float mouseSensitivity = 2f;

    [Header("CLIMB")]
    public float climbSpeedV = 5f;
    public float climbSpeedH = 5f;
    public float obstacleMaxDistance = 0.7f;
    public float climbLock = 0.3f;
    private LayerMask obstacleLayer;
    [SerializeField] private bool isClimbing;
    private float climbLockTimer;

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
        obstacleLayer = LayerMask.GetMask("Obstacle");
    }

    void Update()
    {
        if (!GameController.gameController.isPlaying || GameController.gameController.isPaused || !canMove || isInvestigating)
        {
            dir = Vector3.zero;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (PossibleJump() || isClimbing))
        {
            //Vector3 jumpImpulse = new Vector3(0f, jumpForce, 0f);

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(jumpForce, ForceMode.Impulse);

            isClimbing = false;
            climbLockTimer = climbLock;
        }

        Look();
    }

    void FixedUpdate()
    {
        Climbing();
        /*if (!GameController.gameController.isPlaying || !canMove || isInvestigating)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
        else*/ if (isClimbing)
        {
            Climb();  
        }
        else Movement();
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


        Vector3 velocidadeAlvo = dir * walkSpeed;
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
    //    tablet.ClueHelper();
    //}

    public void ExitInvestigationMode()
    {
        isInvestigating = false;
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        GameController.gameController.uiController.FreeCursor(false);
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

        if (other.CompareTag("Parkour"))
        {
            defaltCamera.gameObject.SetActive(!defaltCamera.gameObject.activeSelf);
            parkourCamera.gameObject.SetActive(!parkourCamera.gameObject.activeSelf);
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

    public bool PossibleJump()
    {
        possibleJump = Physics.Raycast(transform.position, Vector3.down, 1.15f, ~0, QueryTriggerInteraction.Ignore);
        return possibleJump;
    }

    public bool ObstacleAhead()
    {

        bool obstacleAhead = Physics.Raycast(transform.position + Vector3.down * 1f, transform.forward, out RaycastHit obstacleHit, obstacleMaxDistance, obstacleLayer);
        return obstacleAhead;

    }

    public void Climbing()
    {
        if (climbLockTimer > 0f)
        {
            climbLockTimer -= Time.fixedDeltaTime;
            isClimbing = false;
            return;
        }
        else if (ObstacleAhead())
        {
            Debug.Log("d");
            isClimbing = true;
        }
        else isClimbing = false;
    }

    public void Climb()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        isMoving = h != 0 || v != 0;

        //rb.AddForce(gravityForce, ForceMode.Force);

        rb.linearVelocity = transform.up * v * climbSpeedV + transform.right * h * climbSpeedH;
    }
}