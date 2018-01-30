using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiVR
{
    public class BulletController : MonoBehaviour
    {

        //lifespan
        public float lifespan = 3;

        private void OnEnable()
        {
            //set a timer
            Invoke("DeactivateBullet", lifespan);
        }

        void DeactivateBullet()
        {
            gameObject.SetActive(false);
        }

        void OnDisable()
        {
            //cancel invoke
            CancelInvoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
        }

    }
}