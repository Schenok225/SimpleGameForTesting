using UnityEngine;

public class PlayerDamageCheck : MonoBehaviour
{
    [SerializeField] private float damageRadius = 1f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int damageAmount = 10;

    void Update()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, damageRadius, enemyLayer);

        foreach (var enemyCollider in hitEnemies)
        {
            var damageDealer = enemyCollider.GetComponent<TrapDamageDealer>();
            if (damageDealer != null)
            {
                var health = GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(damageAmount);
                }
            }
        }
    }
}
