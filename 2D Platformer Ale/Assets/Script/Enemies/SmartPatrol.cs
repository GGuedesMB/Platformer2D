using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartPatrol : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Transform rayOrigin;
    [SerializeField] float rayLength;
    [SerializeField] LayerMask groundLayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HoleCheck();
        Movement();
    }

    void Movement()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    void HoleCheck()
    {
        Debug.DrawRay(rayOrigin.position, Vector2.down * rayLength, Color.red);
        if (Physics2D.Raycast(rayOrigin.position, Vector2.down, rayLength, groundLayer).collider == null)
        {
            transform.Rotate(0, 180, 0);
            //Debug.Log("Turn");
        }
    }
}
