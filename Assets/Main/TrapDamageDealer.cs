using UnityEngine;

public class TrapDamageDealer : MonoBehaviour
{
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private string targetTag = "Player";

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(targetTag))
        {
            var health = collision.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}
