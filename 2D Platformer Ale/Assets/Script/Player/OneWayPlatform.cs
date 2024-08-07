using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] float disableTime;
    GameObject platform;
    BoxCollider2D playerCollider;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (platform != null && Input.GetAxis("Vertical") < 0)
        {
            StartCoroutine(DisableCollision());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWay"))
        {
            platform = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWay"))
        {
            platform = null;
        }
    }

    IEnumerator DisableCollision()
    {
        BoxCollider2D platformCollider = platform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider, platformCollider);
        yield return new WaitForSeconds(disableTime);
        Physics2D.IgnoreCollision(playerCollider, platformCollider, false);
    }
}
