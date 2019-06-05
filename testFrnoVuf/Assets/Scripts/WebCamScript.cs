using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WebCamScript : MonoBehaviour {

	public GameObject webCameraPlane;
	public Text tx1;
	public Text tx2;
	public Text tx3;
	public Text tx4;
	public Text tx5;
	public Text tx6;

	private Gyroscope gyro;
	private Quaternion rot;
	[SerializeField]
	private bool groundCheck;
	[SerializeField]
	private GameObject ground;
	private GameObject cameraParent;
	[SerializeField]
	private bool gyroCheck;
	private bool Gyroena;
	// Use this for initialization
	void Start () {
		WebCamTexture webCameraTexture = new WebCamTexture();
		#if UNITY_ANDROID
		webCameraPlane.transform.rotation = Quaternion.Euler (90f,180f,0f);
		#endif
		#if UNITY_IOS
		webCameraPlane.transform.rotation = Quaternion.Euler (-90f,0f,0f);
		#endif

		webCameraPlane.GetComponent<MeshRenderer>().material.mainTexture = webCameraTexture;
		webCameraTexture.Play();
//		if (Application.isMobilePlatform) {
		cameraParent = new GameObject ("camParent");
		cameraParent.transform.position = this.transform.position;
		this.transform.SetParent (cameraParent.transform);
			Debug.Log ("sdff");
//			if(SystemInfo.supportsGyroscope){
//				gyro = Input.gyro;
//				gyro.enabled = true;
//				gyroCheck = true;
//				cameraParent.transform.rotation = Quaternion.Euler (90f,90f,0f);
//				rot = new Quaternion (0, 0, 1, 0);
//			}
		Gyroena = enbleGyro ();
//		}
//		GameObject cameraParent = new GameObject ("camParent");
//		cameraParent.transform.position = this.transform.position;
//		this.transform.SetParent (cameraParent.transform);
//		//			this.transform.parent = cameraParent.transform;
//		cameraParent.transform.Rotate (Vector3.right, 90);
	}
		
	void Update () {
		if(Gyroena){
			transform.localRotation = gyro.attitude*rot;
		}
		if(gyroCheck){
			Debug.Log ("sdff");
			Quaternion cameraRotation = new Quaternion (Input.gyro.attitude.x, Input.gyro.attitude.y, -Input.gyro.attitude.z, -Input.gyro.attitude.w);//
			this.transform.localRotation = cameraRotation;//
//			transform.localRotation = gyro.attitude*rot;
			if(!groundCheck){
				ground = GameObject.FindGameObjectWithTag ("ground");
				if(ground !=null){
					groundCheck = true;
				}
			}
			if(ground !=null){
				if(transform.localRotation.x!=0 ||transform.localRotation.y!=0 ||transform.localRotation.z!=0){
//					ground.transform.rotation = Quaternion.LookRotation(Vector3.forward*Time.deltaTime);
					tx1.text = transform.localRotation.x.ToString ();
					tx2.text = transform.localRotation.y.ToString ();
					tx3.text = transform.localRotation.z.ToString ();
					tx4.text = ground.transform.localRotation.x.ToString ();
					tx5.text = ground.transform.localRotation.y.ToString ();
					tx6.text = ground.transform.localRotation.z.ToString ();
				}

			}
		}
//		if(!groundCheck){
//			ground = GameObject.FindGameObjectWithTag ("ground");
//			if(ground !=null){
//				groundCheck = true;
//			}
//		}
//		if(ground !=null){
//			if(transform.localRotation.x!=0 ||transform.localRotation.y!=0 ||transform.localRotation.z!=0){
//				ground.transform.localRotation = Quaternion.identity;
//			}
//
//		}
	}
	private  bool enbleGyro(){
		if(SystemInfo.supportsGyroscope){
			gyro = Input.gyro;
			gyro.enabled = true;
//			gyroCheck = true;
			cameraParent.transform.rotation = Quaternion.Euler (90f,90f,0f);
			rot = new Quaternion (0, 0, 1, 0);
			return true;
		}
		return false;
	}
}
