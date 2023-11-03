using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class GameManager : MonoBehaviour
{

    [Serializable]   // bu classi listelebilirim artik demek
    public class Hedefler
    {
       
        public Sprite HedefGorsel;
        public int TopDegeri;
        public GameObject GorevTamam;
        public string HedefTuru;
    }


    [Serializable]   // bu classi listelebilirim artik demek
    public class Hedefler_UI
    {
        public GameObject Hedef;
        public Image HedefGorsel;
        public TextMeshProUGUI HedefDegerText;
        public GameObject GorevTamam;
      
    }

    public AudioSource[] sesler;
    public GameObject KazanmaEkrani;

    [Header("---LEVEL AYARLAR")]
    public Sprite[] SpriteObjeleri;
    [SerializeField] private GameObject[] Toplar;
    [SerializeField] private TextMeshProUGUI KalanTopSayisiText;
    int KalanTopSayisi;
    int HavuzIndex;

    [Header("---DIGER OBJELER")]
    [SerializeField] private ParticleSystem PatlamaEfekt;
    [SerializeField] private ParticleSystem[] KutuKirilmaEfektleri;
    int KutuKirilmaEfektIndex;

    [Header("---TOP ATIS SISTEMI")]
    [SerializeField] private GameObject TopAtici;
    [SerializeField] private GameObject TopSoketi;
    [SerializeField] private GameObject GelecekTop;
    GameObject SeciliTop;

    [Header("---GOREV ISLEMLERI")]
    [SerializeField] private List<Hedefler> Hedeflerr;
    [SerializeField] private List<Hedefler_UI> Hedeflerr_UI;
    int TopDegeri, KutuDegeri, ToplamGorevSayisi;
    bool KutuHedefiVarmi;
    public bool TopHedefiVarmi;



    void Start()
    {
        KalanTopSayisi = Toplar.Length;
        TopGetir(true);
        ToplamGorevSayisi = Hedeflerr.Count;


        for (int i = 0; i < Hedeflerr.Count; i++)
        {
            Hedeflerr_UI[i].Hedef.SetActive(true);
            Hedeflerr_UI[i].HedefGorsel.sprite = Hedeflerr[i].HedefGorsel;
            Hedeflerr_UI[i].HedefDegerText.text = Hedeflerr[i].TopDegeri.ToString();
            if (Hedeflerr[i].HedefTuru == "Top")
            {
                TopHedefiVarmi = true;
                TopDegeri = Hedeflerr[i].TopDegeri;
            }else if (Hedeflerr[i].HedefTuru == "Kutu")
            {
                KutuHedefiVarmi = true;
                KutuDegeri = Hedeflerr[i].TopDegeri;
            }
        }

    }
    // 2-Kirmizi
    // 4-Sari
    // 8-Yesil
    // 16-Mavi
    // 32-Koyu Mavi
    // 64-Koyu Yesil
    // 128-Mor
    // 256-Turuncu
    // 512-Karisik Sari
    // 1024
    // 2048


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.CompareTag("OyunZemini"))
                {
                    Vector2 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    TopAtici.transform.position = Vector2.MoveTowards(TopAtici.transform.position, new Vector2(MousePosition.x, TopAtici.transform.position.y), 30 * Time.deltaTime);
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
        }

        if (Input.GetMouseButtonUp(0)) // parmagi mousedan cekersen
        {
            SeciliTop.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // top asaya dussun diye degistirdik
            SeciliTop.transform.parent = null; // top asaya duserken parenttan kurtulsun.
            SeciliTop.GetComponent<Top>().BirincilDurumDegistir();
            TopGetir(false);
        }

        
    }

    void TopGetir(bool IlkKurulum)
    {
        if (IlkKurulum)
        {
            Toplar[HavuzIndex].transform.SetParent(TopAtici.transform);
            Toplar[HavuzIndex].transform.position = TopSoketi.transform.position;
            Toplar[HavuzIndex].SetActive(true);
            SeciliTop = Toplar[HavuzIndex];

            HavuzIndex++;

            Toplar[HavuzIndex].transform.position = GelecekTop.transform.position;
            Toplar[HavuzIndex].SetActive(true);
            KalanTopSayisiText.text = KalanTopSayisi.ToString();
        }
        else
        {
            if (Toplar.Length != 0)
            {
                Toplar[HavuzIndex].transform.SetParent(TopAtici.transform);
                Toplar[HavuzIndex].transform.position = TopSoketi.transform.position;
                Toplar[HavuzIndex].SetActive(true);
                SeciliTop = Toplar[HavuzIndex];

                KalanTopSayisi--;
                KalanTopSayisiText.text = KalanTopSayisi.ToString();

                if(HavuzIndex != Toplar.Length - 1)
                {

                    HavuzIndex++;
                    Toplar[HavuzIndex].transform.position = GelecekTop.transform.position;
                    Toplar[HavuzIndex].SetActive(true);


                }
               

            }

            if(KalanTopSayisi == 0)
            {
                if (ToplamGorevSayisi == 0)
                    Kazandin();
                else
                    Kaybettin();
            }
        }
        
    }

    public void PatlamaEfekti(Vector2 Pozisyon)
    {
        PatlamaEfekt.gameObject.transform.position = Pozisyon;
        PatlamaEfekt.gameObject.SetActive(true);
        PatlamaEfekt.Play();
        sesler[0].Play();
    }

    public void KutuParcalanmaEfekt(Vector2 Pozisyon)
    {
        KutuKirilmaEfektleri[KutuKirilmaEfektIndex].gameObject.transform.position = Pozisyon;
        KutuKirilmaEfektleri[KutuKirilmaEfektIndex].gameObject.SetActive(true);
        PatlamaEfekt.Play();
        sesler[3].Play();


        if (KutuHedefiVarmi)
        {
            KutuDegeri--;
            if (KutuDegeri == 0)
            {
                Hedeflerr_UI[1].GorevTamam.SetActive(true);
            }
            ToplamGorevSayisi--;
            if (ToplamGorevSayisi == 0)
            {
                Kazandin();
            }
        }

        if (KutuKirilmaEfektIndex == KutuKirilmaEfektleri.Length - 1)
            KutuKirilmaEfektIndex = 0;
        else
            KutuKirilmaEfektIndex++;

    }

    public void GorevSayiKontrol(int sayi)
    {
        if (sayi == TopDegeri)
        {
            Hedeflerr_UI[0].GorevTamam.SetActive(true);

            ToplamGorevSayisi--;
            if (ToplamGorevSayisi == 0)
            {
                Kazandin();
            }

        }
    }

    void Kazandin()
    {
        Debug.Log("Kazandin");
        KazanmaEkrani.SetActive(true);
        
    }

    

    void Kaybettin()
    {
        Debug.Log("Kaybettin");
    }


}
