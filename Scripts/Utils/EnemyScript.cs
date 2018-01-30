using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiVR;

namespace KaiVR
{
    public class EnemyScript : MonoBehaviour
    {

        public float speed = 0.5f;
        public float chaseDistance = 20;
        public float angularSpeed = 1;
        Rigidbody rb;
        Animator anim;



        //avail states
        enum State { idle, attacking, dead };

        //curr state
        State currentState = State.idle;

        //player
        Playercontroller player;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            anim = GetComponentInChildren<Animator>();
            player = FindObjectOfType<Playercontroller>();
            if (!player)
            {
                Debug.LogError("no player controller found");
            }

            //search at interval
            InvokeRepeating("LookForPlayer", 0, 0.5f);
        }


        void LookForPlayer()
        {
            //only look if idle
            if (currentState != State.idle) return;

            //check distance
            if (Vector3.Distance(player.transform.position, transform.position) < chaseDistance)
            {
                currentState = State.attacking;

                //activate attack
                anim.SetBool("sawPlayer", true);


                //stop looking for player
                CancelInvoke();
                //print("saw player");
                //print(Vector3.Distance(player.transform.position, transform.position));



            }



        }
        void FixedUpdate()
        {
            // move the parent to the position of the child (the model)
            transform.position = transform.GetChild(0).position;

            // set the child to be in the origin of the parent
            transform.GetChild(0).localPosition = Vector3.zero;

            // only chase if we are attacking!
            if (currentState != State.attacking) return;

            // instant rotation of the transform:
            transform.LookAt(player.transform.position);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bullet"))
            {
                //change state
                currentState = State.dead;


                //set animation
                anim.SetBool("isAlive", false);


                //disable collider
                GetComponent<Collider>().enabled = false;
                rb.isKinematic = true;
            }

        }

    }
}