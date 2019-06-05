using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Touch;

public class billboard : MonoBehaviour {
	public Transform target;
	public Vector3 originalSize;
	public Vector3 originalParentSize;
	public Vector3 sclfactor;
	public Vector3 vec;
	public Camera mainCam;
	// Use this for initialization
	void Start () {
		mainCam = Camera.main;
		originalSize = this.transform.localScale;
//		originalParentSize = this.transform.parent.localScale;
		//set your target here
	}
	
	// Update is called once per frame

	void Update () {
		if(mainCam != null){
			
//			sclfactor = originalParentSize - this.transform.parent.localScale;
			vec = mainCam.transform.position - transform.position;

			vec.x = vec.z = 0.0f;
			transform.LookAt (mainCam.transform.position -vec);
//			transform.position = target.position;
			transform.rotation = mainCam.transform.rotation;
//			transform.rotation = Quaternion.Euler (mainCam.transform.rotation.x,mainCam.transform.rotation.y,0f);//camera.main.transforn.rotation
		}

		
	}
//	public void Update(){
//		if (mainCam != null) {
//			transform.LookAt (transform.position + mainCam.transform.rotation * Vector3.forward, mainCam.transform.rotation * Vector3.forward);
//		}
//	}



}
