using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed = 20;
    [SerializeField] int damage = 2;
    [SerializeField] GameObject impactVFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.GetComponent<Coin>() || other.gameObject.GetComponent<Health>())
        {
            return;
        }

        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            other.GetComponent<EnemyHealth>().ApplyDamage(damage);
        }
        //Debug.Log("My layerMask: " + gameObject.layer);
        //Debug.Log("Collider: " + other.gameObject.name);
        Instantiate(impactVFX, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
