using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Staff : MonoBehaviour
{
    [SerializeField] Transform aim;

    [Header("Shooting")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float reloadTime;

    Vector3 screenPos;
    Vector3 worldPos;

    bool reloading = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        screenPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        aim.position = new Vector3(worldPos.x, worldPos.y, 0);

        Shooting();
    }

    private void Shooting()
    {
        if (Input.GetButtonDown("Fire1") && !reloading)
        {
            Vector2 dist = aim.position - firePoint.position;
            Vector2 dir = dist.normalized;

            float angle = Vector2.Angle(Vector2.right, dir);

            if (dir.y < 0)
            {
                angle = -angle;
            }

            firePoint.eulerAngles = new Vector3(0, 0, angle);

            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            reloading = true;
            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
