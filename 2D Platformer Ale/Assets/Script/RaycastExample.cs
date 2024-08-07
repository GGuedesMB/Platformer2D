using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastExample : MonoBehaviour
{
    [SerializeField] float groundCheckOffset;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float rayLength;
    [SerializeField] LayerMask raylayer;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Groundcheck();
        Debug.Log(Physics2D.Raycast(transform.position, transform.right, rayLength, raylayer).collider);
        Debug.DrawRay(transform.position, transform.right * rayLength, Color.red);
    }

    void Groundcheck()
    {
        Vector2 origin = transform.position + Vector3.down * groundCheckOffset;
        if (Physics2D.BoxCast(origin, groundCheckSize, 0f, Vector2.down, groundCheckSize.y, groundLayer).collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireCube(transform.position + Vector3.down * groundCheckOffset, Vector3.one * groundCheckSize);   
    }
}
