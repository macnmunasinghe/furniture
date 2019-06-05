using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation_freeze : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.gameObject.transform.localRotation = Quaternion.identity;
	}
}
