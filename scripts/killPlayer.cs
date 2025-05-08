using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class killPlayer : MonoBehaviour
{

        public int Respawn;
        public AudioClip deathSound;
        private AudioSource audiosrc;
        
        private playerMovement movment;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       audiosrc = GetComponent<AudioSource>(); 
       audiosrc.dopplerLevel = 1.0f;
    }// end of start

    // Update is called once per frame
    void Update()
    {
        
    }// end of update


    private void OnTriggerEnter2D(Collider2D other)
    {
// Restart the current scene
            if (other.CompareTag("Player")){
                    movment = other.gameObject.GetComponent<playerMovement>();

                    if (movment != null){
                        movment.enabled = false;
                        Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
                        if (rb != null){
                            rb.linearVelocity = Vector2.zero;
                            rb.bodyType = RigidbodyType2D.Static;
                        }
                    }


                    StartCoroutine(PlayDeathSoundAndRespawn());
                
            }
    }// end of method

IEnumerator PlayDeathSoundAndRespawn()
    {
        if (deathSound != null && audiosrc != null)
        {
            audiosrc.PlayOneShot(deathSound);
            Debug.Log("Death sound played");

           yield return new WaitForSeconds(deathSound.length - 1); 
        }

        //change scene to gameover scene
        Debug.Log("loading game over screen");
        SceneManager.LoadScene("gameover");
        
    }// end of method

    
}
