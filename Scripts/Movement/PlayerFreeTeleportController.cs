using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

namespace KaiVR
{
    public class PlayerFreeTeleportController : MonoBehaviour
    {

        public GameObject target;
        public float maxDistance;

        bool isShowing;
        Reticle reticle;


        LineRenderer lineRend;
        public bool showArc;
        public int numArcPoints;
        Vector3[] arcPoints;
        public GameObject arcOrigin;

        private void Awake()
        {

            HideTarget();

            reticle = FindObjectOfType<Reticle>();
            if (showArc)
            {
                lineRend = target.GetComponent<LineRenderer>();
                //set # of points
                lineRend.positionCount = numArcPoints;

                arcPoints = new Vector3[numArcPoints];

                if (lineRend == null)
                {
                    Debug.LogError("missing line renderer on target");
                }
            }
        }

        public void HideTarget()
        {
            if (reticle != null)
            {
                reticle.Show();
            }
            target.SetActive(false);
            isShowing = false;
        }

        //show target

        public void ShowTarget(Vector3 position)
        {
            if (Vector3.Distance(position, transform.position) <= maxDistance)
            {
                if (!target.activeInHierarchy)
                {
                    target.SetActive(true);
                }
                if (reticle != null)
                {
                    reticle.Hide();
                }
                target.transform.position = position;

                isShowing = true;
                if (showArc)
                {
                    DrawRay();
                }
            }
            else if (isShowing)
                HideTarget();
        }



        public void Teleport()
        {
            if (isShowing)
            {
                transform.position = target.transform.position;
                HideTarget();
            }
        }


        void DrawRay()
        {
            // starting position for first point
            Vector3 startPoint = arcOrigin.transform.position;

            //end position
            Vector3 endPoint = target.transform.position;

            //create all points untill end
            //arc effect

            float arcY;

            for (int i = 0; i < numArcPoints; i++)
            {
                //curvature
                arcY = Mathf.Sin(((float)i / numArcPoints) * Mathf.PI) / 2;


                //create point
                arcPoints[i] = Vector3.Lerp(startPoint, endPoint, (float)i / numArcPoints);
                arcPoints[i].y += arcY;
                //print(arcPoints[i]);
            }


            // assign points to renderere
            lineRend.SetPositions(arcPoints);

        }

        //return if we are selecting (encapsulation)
        public bool IsSelecting()
        {
            return isShowing;
        }
    }
}