using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    public GameObject projectilePrefab;
    public GameObject projectilePrefab2;

    public Transform firePoint;

    Camera cam;
    bool isSpecialWeapon = false;

    // Start is called before the first frame update
    void Start()
    {
      cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
          
        }

        if(Input.GetKeyDown(KeyCode.Z))
        {
            ChangeWeapon();
        }

    }

    void Shoot()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);
        Vector3 direction = (targetPoint - firePoint.position).normalized;

        if (isSpecialWeapon)
        {
            GameObject proj2 = Instantiate(projectilePrefab2, firePoint.position, Quaternion.LookRotation(direction));
            return;
        }
        else
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.LookRotation(direction));
        }
    }

    void ChangeWeapon()
    {
        isSpecialWeapon = !isSpecialWeapon;
    }
}
