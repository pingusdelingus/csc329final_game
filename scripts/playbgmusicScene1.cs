using UnityEngine;
using UnityEngine.SceneManagement;

public class playbgmusicScene1 : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if( SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (audioSource != null)
            {
                audioSource.Play();
                Debug.Log("Audio Started in Scene 1 (Index 0)");
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
