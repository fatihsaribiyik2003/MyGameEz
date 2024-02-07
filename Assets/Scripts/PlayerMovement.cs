using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using TMPro;

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
        // isGrounded = IsGrounded();
        transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
        dirX =Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed ,rb.velocity.y);

        if (IsGrounded())
        {
            jumpCount = 0; // Yerdeyken zıplama sayaçını sıfırla
        }
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
        UpdateAnimationUpdate();
        
       
    

        if (Input.GetKeyDown("space") && jumpCount < 1)
        {
            jumpSoundEffect.Play();
            // animator.SetBool("jump",true);
            rb.velocity = new Vector3(rb.velocity.x,jumpForce,0);
            Jump();
        }
        else if (Input.GetKeyDown("w") && jumpCount < 1)
        {
            // animator.SetBool("jump",true);
            jumpSoundEffect.Play();

            rb.velocity = new Vector3(rb.velocity.x,jumpForce,0);
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

        if (jumpCount ==1 && rb.velocity.y < -.1f )
        {
            state =MovementState.falling;
            
        }
        else if (jumpCount == 1)
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
    // Zıplama kodu buraya gelecek

    jumpCount++;
}
}
