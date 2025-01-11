using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    private bool _canShoot = true;
    
    private static InputActions _inputActions;
    
    private void Awake()
    {
        _inputActions = new InputActions(); 
        _inputActions.Player.Attack.performed += ctx =>
        {
            if (Pause.IsPause) return;
            Shoot();
        };
    }
    
    private void OnEnable() => _inputActions.Enable();

    private void OnDisable() => _inputActions.Disable();

    private void Shoot()
    {
        if (Pause.IsPause)
        {
            return;
        }
        
        // shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    public void ButtonShoot()
    {
        // shooting logic
        if (Pause.IsPause || !_canShoot)
        {
            return;
        }
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        _canShoot = false;
        yield return new WaitForSeconds(0.1f);
        _canShoot = true;
    }
}
