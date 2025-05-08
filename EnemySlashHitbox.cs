using UnityEngine;

public class EnemySlashHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Enemy slash hit the player!");

            // Access the player's controller
            PlayerAnimationController player = other.GetComponent<PlayerAnimationController>();
            if (player != null)
            {
                player.StartCoroutine("DieAfterDelay");  // Or call KillPlayer directly
            }
        }
    }
}
