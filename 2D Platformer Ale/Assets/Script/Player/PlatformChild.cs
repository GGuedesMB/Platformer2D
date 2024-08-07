using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChild : MonoBehaviour
{
    MovingPlatform platform;
    Player player;
    [SerializeField] float momentumLosePerSecond;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatform>() && player.isGrounded)
        {
            platform = collision.gameObject.GetComponent<MovingPlatform>();
            transform.parent = platform.transform;
            speed = 0f;
        }
        else if (collision.gameObject.GetComponent<VerticalPlatform>())
        {
            transform.parent = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<MovingPlatform>() && transform.parent != null)
        {
            speed = platform.speed;
            StartCoroutine(MomentumLose());
            platform = null;
            transform.parent = null;
        }
        else if (collision.gameObject.GetComponent<VerticalPlatform>())
        {
            transform.parent = null;
        }
    }

    IEnumerator MomentumLose()
    {
        while (Mathf.Abs(speed) > 0.01f)
        {
            speed += Mathf.Sign(speed) * -1 * momentumLosePerSecond * Time.deltaTime;
            yield return null;
        }
    }
}
