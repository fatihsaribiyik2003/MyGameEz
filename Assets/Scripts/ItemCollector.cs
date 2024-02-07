using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    public TextMeshProUGUI cherriesText;
    
    // [SerializeField] private Text cherriesText;
    [SerializeField] private AudioSource collectionSoundsEffect;
    private void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.gameObject.CompareTag("Cherry"))
        {
        
            Destroy(collison.gameObject);
            collectionSoundsEffect.Play();
            cherries ++;
            // Debug.Log("Cherries:" + cherries);
            // cherriesText.text ="Cherries: " +cherries;

            UpdateCherriesText(); // Text nesnesini güncelle
        
        }
    }

    private void UpdateCherriesText()
    {
        cherriesText.text = "Cherries: " + cherries.ToString();
    }



}
