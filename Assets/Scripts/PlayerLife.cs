using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerLife : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private AudioSource deathSoundEffect;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        
    }

     private void OnCollisionEnter2D(Collision2D collision)
    {
        // OnCollisionEnter kodları buraya gelecek
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
        
    }
    private void Die()
    {
        anim.SetTrigger("Death");
        deathSoundEffect.Play();
        // Rigidbody2D'yi etkisizleştir
        //rb.isKinematic = true; OnKeyboardInput //yön ile hareket ediyo hala
      // İsterseniz diğer ölümle ilgili işlemleri de burada ekleyebilirsini  z
        // Rigidbody2D'yi statik yap
        rb.bodyType = RigidbodyType2D.Static;
    }
    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
