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


    [Header("---TOP ATIS SISTEMI")]
    [SerializeField] private GameObject TopAtici;
    [SerializeField] private GameObject TopSoketi;
    [SerializeField] private GameObject GelecekTop;


    void Start()
    {
        KalanTopSayisi = Toplar.Length;
        TopGetir();
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

        
    }

    void TopGetir()
    {
        Toplar[HavuzIndex].transform.SetParent(TopAtici.transform);
        Toplar[HavuzIndex].transform.position = TopSoketi.transform.position;
        Toplar[HavuzIndex].SetActive(true);
    }
}
