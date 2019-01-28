using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector2 cameraSmoothCoefficients = new Vector2(0.05f, 0.05f);
    public Vector2 window = new Vector2(0.1f, 0.1f);

    private Vector3 offset;

    private Vector2 lastPosition;
    private bool shouldCameraChangePosition = false;
    private PlayerController playerController;
    private Rigidbody2D playerRigidBody;
    private Rigidbody2D cameraRigidBody;

    private Vector2 cameraVelocity = Vector2.zero;

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

    private void FixedUpdate()
    {
        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref cameraVelocity.x, cameraSmoothCoefficients.x);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref cameraVelocity.y, cameraSmoothCoefficients.y);

        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}
