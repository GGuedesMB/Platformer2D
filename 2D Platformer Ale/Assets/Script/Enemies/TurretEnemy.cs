using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : MonoBehaviour
{
    [SerializeField] float cooldown;
    [SerializeField] int numberOfShots;
    [SerializeField] float firerate;
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
            // Sequência de tiros
            for (int i = 0; i < numberOfShots; i++)
            {
                Debug.Log("Shoot!");
                yield return new WaitForSeconds(firerate);
            }

            // Intervalo de tempo
            yield return new WaitForSeconds(cooldown);
        }
    }
}
