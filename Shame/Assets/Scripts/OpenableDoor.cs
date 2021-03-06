﻿using UnityEngine;
using System.Collections;

public class OpenableDoor : MonoBehaviour {
	
	public float closedAngle = 0;
	public float openedAngle = 90;
	public float doorSwingSmoothingTime = 0.5f;
	public float doorSwingMaxSpeed = 90;
	public AudioClip openAudio;
	public AudioClip closeAudio;
	
	private float targetAngle;
	private float currentAngle;
	private float currentAngularVelocity;

	GameObject player;                          // Reference to the player GameObject.
	
	bool enter;
	
	void Awake ()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () 
	{
		if (currentAngle != targetAngle) {
			UpdateAngle ();
			UpdateRotation ();
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject == player) {
			if(Input.GetKeyDown("f")) {
				ToggleDoor ();
			}
		}
	}
	
	public void ToggleDoor ()
	{
		Debug.Log ("Door Toggled");
		if (targetAngle == openedAngle) {
			audio.clip = closeAudio;
			audio.Play (55555);	
			targetAngle = closedAngle;
		} else {
			audio.clip = openAudio;
			audio.Play ();
			targetAngle = openedAngle;
		}

	}
	
	void UpdateAngle ()
	{
		currentAngle = Mathf.SmoothDamp (currentAngle, 
		                                 targetAngle, 
		                                 ref currentAngularVelocity, 
		                                 doorSwingSmoothingTime, 
		                                 doorSwingMaxSpeed);
	}
	
	void UpdateRotation ()
	{
		transform.localRotation = Quaternion.AngleAxis (currentAngle, Vector3.up);
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log ("Player enter the door trigger");
		if (other.gameObject.tag == "Player") {
			enter = true;
		}
	}
	
	void OnTriggerExit (Collider other){
		Debug.Log ("Player exit the door trigger");
		if (other.gameObject.tag == "Player") {
			enter = false;
		}
	}
	
	void OnGUI() {
		if (enter) {
			string message;
			if (targetAngle == closedAngle)
				message = "Press 'F' to open the door";
			else
				message = "Press 'F' to close the door";
			GUI.Label (new Rect (Screen.width / 2 - 75, Screen.height - 100, 200, 50), message);

		}
	}
}
