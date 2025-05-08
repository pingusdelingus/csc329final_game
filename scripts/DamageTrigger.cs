using UnityEngine;
using UnityEngine.SceneManagement;

public class DamageTrigger : MonoBehaviour
{
    private float spawnTime;
    public float immuneTime = 0.5f;

    [Tooltip("Who this trigger is supposed to damage: Player or Enemy")]
    public string targetTag = "Player";

    void Start()
    {
        spawnTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time - spawnTime < immuneTime) return;

        if (other.CompareTag("GHOST") || other.gameObject.layer == LayerMask.NameToLayer("GHOST"))
            return;

        if (other.CompareTag(targetTag))
        {
            Debug.Log($"{targetTag} killed by DamageTrigger on {gameObject.name}");
            SceneManager.LoadScene("gameover");
        }
    }
}