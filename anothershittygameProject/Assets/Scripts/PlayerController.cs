using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private RaycastHit2D[] raycastHitBuffer;
    private ContactFilter2D contactFilter;
    public float maxSpeed = 7;
    public float force = 25;
    public float dropRate = 0.5f;
    public int maxCollisionsCount = 16;

    private bool isDownForceSetted = false;
    private float shell = 0.01f;

    private bool isGrounded = true;
    private bool IsGrounded
    {
        get => isGrounded;
        set
        {
            if (value != isGrounded)
            {
                isGrounded = value;
                Debug.Log($"IsGround now {value}");
                if (value)
                {
                    isDownForceSetted = false;
                }
            }
        }
    }
    public LayerMask whatIsGround;

    private Rigidbody2D rigidBody;
    private SpriteRenderer spriteRenderer2D;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer2D = GetComponent<SpriteRenderer>();
        contactFilter = new ContactFilter2D()
        {
            layerMask = this.whatIsGround,
            useLayerMask = true
        };
        raycastHitBuffer = new RaycastHit2D[maxCollisionsCount];
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        int downCount = Cast(Vector2.down, shell);
        if (downCount > 0)
        {
            IsGrounded = true;
        }
        float x = Input.GetAxis("Horizontal");
        int leftCount = Cast(Vector2.left, shell * 2);
        if (leftCount > 0)
        {
            foreach (var rayHit in raycastHitBuffer.Take(leftCount))
            {
                if (rayHit.normal.x == 1 && x < 0)
                {
                    Jump();
                    return;
                }
            }
        }
        int rightCount = Cast(Vector2.right, shell * 2);
        if (rightCount > 0)
        {
            foreach (var rayHit in raycastHitBuffer.Take(rightCount))
            {
                if (rayHit.normal.x == -1 && x > 0)
                {
                    Jump();
                    return;
                }
            }
        }
        var velocity = rigidBody.velocity;
        velocity.x = x * maxSpeed;
        rigidBody.velocity = velocity;
        Jump();
    }

    private void Jump()
    {
        float jump = Input.GetAxis("Vertical");
        if (jump > 0 && IsGrounded)
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, force);
            IsGrounded = false;
        }
    }

    private int Cast(Vector2 direction, float distance)
    {
        return rigidBody.Cast(
            direction,
            contactFilter,
            raycastHitBuffer,
            distance);
    }
}

