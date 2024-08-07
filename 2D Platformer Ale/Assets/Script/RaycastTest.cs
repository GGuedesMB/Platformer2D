using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTest : MonoBehaviour
{
    [SerializeField] Vector3 rayOffset;
    [SerializeField] float rayLength;
    [SerializeField] LayerMask rayLayer;
    [SerializeField] float boxWidth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + rayOffset, Vector2.down * rayLength, Color.red);
        //Debug.Log(Physics2D.Raycast(transform.position + rayOffset, Vector2.down, rayLength, rayLayer).collider);
        Debug.Log(Physics2D.BoxCast(transform.position + rayOffset, new Vector2(boxWidth, rayLength), 0, Vector2.down, rayLength, rayLayer).collider);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position + rayOffset, new Vector2(boxWidth, rayLength * 2));
    }
}
