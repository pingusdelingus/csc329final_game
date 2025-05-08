using UnityEngine;
using UnityEngine.SceneManagement;
public class tryAgainScrpt : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void TryAgainbtn()
    {
        Debug.Log("loading game scene");
        SceneManager.LoadScene("Level1");

    }// end of try again
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
