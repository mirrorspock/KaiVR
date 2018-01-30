using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

namespace KaiVR
{

    [RequireComponent(typeof(VRInteractiveItem))]
    public class FreeTeleportationZone : MonoBehaviour
    {
        VRInteractiveItem vrItem;
        VREyeRaycaster vrEyeRaycaster;
        PlayerFreeTeleportController playerCtrl;

        void Awake()
        {
            vrItem = GetComponent<VRInteractiveItem>();

            vrEyeRaycaster = FindObjectOfType<VREyeRaycaster>();
            if (vrEyeRaycaster == null)
            {
                Debug.LogError("no EyeRaycaster found.");
            }

            playerCtrl = FindObjectOfType<PlayerFreeTeleportController>();
            if (playerCtrl == null)
            {
                Debug.LogError("no PlayerFreeTeleportController found.");
            }

        }

        void OnEnable()
        {
            vrEyeRaycaster.OnRaycasthit += HandleShowTarget;
            vrItem.OnOut += HandleOut;
            vrItem.OnClick += HandleClick;
        }

        void OnDisable()
        {
            vrEyeRaycaster.OnRaycasthit -= HandleShowTarget;
            vrItem.OnOut -= HandleOut;
            vrItem.OnClick -= HandleClick;
        }

        void HandleShowTarget(RaycastHit hit)
        {
            //check if we are looking at this object.
            if (!vrItem.IsOver) return;

            playerCtrl.ShowTarget(hit.point);
        }

        void HandleClick()
        {
            playerCtrl.Teleport();
        }

        void HandleOut()
        {
            playerCtrl.HideTarget();
        }

    }
}