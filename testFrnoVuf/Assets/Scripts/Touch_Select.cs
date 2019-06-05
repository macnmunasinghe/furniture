using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Touch_Select : MonoBehaviour {

	public static Touch_Select Instance1{ set; get;}

	[Header("Shared")]
	public bool clear;

	public Text tx;
	[SerializeField]
	private int tapCount;
	[SerializeField]
	private float doubletapTimer;
	[SerializeField]
	private bool selecter;
	public GameObject DesPanel;
	public List<GameObject> SelectedObj;

	[Header ("button")]
	[SerializeField]
	private GameObject closeButton;


	SelectedObjs selec;
	// Use this for initialization
	private void Awake () {
		Instance1 = this;
	}
	public void Start(){
		selec = SelectedObjs.Instance7;
	}
	// Update is called once per frame
	void Update () {

		if (SelectedObj.Count > 0) {
			bool DD = SelectedObj [0].gameObject.GetComponent<individual_item> ().des;
			if (DD) {
				tx.text = "true";
				DesPanel.SetActive (true);
			} else {
				DesPanel.SetActive (false);
				tx.text = "false";
			}
		}

		if(Instantiation.sendCheck){
			selecter = false;
			Instantiation.sendCheck = false;
		}
		if(clear){
			buttoscript ();
			clear = false;
		}
		/*
		if(Input.touchCount == 1){
			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,100f)){
				Debug.Log ("selected obj "+hit.collider.name);
				tx.text = hit.collider.name.ToString ();
				if(hit.collider.tag =="obs"){
					SelectedObj.Add (hit.collider.gameObject);
					Touch touch = Input.GetTouch (0);
					if(touch.phase == TouchPhase.Moved)
					{
						hit.collider.gameObject.transform.Rotate (0f,0f,touch.deltaPosition.x);
					}
				}
			}
			
		}*/
		/*
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast(ray,out hit,500f)){
				Debug.Log ("sss selected obj "+hit.collider.name);
				tx.text = "m" + hit.collider.name.ToString ();
			}
		}*/
		/*
		sss
		*/
		//double tap selecet and rotate
		if(Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began){
			tapCount++;
		}
		if(tapCount>0){
			doubletapTimer += Time.deltaTime;
		}
		if(tapCount >= 2){

			Ray ray = Camera.main.ScreenPointToRay (Input.GetTouch (0).position);
			RaycastHit hit;
			if(Physics.Raycast(ray,out hit,500f)){
				Debug.Log ("selected obj "+hit.collider.name);
				tx.text = hit.collider.name.ToString ();
				if(hit.collider.tag =="obs"){
					selecter = !selecter;
					SelectedObj.Add (hit.collider.gameObject);
					string name = hit.collider.gameObject.name;
					string[] takeIndex = name.Split ('(');
					int newIndex = int.Parse (takeIndex [0]);
					selec.Modelindex = newIndex-1;
				}
			}
			doubletapTimer = 0f;
			tapCount = 0;
		}
		if(doubletapTimer>0.5f){
			doubletapTimer = 0f;
			tapCount = 0;
			
		}
		if (selecter) {
			
			SelectedObj [0].transform.GetChild (0).gameObject.SetActive (true);
			SelectedObj [0].transform.GetChild (1).gameObject.SetActive (true);
			Touch touch = Input.GetTouch (0);
			if (touch.phase == TouchPhase.Moved) {
				SelectedObj [0].transform.Rotate (0f, -touch.deltaPosition.x*0.2f,0f );
			}
		} else {
			if(SelectedObj.Count > 0){
				SelectedObj [0].transform.GetChild (0).gameObject.SetActive (false);
				SelectedObj [0].transform.GetChild (1).gameObject.SetActive (false);
				SelectedObj.Clear ();
			}


		}

	}
	//button for destry
	public void buttoscript(){
		if(SelectedObj.Count > 0){
			Destroy (SelectedObj[0]);
			SelectedObj.Clear ();
		}
	}
	public void desPanel(){
//		
		DesPanel.SetActive (false);
		SelectedObj [0].gameObject.GetComponent<individual_item> ().des = false;
		SelectedObj [0].gameObject.GetComponent<individual_item> ().check = false;
	}
}
