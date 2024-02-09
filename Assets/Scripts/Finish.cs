using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private bool levelComplate = false;
    // Start is called before the first frame update
   private void Start()
    {
        finishSound=GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" !=levelComplate==true)
        {
            finishSound.Play();
            levelComplate=true;
            Invoke("ComplateLevel",2f);
            // ComplateLevel(); //bölümü anında bitirir
        }
    }

    private void ComplateLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

   
}
