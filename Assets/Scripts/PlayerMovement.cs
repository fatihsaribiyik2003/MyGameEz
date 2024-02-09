using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rb;
    // private Rigidbody rb;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D coll;
    [SerializeField] public float moveSpeed = 7f;
    float dirX =0f;
    private int jumpCount = 0;
    private Animator animator;
    private bool canCheckGround = true;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float jumpForce=14f;
    [SerializeField] private AudioSource jumpSoundEffect;

    private enum MovementState{idele ,jumping ,falling,running,doubleJumping }
    // Start is called before the first frame update
    private void Start()
    {
        coll =GetComponent<BoxCollider2D>();
        rb =GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // rb.interpolation = RigidbodyInterpolation.Interpolate; // veya .Extrapolate duvarın içinden geçme hatası için
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(jumpCount);
        // isGrounded = IsGrounded();
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        dirX =Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed ,rb.velocity.y);

        HareketEt(dirX);
        // Diğer karakter kontrolleri buraya eklenir
        // float horizontalInput = Input.GetAxis("Horizontal");

        // Zıplama tuşunu algıla
        
         // Karakterin yönünü kontrol et ve gerekirse çevir
        if (dirX > 0)
        {
            Dondur(false); // Sağa hareket ediyorsa flip etme
        }
        else if (dirX < 0)
        {
            Dondur(true); // Sola hareket ediyorsa flip etme
        }
        
        if (canCheckGround && IsGrounded())//zıpladığım an frameler çok hızlı olduğu için heemn 0 oluyo geri 
        {
            
            Debug.Log("is Grundun içindeyiz");
            if (jumpCount ==1)
            {
                jumpCount=1;

            }
            else{
                jumpCount=0;
            }
            jumpCount = 0; // Yerdeyken zıplama sayaçını sıfırla
            Debug.Log("isGround calisti" + jumpCount);
        }
        else{
            
        }
       
    

        if (canCheckGround && Input.GetKeyDown("space") && jumpCount < 2 )
        {
            Time.timeScale = 0.1f;
            jumpSoundEffect.Play();
            // animator.SetBool("jump",true);
            Jump();
        }
        else if (canCheckGround && Input.GetKeyDown("w") && jumpCount < 2 )
        {
            // animator.SetBool("jump",true);
            jumpSoundEffect.Play();

            Jump();
        }

        

       


         void HareketEt(float horizontalInput)
        {
            // Karakteri yatay eksende hareket ettir
            Vector2 movement = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            rb.velocity = movement;
        }
        void Dondur(bool flipX)
        {
            // SpriteRenderer'ın flipX özelliğini ayarla
            spriteRenderer.flipX = flipX;
        }    
    UpdateAnimationUpdate();
        
    }
    

    private void UpdateAnimationUpdate()
    {
        MovementState state;

        // Animator'daki "running" parametresi
        if (dirX != 0)
        {
            state = MovementState.running;
            // animator.SetBool("running", true);
        }
        else
        {
            state = MovementState.idele;

            // animator.SetBool("running", false);
        }

        if (jumpCount ==2 && rb.velocity.y < -.1f )
        {
            state =MovementState.falling;
            
        }
        else if (jumpCount == 2 )
        {
            state =MovementState.doubleJumping;
        }
        else if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state =MovementState.falling;
        }
        else
        {
            // Debug.Log("MovementState not calismiyor");
            //velositi 0 sa else girer
        }

        animator.SetInteger("state",(int)state);

    }
    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center,coll.bounds.size,0f,Vector2.down, .1f ,jumpableGround);
    }
    void Jump()
    {
    StartCoroutine(ResetTimeScaleAfterDelay(0.2f)); // 0.5 saniye sonra zaman ölçeğini sıfırla
    StartCoroutine(DisableGroundCheckForDelay(0.2f)); // 0.5 saniye boyunca isGrounded kontrolünü devre dışı bırak
    // Zıplama kodu buraya gelecek
    jumpCount++;
    Debug.Log(jumpCount +" ZIpla arttırıldıııııı");
    rb.velocity = new Vector3(rb.velocity.x,jumpForce,0);

    }
    IEnumerator ResetTimeScaleAfterDelay(float delay)
    {
       yield return new WaitForSeconds(delay);
       Time.timeScale = 1.0f; // Oyun hızını varsayılan değere geri getir
    }
    IEnumerator DisableGroundCheckForDelay(float delay)
    {
       canCheckGround = false;
       yield return new WaitForSeconds(delay);
      canCheckGround = true; // Belirli bir süre sonra isGrounded kontrolünü tekrar aktif hale getir
    }
    
}
