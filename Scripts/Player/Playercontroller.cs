using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiVR;

namespace KaiVR
{
    [RequireComponent(typeof(PlayerCollectableController))]
    [RequireComponent(typeof(PlayerFreeTeleportController))]
    public class Playercontroller : MonoBehaviour
    {

        public GunController gun;

        PlayerCollectableController playerCollect;
        PlayerFreeTeleportController playerTeleport;


        void Awake()
        {
            playerCollect = GetComponent<PlayerCollectableController>();
            playerTeleport = GetComponent<PlayerFreeTeleportController>();
        }

        private void OnEnable()
        {
            playerCollect.OnCollect += Handlecollection;
        }
        private void OnDisable()
        {
            playerCollect.OnCollect -= Handlecollection;
        }

        private void Handlecollection(GameObject item)
        {
            gun.Recharge(item.GetComponent<CollectableController>().GetProperty("ammo"));

        }

        void Update()
        {

            //check we are not selecting

            if (playerCollect.IsSelecting() || playerTeleport.IsSelecting()) return;

            if (Input.GetButtonDown("Fire1"))
            {
                if (gun.CanShoot())
                {
                    gun.Shoot();
                }
            }

        }
    }
}