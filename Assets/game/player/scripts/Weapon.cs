using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    bool canShoot = true;

    // Update is called once per frame
    void Update()
    {
        
        if (Pause.IsPause == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
    }

    void Shoot ()
    {
        if (Pause.IsPause == false)
        {
            // shooting logic
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void ButtonShoot()
    {
        // shooting logic
        if (Pause.IsPause == false && canShoot == true)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        canShoot = false;
        yield return new WaitForSeconds(0.1f);
        canShoot = true;
    }
}
