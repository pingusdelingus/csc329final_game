using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AudioSource musicSource;


    void Start()
    {
         if (SceneManager.GetActiveScene().name == "menu")
        {
            musicSource.loop = true;
            musicSource.Play();
        }
        else
        {
            Destroy(gameObject); // Kill self if not in menu
        }



    }// end of start method

    void Awake()
    {
        if (musicSource == null) {musicSource = GetComponent<AudioSource>();}

        if (Instance != null && Instance != this ||  SceneManager.GetActiveScene().name != "menu")
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}