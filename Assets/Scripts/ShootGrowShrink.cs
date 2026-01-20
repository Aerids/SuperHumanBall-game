using UnityEngine;

public class PlayerGrowShoot : MonoBehaviour
{
    [Header("Inventory")]
    public int startingAmmo = 2;
    public int maxAmmo = 999;
    public int ammo;

    [Header("Scale")]
    public float scalePerItem = 0.12f;          
    public float minScaleMultiplier = 0.6f;     
    public float maxScaleMultiplier = 3.0f;     

    [Header("Shooting")]
    public Transform shootPoint;                
    public GameObject projectilePrefab;         
    public float shootForce = 18f;
    public float shootCooldown = 0.15f;
    public KeyCode shootKey = KeyCode.Mouse0;


    [Header("Mass Multiplier")]
    public bool autoUpdateMass = true;          
    public float baseMass = 1f;
    public float massPerItem = 0.25f;

    Vector3 baseScale;
    Rigidbody rb;
    float nextShootTime;

    void Awake()
    {
        baseScale = transform.localScale;
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        ammo = Mathf.Clamp(startingAmmo, 0, maxAmmo);
        ApplyScaleFromAmmo();
    }

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
            TryShoot();
    }

    public void AddAmmo(int amount)
    {
        if (amount <= 0) return;

        ammo = Mathf.Clamp(ammo + amount, 0, maxAmmo);
        ApplyScaleFromAmmo();
    }

    public bool TryShoot()
    {
        if (Time.time < nextShootTime) return false;
        if (ammo <= 0) return false;
        if (projectilePrefab == null || shootPoint == null) return false;

        nextShootTime = Time.time + shootCooldown;

        ammo--;
        ApplyScaleFromAmmo();

        GameObject proj = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        Rigidbody projRb = proj.GetComponent<Rigidbody>();
        if (projRb != null)
        {
            projRb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        }

        return true;
    }

    void ApplyScaleFromAmmo()
    {

        float multiplier = 1f + ammo * scalePerItem;

        multiplier = Mathf.Clamp(multiplier, minScaleMultiplier, maxScaleMultiplier);

        transform.localScale = baseScale * multiplier;

        if (autoUpdateMass && rb != null)
        {
            rb.mass = Mathf.Max(0.01f, baseMass + ammo * massPerItem);
        }
    }
}
