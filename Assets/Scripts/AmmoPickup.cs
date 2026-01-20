using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int amount = 1;
    public float pickupDelay = 0f;   // 0 for level pickups, >0 for shot ammo

    bool canPickup = true;

    void Start()
    {
        if (pickupDelay > 0f)
        {
            canPickup = false;
            Invoke(nameof(EnablePickup), pickupDelay);
        }
    }

    void EnablePickup()
    {
        canPickup = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canPickup) return;

        var growShoot = other.GetComponent<PlayerGrowShoot>();
        if (growShoot == null) return;

        growShoot.AddAmmo(amount);

        Destroy(transform.root.gameObject);
    }
}
