using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GyroRead : MonoBehaviour {

	public Text txt;
	public Text txt1;
	private Gyroscope gyro;
	// Use this for initialization
	void Start () {
		Camera.main.transform.rotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update () {
//		Camera.main.transform.rotation = Quaternion.identity;
		if (SystemInfo.supportsGyroscope) {
			gyro = Input.gyro;
			gyro.enabled = true;
			txt1.text = gyro.enabled.ToString ();
			txt.text = gyro.rotationRateUnbiased.x.ToString ();
		} else {
//			txt1.text = gyro.enabled.ToString ();
		}

		
	}
}
