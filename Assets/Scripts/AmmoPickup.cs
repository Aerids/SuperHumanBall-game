using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int amount = 1;

    private void OnTriggerEnter(Collider other)
    {
        var growShoot = other.GetComponent<PlayerGrowShoot>();
        if (growShoot == null) return;

        growShoot.AddAmmo(amount);
        Destroy(gameObject);
    }
}
