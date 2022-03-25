using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   
    [SerializeField]
    float speed;

    [SerializeField]
    float keepAlive;
       

    [SerializeField]
    Spider spider;

    void Start()
    {
       // Destroy(gameObject, keepAlive);
    }


    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("base"))
        {
          //  Destroy(gameObject);
          spider.AddBullet(transform);
          gameObject.SetActive(false);
        }
    }



    public void SetSpider(Spider spider)=> this.spider = spider;
}
