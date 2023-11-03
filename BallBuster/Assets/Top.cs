using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Top : MonoBehaviour
{

    [SerializeField] private int Sayi;
    [SerializeField] TextMeshProUGUI SayiText;
    [SerializeField] GameManager _GameManager;
    [SerializeField] ParticleSystem BirlesmeEfekt;
    [SerializeField] SpriteRenderer _Renderer;

    // Start is called before the first frame update
    void Start()
    {
        SayiText.text = Sayi.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Sayi.ToString())) //ayni tagde baska top ile carpisirsa
        {
            BirlesmeEfekt.Play();
            collision.gameObject.SetActive(false); //carpistigimiz topu kapat
            Sayi += Sayi; //sayiyi topla
            gameObject.tag = Sayi.ToString(); //topumun tagini guncelle
            SayiText.text = Sayi.ToString(); //topumun textini guncelle
            //Sprite degisimi
        }
    }

}
