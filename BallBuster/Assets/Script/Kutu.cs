using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kutu : MonoBehaviour
{

    [SerializeField] GameManager _GameManager;

    public void EfektOynat()
    {
        _GameManager.KutuParcalanmaEfekt(transform.position);
        gameObject.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
