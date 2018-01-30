using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiVR
{
    public class PlayerCollectableController : MonoBehaviour
    {
        //flag if you are selecting item
        bool isSelecting = false;

        //max distance you can collect
        public float maxDistance;

        //event for when item collected
        //1. delegate
        public delegate void OnCollectedEventHandler(GameObject item);
        //2. create event
        public event OnCollectedEventHandler OnCollect;

        // called when collecting item
        public void Collect(GameObject item)
        {
            if (OnCollect != null)
            {
                if (Vector3.Distance(transform.position, item.transform.position) <= maxDistance)
                {
                    print("got it");
                    OnCollect(item);
                    isSelecting = false;
                }
            }
        }

        //called when selecting item
        public void SelectionOver()
        {
            isSelecting = true;
        }
        //called when deselecting item
        public void SelectionOut()
        {
            isSelecting = false;

        }

        //return if we are selecting (encapsulation)
        public bool IsSelecting()
        {
            return isSelecting;
        }
    }
}