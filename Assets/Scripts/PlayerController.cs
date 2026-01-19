using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    private Vector3 desiredVelocity;

    public float maxSpeed = 6f;
    public float acceleration = 20f;
    public float deceleration = 25f;

    public float jumpHeight = 6f;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(h, 0f, v);
        if (input.sqrMagnitude > 1f) input.Normalize();

        desiredVelocity = input * maxSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(
                rb.linearVelocity.x,
                jumpHeight,
                rb.linearVelocity.z  
            );
        }
    }
    void FixedUpdate()
    {
        Vector3 vel = rb.linearVelocity;
        Vector3 horizontal = new Vector3(vel.x, 0f, vel.z);

        float accel = desiredVelocity.sqrMagnitude > 0.001f ? acceleration : deceleration;

        Vector3 newHorizontal = Vector3.MoveTowards(horizontal, desiredVelocity, accel * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(newHorizontal.x, vel.y, newHorizontal.z);
    }

    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        isGrounded = false;
    }

}
