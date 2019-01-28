using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector2 cameraSpeedCoefficients = new Vector2(0.5f, 0.5f);

    private Vector3 offset;

    private Vector2 lastPosition;
    private bool shouldCameraChangePosition = false;
    private PlayerController playerController;
    private Rigidbody2D playerRigidBody;
    private Rigidbody2D cameraRigidBody;

    private void Awake()
    {
        lastPosition = player.transform.position;
        playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerController = player.GetComponent<PlayerController>();
        cameraRigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if(transform.position != player.transform.position)
        {
            cameraRigidBody.velocity = cameraSpeedCoefficients * playerRigidBody.velocity;
        }
        else
        {
            cameraRigidBody.velocity = Vector2.zero;
        }
    }

    private void LateUpdate()
    {
        //if(shouldCameraChangePosition)
        //{
        //    transform.position = player.transform.position + offset;
        //    shouldCameraChangePosition = false;
        //}
    }
}
