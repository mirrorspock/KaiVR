using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaiVR
{
    public class PlayerFixedTeleportationcontroller : MonoBehaviour
    {

        public FixedTeleportationPadController currentPad;

        //teleport range
        public float Range;

        //refreshrate
        public float refreshRate = 0;

        //list all the Pads
        List<GameObject> teleportNetwork;

        void Start()
        {
            if (refreshRate > 0)
            {
                InvokeRepeating("RefreshPads", 0, refreshRate);

            }
            else
            {

                RefreshPads();
            }

        }




        public void Teleport(FixedTeleportationPadController pad)
        {
            //pad position
            Vector3 padPos = pad.gameObject.transform.position;

            float diffY = padPos.y - currentPad.gameObject.transform.position.y;

            //move to teleportation pad
            transform.position = new Vector3(padPos.x, transform.position.y + diffY, padPos.z);
            //update new currentpad
            currentPad = pad;


            //make parent of Pad, parent of player

            transform.parent = currentPad.gameObject.transform.parent;



            RefreshPads();
        }

        //shows pads in range
        void RefreshPads()
        {
            //transform of pad
            Transform pad;

            for (int i = 0; i < teleportNetwork.Count; i++)
            {
                //assign pad
                pad = teleportNetwork[i].transform;
                //				Debug.Log ("Distance" + i + " is " + Vector3.Distance (pad.position, transform.position));

                //check distance
                if (Vector3.Distance(pad.position, transform.position) <= Range)
                {
                    pad.gameObject.SetActive(true);
                    //					DrawLine (transform.position, pad.position, Color.red);
                }
                else
                {
                    pad.gameObject.SetActive(false);
                }


            }

            //only activate near pads
            // Physics.OverlapSphere only works with active objects..





            //hide current pad
            currentPad.gameObject.SetActive(false);

        }

        public void AddTeleportationPad(GameObject pad)
        {
            print(teleportNetwork);

            if (teleportNetwork == null)
                teleportNetwork = new List<GameObject>();

            teleportNetwork.Add(pad);
        }



        //		void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 5f)
        //		{
        //			Vector3 raiseY = new Vector3(0,0.5f,0);
        //			GameObject myLine = new GameObject();
        //			myLine.transform.position = start;
        //			myLine.AddComponent<LineRenderer>();
        //			LineRenderer lr = myLine.GetComponent<LineRenderer>();
        //			lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        //			lr.SetColors(color, color);
        //			lr.SetWidth(0.1f, 0.1f);
        //			lr.SetPosition(0, start+raiseY);
        //			lr.SetPosition(1, end);
        //			GameObject.Destroy(myLine, duration);
        //		}


    }
}