using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public float maxTiltDegrees = 12f;     
    public float tiltSpeed = 6f;           

    public float inputDeadzone = 0.05f;

    float currentX; 
    float currentZ; 

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal"); 
        float v = Input.GetAxisRaw("Vertical");   

        if (Mathf.Abs(h) < inputDeadzone) h = 0f;
        if (Mathf.Abs(v) < inputDeadzone) v = 0f;

        float targetX = -v * maxTiltDegrees;
        float targetZ = h * maxTiltDegrees;

        currentX = Mathf.Lerp(currentX, targetX, 1f - Mathf.Exp(-tiltSpeed * Time.deltaTime));
        currentZ = Mathf.Lerp(currentZ, targetZ, 1f - Mathf.Exp(-tiltSpeed * Time.deltaTime));

        transform.localRotation = Quaternion.Euler(currentX, 0f, currentZ);
    }
}
