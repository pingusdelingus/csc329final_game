using UnityEngine;
using UnityEngine.SceneManagement;
public class backtommscript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void backToMenu()
    {
        Debug.Log("going back to main menu");
        SceneManager.LoadScene("menu"); // loads main

    }// end of back to main menu 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
