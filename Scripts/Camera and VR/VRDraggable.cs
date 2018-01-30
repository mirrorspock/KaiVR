using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRStandardAssets.Utils;

namespace KaiVR {
	[RequireComponent(typeof(VRInteractiveItem))]
	public class VRDraggable : MonoBehaviour {

		//distance from camera when dragging
		public float cameraDistance = 3;

		//Max distance for grabbing
		public float maxGrabdistance = 4;

		// event for when dragging starts
		public event Action OnDrag;

		// event for when dragging ends
		public event Action OnDrop;

		//VR interactive item component
		VRInteractiveItem vrInteractive;

		// states
		enum State {Ready, Dragging, Blocked};

		// current state
		State currState;

		//original position should be saved
		Vector3 initialPos;


		//original rotatiojn
		Quaternion initialRotation;

		//keep track of whether something is being dragged
		static bool isDragging = false;


		void Awake() {
			//grab VR int. item component
			vrInteractive = GetComponent<VRInteractiveItem> ();

			//default state is ready
			currState = State.Ready;
		
			//save initial position
			initialPos = transform.position;

			//save initla rotation
			initialRotation = transform.rotation;
		}


		void OnEnable(){
			vrInteractive.OnClick += HandleClick;
		}

		void OnDisable(){
			vrInteractive.OnClick -= HandleClick;
		}


		void HandleClick(){
			//check state if its ready and we are not dragging
			if (currState == State.Ready && !VRDraggable.isDragging) {
				
				float dist = Vector3.Distance (transform.position, Camera.main.transform.position);

				//check grabdistance
				if (dist > maxGrabdistance)
					return;
				//start dragging
				currState = State.Dragging;
				VRDraggable.isDragging = true;
				if (OnDrag != null) 
					// trigger event
					OnDrag ();


//				print ("start dragging");
			} else if (currState == State.Dragging) {
		
				currState = State.Ready;
				VRDraggable.isDragging = false;
				if (OnDrop != null) 
					// trigger event
					OnDrop ();
				
//				print ("dropped");

			}
		}

		void Update(){
			//when dragging
			if (currState == State.Dragging) {
				//grab camera
				Transform camTransf = Camera.main.transform;

				//Set position Attribute cameradistance
				transform.position = camTransf.position + camTransf.forward * cameraDistance;

				//make it face us
				transform.LookAt(camTransf.position);
			}
		}
		//send item back to original position
		public void SendToOriginalPosition(){
			transform.position = initialPos;
			transform.rotation = initialRotation;
		}

		public void ToggleBlock(bool isBlock){
			if (isBlock) {
				currState = State.Blocked;
			} else {
				currState = State.Ready;
			}
		}
	}
}