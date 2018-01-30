using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiVR;
using System;

namespace KaiVR
{
    [RequireComponent(typeof(ObjectPool))]
    public class GunController : MonoBehaviour
    {

        //gun setup
        public float ammo = 10;
        public float maxAmmo = 10;
        public float bulletSpeed = 50;

        public RectTransform ammoIndicator;

        //pool
        ObjectPool bulletPool;

        private void Awake()
        {
            bulletPool = GetComponent<ObjectPool>();

            bulletPool.InitPool();

            RefreshUI();
        }

        //return if we can shoot
        public bool CanShoot()
        {
            return ammo > 0;
        }

        //shoot a bullet

        public void Shoot()
        {
            //get bullet
            GameObject newBullet = bulletPool.GetObj();

            //place bullet
            newBullet.transform.position = transform.position;

            // get br of new Bullet
            Rigidbody rb = newBullet.GetComponent<Rigidbody>();
            //give it velocity
            rb.velocity = transform.forward * bulletSpeed;

            //decrease ammo
            ammo--;
            RefreshUI();
        }

        internal void Recharge(float amount)
        {
            //chack magazine size
            ammo = Mathf.Min(maxAmmo, (amount + amount));
            RefreshUI();
        }

        void RefreshUI()
        {

            ammoIndicator.localScale = new Vector3((float)ammo / maxAmmo, 1, 1);

        }

    }
}