using UnityEngine;

public class muteSound : MonoBehaviour
{
    private bool isMuted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.M))
       {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        Debug.Log("Audio Muted : " + isMuted);
       } 
    }
}
