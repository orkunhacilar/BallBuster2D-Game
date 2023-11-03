using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Bomba : MonoBehaviour
{


    [SerializeField] private int Sayi;
    [SerializeField] TextMeshProUGUI SayiText;
    [SerializeField] GameManager _GameManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Sayi.ToString()))
        {
            GucUygula();
        }
    }

    List<Collider2D> colliders = new List<Collider2D>();

    void GucUygula()
    {
        //TEMASA GECENLERI DIKKATE AL SADECE GIBI YAZDIK
        var contactFilter2D = new ContactFilter2D
        {
            useTriggers = true
        };

        Physics2D.OverlapBox(transform.position, transform.localScale * 2, 20f, contactFilter2D, colliders);
        // bu carpisma icerisine neleri almak istedigimi soyluyorum -> contactFilter2D ve bunlarida colliders listine aktar diyoruz.

        _GameManager.PatlamaEfekti(transform.position);
        gameObject.SetActive(false);

        foreach (var item in colliders)
        {
            //yukarda colliders icine zaten etkilesime gecicegim objeleri toplamistim simdi onlarin hepsine bomba efekti uygulamak icin guc uyguluyorum.
            item.gameObject.GetComponent<Rigidbody2D>().AddForce(90 * new Vector2(0, 6), ForceMode2D.Force);
        }

    }



}
