using UnityEngine;

public interface IDamageable {
    void OnHit(float damage, Vector3 hitpoint, Vector3 hitNormal);
}

