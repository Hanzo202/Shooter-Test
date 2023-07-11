using Canvas;
using GameSystems;
using Projectile_Space;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using static UnityEditor.PlayerSettings;


namespace WeaponSystem 
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponData weaponData;
        [SerializeField] private Transform firePoint;

        private float timeSinceLastShot;
        private float currentAmmo;
        private Camera mainCam;

        private CanvasController canvasController;
        private ProjectileSpawner projectileSpawner;

        [Inject]
        private void Construct(CanvasController canvasController, ProjectileSpawner projectileSpawner)
        {
            this.canvasController = canvasController;
            this.projectileSpawner = projectileSpawner;
        }

        private void Start()
        {
            currentAmmo = weaponData.MagSize;
            canvasController.OnAmmoTextChanged(currentAmmo, weaponData.MagSize);
            mainCam = Camera.main;
        }

        public void Reload()
        {
            StartCoroutine(RechargeCoroutine());
        }

        private IEnumerator RechargeCoroutine()
        {
            canvasController.OnReloadTextChanged();
            yield return new WaitForSeconds(weaponData.ReloadTime);
            currentAmmo = weaponData.MagSize;
            canvasController.OnReloadTextChanged();
            canvasController.OnAmmoTextChanged(currentAmmo, weaponData.MagSize);
        }

        private bool TimeBetweenShots()
        {
            return !weaponData.Reloading && timeSinceLastShot <= 0;
        }

        public void Shooting()
        {
            if (currentAmmo <= 0)
            {
                Reload();
                return;
            }

            if (!TimeBetweenShots())
            {
                return;
            }


            if (Physics.Raycast(mainCam.transform.position, mainCam.transform.forward, out RaycastHit hitInfo))
            {
                Vector3 dir =  hitInfo.point - firePoint.transform.position ;
                Projectile projectile = projectileSpawner.Spawn();
                projectile.StartPos();
                projectile.transform.SetPositionAndRotation(firePoint.transform.position, firePoint.transform.rotation);
                if (projectile.ProjectileDate.IsRaycast)
                {
                    projectile.GetComponent<RaycastProjectile>().StartRaycast(hitInfo);
                }
                else
                {
                    projectile.GetComponent<PhysicsProjectile>().AddForce(dir);
                }                
            }
            currentAmmo--;
            canvasController.OnAmmoTextChanged(currentAmmo, weaponData.MagSize);
            timeSinceLastShot = weaponData.FireRate;
        }

        private void Update()
        {
            if (timeSinceLastShot > 0)
            {
                timeSinceLastShot -= Time.deltaTime;
            }
        }
    }
}


