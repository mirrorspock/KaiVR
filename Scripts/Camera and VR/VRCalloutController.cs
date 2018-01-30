using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

namespace KaiVR
{

//make requirement 
[RequireComponent(typeof(VRInteractiveItem))]
public class VRCalloutController : MonoBehaviour {

	// canvas that will be shown or hidden
	public Canvas canvas;

	public bool isInitiallyVisible = false;

	//activate by hovering?
	public bool isHoverActivated = false;

	//activate by clicking?
	public bool isClickActivated = false;

	VRInteractiveItem vrInteractive;

	// Use this for initialization
	void Awake () {
		vrInteractive = GetComponent<VRInteractiveItem> ();
		canvas.enabled = isInitiallyVisible;
	}
	void ShowCanvas(){
		canvas.enabled = true;
	}

	void HideCanvas(){
		canvas.enabled = false;
	}

	void ToggleCanvas(){
		canvas.enabled = !canvas.enabled;
	}


	void OnEnable () {
		if (isHoverActivated) {
			vrInteractive.OnOver += ShowCanvas;
			vrInteractive.OnOut += HideCanvas;

		}
		if (isClickActivated) {
			vrInteractive.OnClick += ToggleCanvas;
		}

	}

	void OnDisable () {
		if (isHoverActivated) {
			vrInteractive.OnOver -= ShowCanvas;
			vrInteractive.OnOut -= HideCanvas;

		}
		if (isClickActivated) {
			vrInteractive.OnClick -= ToggleCanvas;
		}

	}

}
}