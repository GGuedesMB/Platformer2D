using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;
    [SerializeField] float movingLength;
    Vector2 minPosX;
    Vector2 maxPosX;

    // Start is called before the first frame update
    void Start()
    {
        minPosX = transform.position + Vector3.left * movingLength / 2;
        maxPosX = transform.position + Vector3.right * movingLength / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= maxPosX.x && speed > 0)
        {
            speed *= -1;
        }
        else if (transform.position.x <= minPosX.x && speed < 0)
        {
            speed *= -1;
        }

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(minPosX, maxPosX);
    }
}
