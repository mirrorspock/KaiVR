using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;


namespace KaiVR
{
	[RequireComponent(typeof(VRInteractiveItem))]
	public class FixedTeleportationPadController : MonoBehaviour {

		//vr interactive item components
		VRInteractiveItem vrInteractive;

		// light
		Light padLight;

		//player fixed teleport controller
		PlayerFixedTeleportationcontroller playerTeleport;

		void Awake () {
			
			vrInteractive = GetComponent<VRInteractiveItem>();
			//		padLight = transform.GetChild (0).gameObject.GetComponents<Light>();
			padLight = GetComponentInChildren<Light>();
			padLight.gameObject.SetActive (false);

			//grab player fixed teloprt
			playerTeleport = FindObjectOfType<PlayerFixedTeleportationcontroller>();
			if (playerTeleport == null) {
				Debug.Log ("missing PlayerFixedTeleportationcontroller");
			}
			//add self to list.
			playerTeleport.AddTeleportationPad(gameObject);

		}

		void OnEnable(){
			//add events
			vrInteractive.OnOver += Highlight;
			vrInteractive.OnOut += Unhighlight;
			vrInteractive.OnClick += ClickPad;
		}
		void OnDisable(){
			vrInteractive.OnOver -= Highlight;
			vrInteractive.OnOut -= Unhighlight;
			vrInteractive.OnClick -= ClickPad;

		}

		void Highlight(){
			padLight.gameObject.SetActive (true);
		}
		void Unhighlight(){
			padLight.gameObject.SetActive (false);
		}
		void ClickPad(){
			print("one to transport");
			playerTeleport.Teleport (this);
		}




		void OnDrawGizmosSelected() {
			playerTeleport = FindObjectOfType<PlayerFixedTeleportationcontroller>();
			float range = playerTeleport.Range;
			Gizmos.color = Color.cyan;
			Gizmos.DrawWireSphere(transform.position, range);
		}


	}
}