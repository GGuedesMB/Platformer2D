using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] Transform min;
    [SerializeField] Transform max;
    [SerializeField] float speed;

    bool isFlipped;

    float xMax;
    float xMin;
    // Start is called before the first frame update
    void Start()
    {
        xMax = max.position.x;
        xMin = min.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);

        if (transform.position.x >= xMax && !isFlipped)
        {
            //speed *= -1;
            isFlipped = true;
            transform.Rotate(0, 180, 0);
        }
        else if (transform.position.x <= xMin && isFlipped)
        {
            //speed *= -1;
            isFlipped = false;
            transform.Rotate(0, 180, 0);
        }
    }

}
