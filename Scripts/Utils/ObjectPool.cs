using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiVR

{
    public class ObjectPool : MonoBehaviour
    {
        //prefab that the pool will use
        public GameObject poolPrefab;

        //initial number of elements
        public int initialNum = 10;

        //collection
        List<GameObject> pooledObjects;

        //initialize pool
        void Awake()
        {
            if (pooledObjects == null)
                InitPool();
        }

        //create a new object
        GameObject CreateObject()
        {
            //create a new object
            GameObject newObj = Instantiate(poolPrefab);

            //set object to inactive
            newObj.SetActive(false);

            //add to list
            pooledObjects.Add(newObj);

            //return obj
            return newObj;
        }

        //retreive an object from the pool
        public GameObject GetObj()
        {
            // search our list for an inactive object
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                //if we find one
                if (!pooledObjects[i].activeInHierarchy)
                {
                    // if we find one, set active 
                    pooledObjects[i].SetActive(true);
                    // and return it
                    return pooledObjects[i];
                }
            }

            // increate our pool (create new obj)
            GameObject newObj = CreateObject();

            //enable that obj
            newObj.SetActive(true);


            //return that obj
            return newObj;
        }

        //init pool
        public void InitPool()
        {
            //initiate list
            pooledObjects = new List<GameObject>();

            //create initial number of objects
            for (int i = 0; i < initialNum; i++)
            {
                //create a new object.
                CreateObject();
            }

        }
        //get all active objects
        public List<GameObject> GetAllActive()
        {

            List<GameObject> activeObjects;
            activeObjects = new List<GameObject>();

            for (int i = 0; i < pooledObjects.Count; i++)
            {
                if (pooledObjects[i].activeInHierarchy)
                {
                    activeObjects.Add(pooledObjects[i]);
                }
            }
            return activeObjects;
        }
    }
}