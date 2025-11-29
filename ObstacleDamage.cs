using UnityEngine;

public class ObstacleDamage : MonoBehaviour
{
    [SerializeField] private int DamageAmount;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealthController>().TakeDamage(DamageAmount);
            print("hasar");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealthController>().TakeDamage(DamageAmount);
            print("hasar");

        }
    }
}
