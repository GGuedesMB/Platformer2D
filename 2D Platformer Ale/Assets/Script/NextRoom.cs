using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextRoom : MonoBehaviour
{
    [SerializeField] Transform nextPosition;

    public int enemies;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() && enemies <= 0)
        {
            collision.transform.position = nextPosition.position;
        }
    }
}
