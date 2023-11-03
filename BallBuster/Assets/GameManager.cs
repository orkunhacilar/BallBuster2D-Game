using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{

    [Header("---LEVEL AYARLAR")]
    public Sprite[] SpriteObjeleri;
    [SerializeField] private GameObject[] Toplar;
    [SerializeField] private TextMeshProUGUI KalanTopSayisiText;
    int KalanTopSayisi;
    int HavuzIndex;

    [Header("---DIGER OBJELER")]
    [SerializeField] private ParticleSystem PatlamaEfekt;

    [Header("---TOP ATIS SISTEMI")]
    [SerializeField] private GameObject TopAtici;
    [SerializeField] private GameObject TopSoketi;
    [SerializeField] private GameObject GelecekTop;
    GameObject SeciliTop;


    void Start()
    {
        KalanTopSayisi = Toplar.Length;
        TopGetir(true);
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

                if(HavuzIndex == Toplar.Length - 1)
                {
                    Debug.Log("Bitti");
                }
                else
                {
                    HavuzIndex++;
                    Toplar[HavuzIndex].transform.position = GelecekTop.transform.position;
                    Toplar[HavuzIndex].SetActive(true);
                }

            }
        }
        
    }

    public void PatlamaEfekti(Vector2 Pozisyon)
    {
        PatlamaEfekt.gameObject.transform.position = Pozisyon;
        PatlamaEfekt.gameObject.SetActive(true);
        PatlamaEfekt.Play();
    }
}
