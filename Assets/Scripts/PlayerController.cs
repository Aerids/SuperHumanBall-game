using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody rb;

    public int jumpHeight = 1;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpHeight, rb.linearVelocity.x);

            
        }
    }
}
