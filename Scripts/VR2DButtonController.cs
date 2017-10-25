using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRStandardAssets.Utils;

namespace KaiVR
{

[RequireComponent(typeof(VRInteractiveItem))]

public class VR2DButtonController : MonoBehaviour {



	VRInteractiveItem vrInteractive;

	Button button;




	// Use this for initialization
	void Awake () {
		vrInteractive = GetComponent<VRInteractiveItem> ();

		button = GetComponentInChildren<Button> ();
	
		
	}

	void OnEnable(){
		vrInteractive.OnClick += button.onClick.Invoke;
	}
	void OnDisable(){
		vrInteractive.OnClick -= button.onClick.Invoke;

	}

}
}