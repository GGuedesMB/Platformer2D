using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Life life;
    [SerializeField] GameObject[] loot;
    [SerializeField] GameObject deathExplosion;
    [SerializeField] NextRoom nextRoom;

    // Start is called before the first frame update
    void Start()
    {
        life = GetComponent<Life>();

        nextRoom.enemies++;
    }

    // Update is called once per frame
    void Update()
    {
        Death();
    }

    void Death()
    {
        if (life.GetHealth() <= 0)
        {
            nextRoom.enemies--;
            Instantiate(deathExplosion, transform.position, transform.rotation);

            int rand = Random.Range(0, loot.Length);
            if (loot[rand])
            {
                Instantiate(loot[rand], transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
