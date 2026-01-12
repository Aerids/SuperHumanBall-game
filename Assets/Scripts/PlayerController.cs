using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;

    public int jumpHeight = 1;

    public Transform groundCheck;
    public float groundDistance = 0.25f;
    public LayerMask groundLayer;
    private bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.x);

            
        }
    }
}
