using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public Transform waypoint1;
    public Transform waypoint2;

    public float speed = 2f;
    // public float delayBetweenMovements = 1f;

    private Vector3 lastPosition;
    private Animator animator;
    private SpriteRenderer spriteRenderer; // SpriteRenderer bileşeni eklenmiştir
  

    private void Start()
    {
        lastPosition = transform.position;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer bileşenini al
     
        
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while (true)
        {
            // Platform waypoint1'e doğru hareket eder
            yield return MoveToWaypoint(waypoint1.position);
        

            // Belirli bir süre bekler
            // yield return new WaitForSeconds(delayBetweenMovements);

            // Platform waypoint2'ye doğru hareket eder
            yield return MoveToWaypoint(waypoint2.position);


            // Belirli bir süre bekler
            // yield return new WaitForSeconds(delayBetweenMovements);
        }
    }

    IEnumerator MoveToWaypoint(Vector3 targetPosition)
    {
        while (transform.position != targetPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            lastPosition = transform.position;
            float direction = Mathf.Sign(targetPosition.x - lastPosition.x);

            // Animasyon yönünü belirle
            SetAnimationDirection(direction);

            // Platformun X yönünde hareket ettiği durumu kontrol et ve Sprite'ın yönünü tersine çevir
            if (direction > 0f )
            {
                spriteRenderer.flipX = true; // Sağa gidiyorsa, flipX'i kapat
            }
            else if (direction < 0f )
            {
                spriteRenderer.flipX = false; // Sola gidiyorsa, flipX'i aç
            }

            yield return null;
        }

    }
     private void SetAnimationDirection(float direction)
    {
        // Animator bileşenini al
        Animator animator = GetComponent<Animator>();
        
        // Animasyon yönünü belirle
        // animator.SetFloat("Direction", direction);
    }
}