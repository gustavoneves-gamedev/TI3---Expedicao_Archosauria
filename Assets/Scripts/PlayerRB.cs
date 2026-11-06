using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Rigidbody))]

public class PlayerRB : MonoBehaviour
{
    Rigidbody rb;

    public bool isMoving;
    public bool canMove = true;

    public GameObject defaltCamera;
    


    [Header("WALK")]
    //public float rotateSpeed = 130f;
    public float walkSpeed = 12f;
    public float jumpHeight = 2.5f; // isso aq é do character controller não é mais usado deixei ai pq tem uns negocio q vc fez q tao chamando ele se tirar vai dar erro
    private Vector3 dir;
    

    [Header("JUMP")]
    [SerializeField] private bool possibleJump;
    //public float jumpForce = 5f;
    public Vector3 jumpForce;
    public float coyoteTime = 0.12f;
    private float coyoteTimer;

    public float deadlySpeed = 30f;

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

    [Header("PARKOUR")]
    public GameObject parkourCamera;
    [SerializeField] private bool inParkour = false;
    public float rotateSpeed = 12f;
    public Vector3 sideParkour = new Vector3(0f, 0f, 1f);

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

        if (PossibleJump() && rb.linearVelocity.y <= 0.1f)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (coyoteTimer > 0f || isClimbing || PossibleJump()))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(jumpForce, ForceMode.Impulse);

            coyoteTimer = 0f;
            isClimbing = false;
            climbLockTimer = climbLock;
        }

        if (!inParkour)Look();
    }

    void FixedUpdate()
    {
        Climbing();
        /*if (!GameController.gameController.isPlaying || !canMove || isInvestigating)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
        }
        else*/
        if (isClimbing)
        {
            Climb();
        }
        else if (inParkour)
        {
            Parkour();
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

    private void Parkour()
    {
        float h = Input.GetAxisRaw("Horizontal");

        isMoving = h != 0;

        rb.AddForce(gravityForce, ForceMode.Force);

        Vector3 velocidadeAlvo = sideParkour * h * walkSpeed;
        rb.linearVelocity = new Vector3(velocidadeAlvo.x, rb.linearVelocity.y, velocidadeAlvo.z);

        

        //Vira para o lado em que anda
        if (h != 0)
        {
            Quaternion alvo = Quaternion.LookRotation(sideParkour * h, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, alvo, rotateSpeed * Time.deltaTime);
            //transform.forward = sideParkour * h;
        }
    }

    IEnumerator EnterParkur()
    {
        defaltCamera.gameObject.SetActive(!defaltCamera.gameObject.activeSelf);
        parkourCamera.gameObject.SetActive(!parkourCamera.gameObject.activeSelf);
        yield return new WaitForSeconds(0.5f);
        inParkour = !inParkour;
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
            StartCoroutine(EnterParkur());
            //defaltCamera.gameObject.SetActive(!defaltCamera.gameObject.activeSelf);
            //parkourCamera.gameObject.SetActive(!parkourCamera.gameObject.activeSelf);
            //inParkour = !inParkour;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.y >= deadlySpeed)
        {
            transform.position = GameController.gameController.lastSave;
        }

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