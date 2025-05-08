using UnityEngine;
using UnityEngine.SceneManagement;
public class playbutton : MonoBehaviour
{

    public void Play()
    {



        Debug.Log("loading game");
        SceneManager.LoadScene("Level1"); // loads main
    }// end of play

    public void Quit()
    {
        Application.Quit();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
