using UnityEngine;
using System.Collections;       
using UnityEngine.SceneManagement;



public class PlayerAnimationController : MonoBehaviour
{

    public GameObject slashHitboxGO;

    void KillPlayer()
    {
        SceneManager.LoadScene("GameOver");

    }
    IEnumerator ActivateHitbox()
    {
        slashHitboxGO.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        slashHitboxGO.SetActive(false);
    }

        private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
            animator = GetComponent<Animator>();
    
    }

    // Update is called once per frame
    void Update()
    {
         if (Input.GetKeyDown(KeyCode.E))
    {
        animator.SetTrigger("Slsh");
        StartCoroutine(ActivateHitbox());
    }
    if (Input.GetKeyDown(KeyCode.R)){
    
    animator.SetTrigger("dead");
    StartCoroutine(DieAfterDelay());
    }

       float horizontal = Mathf.Abs(Input.GetAxisRaw("Horizontal")); // A/D or Left/Right arrows
        animator.SetFloat("Speed", horizontal);        
    
        // flip character logic    
        if (Input.GetAxisRaw("Horizontal") < 0)
        transform.localScale = new Vector3(-1.846432f, 2.344479f, 0);  // Face left
    else if (Input.GetAxisRaw("Horizontal") > 0)
        transform.localScale = new Vector3(1.846432f, 2.344479f, 0);   // Face right
    }

    IEnumerator DieAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("gameover");

    }
}


