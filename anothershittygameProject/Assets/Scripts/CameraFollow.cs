using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer background;

    public float smooth = 2.5f;
    private Transform player;
    private Camera cam;
    private Vector3 min, max, direction;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cam = GetComponent<Camera>();

        CalculateBounds();
    }

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        direction = player.right;
        Vector3 position = player.position + direction * smooth;
        position.z = transform.position.z;
        position = MoveInside(position, new Vector3(min.x, min.y, position.z), new Vector3(max.x, max.y, position.z));
        transform.position = Vector3.Lerp(transform.position, position, smooth * Time.deltaTime);
    }

    private Vector3 MoveInside(Vector3 current, Vector3 pMin, Vector3 pMax)
    {
        if (background == null) return current;
        current = Vector3.Max(current, pMin);
        current = Vector3.Min(current, pMax);
        return new Vector3(current.x, 1, current.z);
    }

    public void CalculateBounds()
    {
        if (background == null) return;
        Bounds bounds = Camera2DBounds();
        min = bounds.max + background.bounds.min;
        max = bounds.min + background.bounds.max;
    }

    Bounds Camera2DBounds()
    {
        float height = cam.orthographicSize * 2;
        return new Bounds(Vector3.zero, new Vector3(height * cam.aspect, height, 0));
    }
}
