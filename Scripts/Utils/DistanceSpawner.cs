using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KaiVR;

namespace KaiVR
{
    [RequireComponent(typeof(ObjectPool))]
    public class DistanceSpawner : MonoBehaviour
    {

        public Transform reference;

        public float genDistance = 20;
        public float minGap;
        public float maxGap;

        public float minScale = 1;
        public float maxScale = 1;
        //dir of spawner
        public Vector3 direction;

        //spanw / dissapeartime
        public float timeStep = 1;

        //object pool component
        ObjectPool pool;

        //newly gen obj
        GameObject newObj;

        void Awake()
        {
            //get the objectpool component
            pool = GetComponent<ObjectPool>();

            //init pool
            pool.InitPool();

            //initial pop of objects
            while (Vector3.Distance(reference.position, transform.position) <= genDistance)
            {
                HandleSpawning();
            }

            //execute spawning and hiding at frequency
            InvokeRepeating("HandleSpawning", 0, timeStep);
            InvokeRepeating("HandleHiding", 0, timeStep + 0.5f);
        }

        void Update()
        {

        }

        void HandleHiding()
        {
            //get active objects
            List<GameObject> actives = pool.GetAllActive();
            //loop all
            for (int i = 0; i < actives.Count; i++)
            {
                if (Vector3.Distance(reference.position, actives[i].transform.position) >= genDistance)
                {
                    actives[i].SetActive(false);
                }
            }


            //check distance

            //deactivate
        }

        void HandleSpawning()
        {
            if (Vector3.Distance(reference.position, transform.position) <= genDistance)
            {
                {
                    //spawn object
                    Spawn();

                    //reposition distance spawner
                    Reposition();
                }

            }
        }
        void Spawn()
        {
            //get obj from pool
            newObj = pool.GetObj();
            //gen random scale
            float scale = UnityEngine.Random.Range(minScale, maxScale);

            //set placement
            newObj.transform.position = transform.position;

            newObj.transform.localScale = Vector3.one * scale;

        }

        void Reposition()
        {
            //move spawner to next position
            //gap 
            float gap = UnityEngine.Random.Range(minGap, maxGap);
            float size = 0;

            //get renderer for size from center
            if (newObj.GetComponent<Renderer>() != null)
            {
                Vector3 directionFilter = Vector3.Scale(newObj.GetComponent<Renderer>().bounds.size, direction);
                size = Mathf.Max(directionFilter.x, directionFilter.y, directionFilter.z);

            }
            else if (newObj.GetComponentInChildren<Renderer>())
            {
                Vector3 directionFilter = Vector3.Scale(newObj.GetComponentInChildren<Renderer>().bounds.size, direction);
                size = Mathf.Max(directionFilter.x, directionFilter.y, directionFilter.z);

            }
            else
            {
                // no renderer found
            }

            //total distance to move
            float total = gap + size;

            transform.Translate(direction * total, Space.World);
        }

    }
}