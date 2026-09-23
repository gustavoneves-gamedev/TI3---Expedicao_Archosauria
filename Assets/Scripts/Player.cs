using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController cc;
    public float speed = 15f;
    public float rotateSpeed = 1f;
    public float jumpHeight = 1.5f;
    public float gravityValue = -9.81f;
    public float fall;
    public bool isMoving; //Pública apenas para fins de testes


    private float idleTime = 5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameController.gameController.player = this;
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.gameController.isPlaying || GameController.gameController.isPaused) return;

        Movement();
        DetectMovement();

    }

    private void DetectMovement()
    {
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

        if (cc.isGrounded && Input.GetButtonDown("Jump"))
        {
            fall = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        }

        fall += gravityValue * Time.deltaTime;

        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Horizontal");

        if(v != 0 || r != 0) isMoving = true;
        else isMoving = false;

            Vector3 dir = transform.forward * v * speed;
        dir.y = fall;

        transform.Rotate(0f, r * rotateSpeed, 0f);
        cc.Move(dir * Time.deltaTime);

        
    }

    


}
