using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

namespace KaiVR
{
    [RequireComponent(typeof(VRInteractiveItem))]
    public class CollectableController : MonoBehaviour
    {

        //serializable class for ANY item stat (health, coins, ammo ect)
        [Serializable]
        public class ItemStat
        {
            public string label;
            public float amount;
        }
        // flexible array of properties
        public ItemStat[] properties;



        VRInteractiveItem vrItem;
        PlayerCollectableController playerCollect;

        void Awake()
        {
            vrItem = GetComponent<VRInteractiveItem>();

            //get player coll ctl.
            playerCollect = FindObjectOfType<PlayerCollectableController>();
            if (playerCollect == null)
            {
                Debug.LogError("missing PlayerCollectableController");
            }

        }

        private void OnEnable()
        {
            vrItem.OnOver += HandleOver;
            vrItem.OnOut += HandleOut;
            vrItem.OnClick += HandleClick;
        }

        private void OnDisable()
        {
            vrItem.OnOver -= HandleOver;
            vrItem.OnOut -= HandleOut;
            vrItem.OnClick -= HandleClick;
        }

        void HandleClick()
        {
            //collect the item
            playerCollect.Collect(gameObject);


            //destroy it (for consumables)
            Destroy(gameObject);
        }

        void HandleOut()
        {
            playerCollect.SelectionOut();
            //remove element
            Highlight(false);
        }

        void HandleOver()
        {
            if (Vector3.Distance(transform.position, playerCollect.gameObject.transform.position) <= playerCollect.maxDistance)
            {
                //the player coll contr. knows we are selecting
                playerCollect.SelectionOver();
                //highlight element
                print("highlight");
                Highlight(true);
            }

        }

        void Highlight(bool flag)
        {
            GetComponent<Renderer>().material.SetFloat("_Outline", flag ? 0.002f : 0f);

        }

        public float GetProperty(string label)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                if (properties[i].label == label)
                {
                    return properties[i].amount;
                }
            }
            return 0;
        }

    }
}