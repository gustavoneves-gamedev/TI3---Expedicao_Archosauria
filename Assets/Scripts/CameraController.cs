using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{

    private CinemachineBrain cineBrain;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    private Vector3 defaultForward;
    private Vector3 defaultPosition;    
    private bool isSetted;
    private bool backToNormal;
    public float moveSpeed = 5f;

    private Player player;

    
    void Start()
    {
        cineBrain = GetComponent<CinemachineBrain>();
        Invoke("Initialize", .2f);
    }

    private void Initialize()
    {
        player = GameController.gameController.player;
    }


    void Update()
    {
        if (GameController.gameController.isInJumpMinigame && !GameController.gameController.shouldCameraFollow)
        {
            MinigameJumpCamera(moveSpeed * 10f * player.v * Time.deltaTime);
        }
        else if (GameController.gameController.shouldCameraFollow)
        {
            transform.position = target.position;
        }
        else
        {
            if (cineBrain.isActiveAndEnabled) return;

            //MinigameJumpCamera(40f * Time.deltaTime);

            BackToNormalCamera(40f * Time.deltaTime);

            if (!backToNormal)
            {
                Invoke("NormalCamera", 1.25f);
                backToNormal = true;
            }
            //NormalCamera();
        }
    }



    public void MinigameJumpCamera(float input = 0f)
    {

        if (cineBrain.isActiveAndEnabled)
        {
            cineBrain.enabled = false;
            backToNormal = false;
        }

        if (!isSetted)
        {
            defaultPosition = transform.position;
            defaultForward = transform.forward;
            isSetted = true;
        }

        if (input > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, input );
            transform.forward = Vector3.MoveTowards(transform.forward, target.forward, input / 25f );
        }
        else if (input < 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, defaultPosition, -input);
            transform.forward = Vector3.MoveTowards(transform.forward, defaultForward, -input / 25f );
            //backToNormal = false;
        }
    }

    public void BackToNormalCamera(float input = 0f)
    {

        //if (cineBrain.isActiveAndEnabled)
        //{
        //    cineBrain.enabled = false;
        //    backToNormal = false;
        //}

        
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position + offset, input);
            transform.forward = Vector3.MoveTowards(transform.forward, player.transform.forward, input / 20f);
            //backToNormal = false;
        }
    }

    private void NormalCamera()
    {
        if (cineBrain.isActiveAndEnabled) return;

        cineBrain.enabled = true;
        isSetted = false;
        GameController.gameController.player.canMove = true;
    }
}
