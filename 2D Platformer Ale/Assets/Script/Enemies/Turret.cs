using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] float roundCooldown;
    [SerializeField] int shotsPerRound;
    [SerializeField] float fireRate;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShootingRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(roundCooldown);
            for (int i = 0; i < shotsPerRound; i++)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                yield return new WaitForSeconds(fireRate);
            }
        }
    }
}
