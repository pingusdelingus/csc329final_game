using UnityEngine;

public class FinishLineTrigger : MonoBehaviour
{
    public InversionController inversion;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            StartCoroutine(inversion.TriggerInversion());

            //Destroy(gameObject); // Prevent repeat triggering
        }
    }
}