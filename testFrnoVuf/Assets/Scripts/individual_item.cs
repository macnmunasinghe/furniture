using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class individual_item : MonoBehaviour {
//	public static individual_item Instance5{ set; get;}

	// Use this for initialization
//	[SerializeField]
	public bool check;

	[SerializeField]
	private bool replaceCheck;

	public GameObject Bgimage;
	public Vector3 MyPos;
//	public int myIndex{ get; set;}
	public int index;
	public bool des;
//	private void Awake () {
//		if (Instance5 == null) {
//			Instance5 = this;
//
//		} else {
//			
//		}
//
	Vector3 CanvasSize;
	void Start () {
//		CanvasSize = this.gameObject.transform.GetChild (0).localScale;
	}
	
	// Update is called once per frame
	void Update () {
//		this.gameObject.transform.GetChild (0).localScale = CanvasSize;
		Debug.Log ("my index "+ index);
		MyPos = transform.position;
	}

	public void destroyer(){
		Touch_Select.Instance1.clear = true;
		Instantiation.Instance4.removeItem (index);
		Destroy (this.gameObject);
	}

	public void Description(){
		check = !check;
		if (check) {
			des = true;
//			DesPanel.SetActive (true);
		} else {
			des = false;
//			DesPanel.SetActive (false);
		}
	}

	public void Replace(){
		Instantiation.Instance4.Index_of_object_replace = index;
		replaceCheck = !replaceCheck;
		if(replaceCheck){
			Instantiation.Instance4.repCheck = true;
			Bgimage.SetActive (true);
		}else{
			Instantiation.Instance4.repCheck = false;
			Bgimage.SetActive (false);
		}
	}
}
