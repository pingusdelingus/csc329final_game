using UnityEngine;

public class InversionTrigger : MonoBehaviour
{
    public InversionController inversion;
    public bool canInvert = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canInvert) return;

        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            StartCoroutine(inversion.TriggerInversion());
            canInvert = false; // lock it
        }
    }
}