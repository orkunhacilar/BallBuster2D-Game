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

    bool Birincil;
    [SerializeField] private bool VarsayilanTop;

    // Start is called before the first frame update
    void Start()
    {
        SayiText.text = Sayi.ToString();

        if (VarsayilanTop)
            Birincil = true;
    }

    public void DurumuAyarla()
    {
        Birincil = true;
    }

    public void BirincilDurumDegistir()
    {
        Invoke("DurumuAyarla", 2f);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Sayi.ToString()) && Birincil) //ayni tagde baska top ile carpisirsa
        {
            BirlesmeEfekt.Play();
            _GameManager.sesler[1].Play();
            collision.gameObject.SetActive(false); //carpistigimiz topu kapat
            Sayi += Sayi; //sayiyi topla
            gameObject.tag = Sayi.ToString(); //topumun tagini guncelle
            SayiText.text = Sayi.ToString(); //topumun textini guncelle


            switch (Sayi)
            {
                case 4:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[1];
                    break;
                case 8:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[2];
                    break;
                case 16:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[3];
                    break;
                case 32:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[4];
                    break;
                case 64:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[5];
                    break;
                case 128:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[6];
                    break;
                case 256:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[7];
                    break;
                case 512:
                case 1024:
                case 2048:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[8];
                    break;
            }
            if (_GameManager.TopHedefiVarmi)
            {
                _GameManager.GorevSayiKontrol(Sayi);
            }

           
            Birincil = false;
            Invoke("DurumuAyarla", 2f);
        }

       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Sayi.ToString()) && Birincil) //ayni tagde baska top ile carpisirsa
        {
            _GameManager.sesler[1].Play();
            BirlesmeEfekt.Play();
            collision.gameObject.SetActive(false); //carpistigimiz topu kapat
            Sayi += Sayi; //sayiyi topla
            gameObject.tag = Sayi.ToString(); //topumun tagini guncelle
            SayiText.text = Sayi.ToString(); //topumun textini guncelle


            switch (Sayi)
            {
                case 4:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[1];
                    break;
                case 8:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[2];
                    break;
                case 16:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[3];
                    break;
                case 32:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[4];
                    break;
                case 64:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[5];
                    break;
                case 128:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[6];
                    break;
                case 256:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[7];
                    break;
                case 512:
                case 1024:
                case 2048:
                    _Renderer.sprite = _GameManager.SpriteObjeleri[8];
                    break;
            }
            if (_GameManager.TopHedefiVarmi)
            {
                _GameManager.GorevSayiKontrol(Sayi);
            }

            Birincil = false;
            Invoke("DurumuAyarla", 2f);
        }

       
    }



}
