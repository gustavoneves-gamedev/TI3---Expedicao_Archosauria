using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraController : MonoBehaviour
{

    private CinemachineBrain cineBrain;

    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    public Vector3 defaultForward;
    public Vector3 defaultPosition;

    public bool shouldFollow;
    
    public Transform defaultCameraPosition;
    
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

    private void Update()
    {
        if (!shouldFollow) return;
        
        transform.position = defaultCameraPosition.position;
        transform.rotation = defaultCameraPosition.rotation;
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
