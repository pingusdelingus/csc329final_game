using UnityEngine;

public class SlashHitbox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
            Debug.Log("Trigger with: " + other.tag); 


    if (!other.CompareTag("Enemy")) return;

            Debug.Log("Hit enemy: " + other.name);
            
            Destroy(other.gameObject); 
    }
}