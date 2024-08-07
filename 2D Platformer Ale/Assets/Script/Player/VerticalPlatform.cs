using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    public float speed;
    [SerializeField] float movingLength;
    Vector2 minPosY;
    Vector2 maxPosY;

    // Start is called before the first frame update
    void Start()
    {
        minPosY = transform.position + Vector3.down * movingLength / 2;
        maxPosY = transform.position + Vector3.up * movingLength / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= maxPosY.y && speed > 0)
        {
            speed *= -1;
        }
        else if (transform.position.y <= minPosY.y && speed < 0)
        {
            speed *= -1;
        }

        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(minPosY, maxPosY);
    }
}
