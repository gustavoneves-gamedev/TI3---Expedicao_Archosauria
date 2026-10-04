using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class Player : MonoBehaviour
{
    CharacterController cc;
    public float gravityValue = -9.81f;

    public bool isMoving; //Pública apenas para fins de testes
    public bool canMove = true;

    [Header("WALK")]
    public float rotateSpeed = 130f;
    public float walkSpeed = 10f;
    public float jumpHeight = 2.5f;
    public float fall;
    public float v; //Variável que controla o movimento "Vertical" do Player

    [Header("CLIMB")]
    public float climbSpeedV = 5f;
    public float climbSpeedH = 5f;
    public float obstacleMaxDistance = 0.6f;
    public float climbLock = 0.3f;
    private LayerMask obstacleLayer;
    [SerializeField] private bool isClimbing;
    private float climbLockTimer;

    public bool isInJumpMinigame;
    private float idleTime = 5f;
    
    

    void Start()
    {
        GameController.gameController.player = this;
        cc = GetComponent<CharacterController>();
        obstacleLayer = LayerMask.GetMask("Obstacle");
    }

    void Update()
    {
        if (!GameController.gameController.isPlaying || GameController.gameController.isPaused || !canMove) return;

        if (isInJumpMinigame)
        {
            SideMovement();

            if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
            {
                fall = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
                //isClimbing = false;
                //climbLockTimer = climbLock;
            }

            return;
        }

        bool obstacleAhead = Physics.Raycast(transform.position + Vector3.down * 1f, transform.forward, out RaycastHit obstacleHit, obstacleMaxDistance, obstacleLayer);

        if (climbLockTimer > 0f)
        {
            climbLockTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && (cc.isGrounded || isClimbing))
        {
            fall = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            isClimbing = false;
            climbLockTimer = climbLock;
        }
        else if (obstacleAhead && !isClimbing && climbLockTimer <= 0f)
        {
            isClimbing = true;
        }
        else if (isClimbing && !obstacleAhead)
        {
            isClimbing = false;
            climbLockTimer = climbLock;
        }

        if (isClimbing)
        {
            Climb();
        }
        else if (isInJumpMinigame)
        {
            SideMovement();
        }
        else
        {
            Movement();
        }

        DetectMovement();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameController.gameController.uiController.PauseGame();
        }
    }

    private void DetectMovement()
    {
        isMoving = cc.velocity.sqrMagnitude > 0.01f;

        if (cc.velocity.magnitude <= 0)
        {
            idleTime -= Time.deltaTime;

            if (idleTime < 0)
            {
                idleTime = 0;
                GameController.gameController.uiController.ShowControls(false);
            }
        }
        else
        {
            idleTime = 5;
            GameController.gameController.uiController.ShowControls(true);
        }
    }

    private void Movement()
    {
        if (cc.isGrounded && fall < 0f)
        {
            fall = -2f;
        }

        fall += gravityValue * Time.deltaTime;

        v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Horizontal");

        isMoving = v != 0 || r != 0;

        if (GameController.gameController.isInJumpMinigame && v < 0)
        {
            v = 0;
        }

        Vector3 dir = transform.forward * v * walkSpeed;
        dir.y = fall;

        transform.Rotate(0f, r * rotateSpeed * Time.deltaTime, 0f);

        cc.Move(dir * Time.deltaTime);
    }

    private void SideMovement()
    {
        if (cc.isGrounded && fall < 0f)
        {
            fall = -2f;
        }       

        fall += gravityValue * Time.deltaTime;

        v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Horizontal");

        
        isMoving = v != 0 || r != 0;

        Vector3 dir = transform.forward * -r * walkSpeed;
        dir.y = fall;

        //transform.Rotate(0f, r * rotateSpeed * Time.deltaTime, 0f);

        cc.Move(dir * Time.deltaTime);
    }

    private void Climb()
    {
        fall = 0f;

        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Horizontal");

        isMoving = v != 0 || r != 0;

        Vector3 dir = transform.up * v * climbSpeedV + transform.right * r * climbSpeedH;
        cc.Move(dir * Time.deltaTime);
    }

    public void ResetForward()
    {
        cc.enabled = false;

        transform.forward = Vector3.back;

        cc.enabled = true;
    }
}