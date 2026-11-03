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

    private PlayerRB player;
    public CinemachineCamera cam;
    
    void Start()
    {
        //cineBrain = GetComponent<CinemachineBrain>();
        
        Invoke("Initialize", .2f);
    }

    private void Initialize()
    {
        player = GameController.gameController.player;
    }



    public void InvestigationCamera(Transform clueFocus)
    {
        //cam.Target.TrackingTarget = clueFocus;
        defaultForward = transform.forward;
        defaultPosition = transform.position;
        //cineBrain.enabled = false;
        transform.position = clueFocus.position;
        transform.forward = clueFocus.forward;

    }

    public void NormalCamera()
    {
        //cam.Target.TrackingTarget = player.transform;
        transform.forward = defaultForward;
        transform.position = defaultPosition;
        //cineBrain.enabled = true;
    }



}
