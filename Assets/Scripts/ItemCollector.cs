using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemCollector : MonoBehaviour
{
    private int cherries = 6;
    public TextMeshProUGUI cherriesText;
    [SerializeField] private AudioSource collectionSoundsEffect;
    [SerializeField] private PlayerLife playerLife;
    
    

    private float countdownTime = 1f; // Her saniye bir azaltmak için 1 saniye olarak ayarlandı
    private bool isCountingDown = false;

    private void Start()
    {
        StartCountdown(); // Oyun başladığında geri sayımı başlat
        // Unity Inspector üzerinden referans alındıysa bu adımı atlayabilirsiniz
            if (playerLife == null)
            {
                playerLife = GetComponent<PlayerLife>();
            }
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cherry"))
        {
            Destroy(collision.gameObject);
            collectionSoundsEffect.Play();
            cherries++;
            UpdateCherriesText();
        }
    }

    private void UpdateCherriesText()
    {
        cherriesText.text = "Kalan Sure:" + cherries.ToString();
        
        // Cherry sayısı 0 olduğunda PlayerLife script'indeki Die metodu çağrılır.
        if (cherries == 0)
        {
            playerLife.Die();
        }
    }
   

    private void StartCountdown()
    {
        isCountingDown = true;
        StartCoroutine(CountdownCoroutine());
    }
    
    

    private IEnumerator CountdownCoroutine()
    {
        while (isCountingDown)
        {
            
            yield return new WaitForSeconds(countdownTime);
            cherries--;

            if (cherries <= 0)
            {
                cherries = 0; // Cherry sayısı negatif olmasın
                isCountingDown = false; // Geri sayımı durdur
            }

            UpdateCherriesText();
        }
    }

    
}