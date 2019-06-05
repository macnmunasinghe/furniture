using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Instantiation : MonoBehaviour {

	public static Instantiation Instance4{ set; get;}


	public GameObject chair;
	public GameObject bttt;
//	public GameObject bttt1;
	public List<GameObject> btns;
	public bool repCheck;
	public Vector3 MyPos;
	public int Index_of_object_replace;
//	[SerializeField]
//	private float raydis=150f;

	SelectedObjs selec;
	int count;
	[Header("Gameobjects")]
	public GameObject cube;
	public GameObject gr;
	public GameObject slider;
	public GameObject grPanel;
	public GameObject grPanelButton;
	public Text GrText;
	public GameObject sidepanel;
	public GameObject Rightsidepanel;

	public GameObject pane;
	public Transform con;

	[Header("test obj")]
	public GameObject test;
	public Text tx1;
	public Text tx2;
	public Text Price;
	[Header("Helpers")]
	public GameObject Helper1;
	public GameObject Helper2;

	public Transform pivot;
	public Text tx;
	public Transform plane;
	public List<GameObject> all;
	public List<GameObject> res;
	public List<int> Sum;
	public int summation=0;

	[SerializeField]
	private GameObject cu1;

	[Header("booleans")]
	[SerializeField]
	private bool toggle;
	[SerializeField]
	private bool togglesllist;
	[SerializeField]
	private bool planeHide;

	[Header("to instantiate.cs")]
	public static bool sendCheck;

	private float transition = 0.0f;
	private int transitionCounter_1=0;
	private float goundInittime = 1f;
	public int HelperCount;

	[Header("Button Creation")]
	public GameObject buttonPrefab;
	public Transform ButtonContainer;
	public List<GameObject> Buttons;
	public RectTransform top;
	public RectTransform bottom;
	private int topbtnDistance;
	private int bottombtnDistance;
	// Use this for initialization
	public Texture[] textures;
	public Dictionary<string,int> itms; 
	SelectedObjs sel;
	private void Awake () {
		if (Instance4 == null) {
			Instance4 = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (gameObject);
		}

	}
	void Start () {
		itms= new Dictionary<string,int> ();
		sel = SelectedObjs.Instance7;

		plane.GetComponent<MeshRenderer> ().enabled = false;
		HelperCount = PlayerPrefs.GetInt ("Helper",0);
		if(HelperCount >= 5){
			HelperCount = 0;
		}

		textures = Resources.LoadAll<Texture> ("Image");
		Debug.Log (textures.Length);
		foreach(Texture t in textures){
			Debug.Log ("names"+ t.name);
			GameObject btn = Instantiate (buttonPrefab) as GameObject;
			Buttons.Add (btn);
			btn.transform.SetParent (ButtonContainer);
			btn.GetComponent<RawImage> ().texture = t;
			btn.GetComponent<Button> ().onClick.AddListener (() => OnButtonclick (t.name));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!sidepanel.activeSelf) {
			foreach (GameObject d in btns) {
				Destroy (d);
			}
			btns.Clear ();
			itms.Clear ();
			summation = 0;
			Sum.Clear ();
			Price.text = "List is Empty";
//			Debug.Log ("dsjfsd  "+ clear);
//			clear = false;
		}
		GameObject cube = GameObject.FindGameObjectWithTag ("cube");
		if(cube){
			float dis = Vector3.Distance (cube.transform.position,Camera.main.transform.position);
			tx1.text = dis.ToString ();
		}

//		tx2.text = Camera.main.transform.position.y.ToString ();
		if(planeHide){
			transitionCounter_1++;
			transition += Time.deltaTime;
			grPanel.GetComponent<Image> ().color = Color.Lerp (new Color((float)36/256f,(float)36/256f,(float)36/256,(float)250/256),new Color((float)36/256f,(float)36/256f,(float)36/256f,0),transition);
			grPanelButton.GetComponentInChildren<Image> ().color = Color.Lerp (new Color(1,1,1,(float)240/256),new Color(1,1,1,0),transition);
			GrText.CrossFadeAlpha (0.0f,0.1f,false);
			if(transitionCounter_1 == 50){
				transition = 0.0f;
				transitionCounter_1 = 0;
				grPanel.GetComponent<Image> ().color = new Color ((float)36/256f,(float)36/256f,(float)36/256f,(float)250/256f);
				grPanelButton.GetComponentInChildren<Image> ().color =new Color(1,1,1,(float)240/256);

				grPanel.SetActive (false);
//				if(!grPanel.activeSelf){
//					Camera.main.GetComponent<WebCamScript> ().enabled = true;
//				}
				planeHide = false;
			}
//			
//			grPanel.SetActive (false);
		}
		if(Buttons.Count > 0){
			topbtnDistance = (int)Mathf.Abs(top.transform.position.y - Buttons[0].transform.position.y);
			Debug.Log ("Distance "+topbtnDistance);
			Debug.Log ("ddce "+Buttons[0].transform.position.y);
			if(topbtnDistance == 0 ){
				Debug.Log ("Zero");
//				Buttons [0].gameObject.GetComponent<RectTransform>().anchoredPosition =new Vector2(0,top.anchoredPosition.y);
			}
		}
	}
	/*
	public void instantiation(){
//		GameObject cu = Instantiate (cube,pivot.position ,cube.transform.rotation);
//		cu.transform.parent = plane;
//		Ray ray = Camera.main.ScreenPointToRay (new Vector3(200,200,0));
		RaycastHit hit;
//		Vector3 center = Camera.main.ScreenToWorldPoint (ray);
//		Debug.DrawRay (ray.origin,ray.direction*20,Color.red);
		//if(Physics.Raycast(ray, out hit)){
		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit)) {	
			Vector3 hitpoint = hit.point;
			tx.text = hitpoint.ToString ();
			if (hit.collider.tag == "ground" && hit.collider.tag != "obs") {
				GameObject cu1 = Instantiate (res [0], hitpoint, res [0].transform.rotation);
				all.Add (cu1);
				setparent (cu1);
//				cu1.transform.parent = plane;
			}
		}

	}
*/

	public void ground(){
		if (cu1 == null) {
			planeHide = true;
//			grPanel.SetActive (false);
			StartCoroutine ("initGround");
		} else {
			return;
		}
		 
	}
	IEnumerator initGround(){
		yield return new WaitForSeconds (goundInittime);
		cu1 = Instantiate (gr, gr.transform.position, gr.transform.rotation);
		slider.SetActive (true);
		if(HelperCount==0){
//			Helper1.SetActive (true);
//			Helper2.SetActive (true);
		}
		Helper1.SetActive (true);
//		Helper2.SetActive (true);
		HelperCount++;
		PlayerPrefs.SetInt ("Helper",HelperCount);
	}

	public void destroying(){
//		GameObject[] ar = GameObject.FindGameObjectsWithTag ("obs");
//		foreach(GameObject a in ar){
//			all.Add (a);
//		}
		sendCheck = true;
		foreach (GameObject g in all) {
			Destroy (g);
		}
		all.Clear ();
		itms.Clear ();
		allitem ();
//		allitem ();
	}

	public void onQuit(){
		Application.Quit ();
	}

	public void hide(){
		if (sidepanel.activeSelf) {
			Helper1.SetActive (false);
		} else {
			Helper1.SetActive (true);
		}

		bttt.SetActive (false);
	}

	public void ToggleGround(){
		Helper1.SetActive (false);
		bttt.SetActive (true);
		toggle = !toggle;
//		if (toggle) {
//			Helper1.SetActive (false);
//
//
//			Helper2.SetActive (false);
//
//			if(cu1!= null){
//
//
//			}
//
//		} else {
//			Helper1.SetActive (true);
//
//			if (cu1 != null) {
//
//			}
//		}

	}
	public void setparent(GameObject g){
		//
		GameObject gr = GameObject.FindGameObjectWithTag("ground");
		g.transform.parent = gr.transform;
		//
//		g.transform.parent = plane;
	}

	public void hideHelper1(){
		Helper1.SetActive (false);
	}
	public void hideHelper2(){
		Helper2.SetActive (false);
	}

	public void OnButtonclick(string t){
		int index = int.Parse (t);
		if(!repCheck){
		RaycastHit hit;
		
		Debug.Log (index);
			if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit,70f)) {	
			Vector3 hitpoint = hit.point;
				MyPos = hitpoint;
			tx.text = hitpoint.ToString ();
			tx2.text = Vector3.Distance (hitpoint, Camera.main.transform.position).ToString ();
			if (2 <= Vector3.Distance (hitpoint, Camera.main.transform.position)) {
				if (hit.collider.tag == "ground" && hit.collider.tag != "obs") {
					GameObject cu1 = Instantiate (res [index - 1], hitpoint, res [index - 1].transform.rotation);
//					Models model = new Models ();
//					model.name = cu1.name;
//					model.prefab = cu1.transform.GetChild (1).transform.GetChild (0).transform.GetChild (3).gameObject;
//						count++;
//						model.count++;
//						selec.mapmodels.Add (model);
	
					all.Add (cu1);
					cu1.gameObject.GetComponent<individual_item> ().index = all.Count - 1;
//						individual_item.Instance5.myIndex = all.Count - 1;
//						setparent (cu1);
//						break;
//					}
//				}

//				GameObject cu1 = Instantiate (res [5], hitpoint, res [5].transform.rotation);
//				all.Add (cu1);
				setparent (cu1);
					//				cu1.transform.parent = plane;
					}
				}
			} 
		}else {
			GameObject cu1 = Instantiate (res [index - 1], all[Index_of_object_replace].transform.position, res [index - 1].transform.rotation);
			Touch_Select.Instance1.clear = true;
			Destroy (all[Index_of_object_replace]);
			all.RemoveAt (Index_of_object_replace);
			all.Add (cu1);
			cu1.gameObject.GetComponent<individual_item> ().index = all.Count - 1;
			setparent (cu1);
			repCheck = false;
		}

	}


	public void allitem(){
		bool clear = false;
		if(togglesllist){
			togglesllist = false;
			clear = true;
		}
//		else{
		if(clear){
			foreach (GameObject d in btns) {
				Destroy (d);
			}
			btns.Clear ();
			itms.Clear ();
			summation = 0;
			Sum.Clear ();
			Price.text = "List is Empty";
			Debug.Log ("dsjfsd  "+ clear);
			clear = false;

		}

		togglesllist = !togglesllist;
//		}

		if (togglesllist) {

			foreach (GameObject g in all) {
				if (itms.ContainsKey (g.name)) {
					itms [g.name]++;
					Debug.Log ("g.name +" + g.name);
				} else {
					itms.Add (g.name, 1);
					Debug.Log ("g.name +" + g.name);
				}
			}
			foreach (var pair in itms) {
				GameObject btn = Instantiate (pane) as GameObject;
				string[] takeIndex = pair.Key.Split ('(');
				int newIndex = int.Parse (takeIndex [0]);
				btn.transform.GetChild (0).GetComponent<RawImage> ().texture = textures [newIndex-1];
				btn.transform.GetChild (1).GetComponent<Text> ().text = sel.mapmodels [newIndex - 1].name;//pair.Key;
				btn.transform.GetChild (2).GetComponent<Text> ().text ="U S D   "+sel.mapmodels [newIndex-1].price+ " X " + pair.Value;
				btns.Add (btn);
				btn.transform.SetParent (con);
				int price = sel.mapmodels [newIndex-1].price;
				Debug.Log ("individual price "+ price);
				Sum.Add (pair.Value*price);
			}
			foreach(int x in Sum){
				summation += x;
			}
			if (all.Count == 0) {
				Price.text = "List is Empty";
			} else {
				Debug.Log ("Summation  "+ summation);
				Price.text = "Total USD " + summation+" /=";
			}

		}else {
			foreach (GameObject d in btns) {
				Destroy (d);
			}
			btns.Clear ();
			itms.Clear ();
			summation = 0;
			Sum.Clear ();
			Price.text = "List is Empty";
		}


	}

	public void sidePanel(){
//		allitem ();
		slider.SetActive (false);
		Helper1.SetActive (false);
		sidepanel.SetActive (true);
		bttt.SetActive (true);
//		allitem ();
		togglesllist=false;
		StartCoroutine("loadItems");
	}
	IEnumerator loadItems(){
		bool ss = sidepanel.activeSelf;
		yield return ss;
		allitem ();
	}

	public void RightsidePanel(){
//		slider.SetActive (false);
		Helper2.SetActive (false);
		Rightsidepanel.SetActive (true);
	}
	public void sidePanelhide(){
		slider.SetActive (true);
		sidepanel.SetActive (false);
		//----------------------
	}

	public void RightsidePanelhide(){
//		slider.SetActive (true);
		Rightsidepanel.SetActive (false);
	}

	public void removeItem(int index){
		all.RemoveAt(index);
	}
	public void chairin(){
//		GameObject ch = Instantiate (chair,);
		RaycastHit hit;

		if (Physics.Raycast (Camera.main.transform.position, Camera.main.transform.forward, out hit)) {	
			Vector3 hitpoint = hit.point;
			MyPos = hitpoint;
//			tx.text = hitpoint.ToString ();
//			tx2.text = Vector3.Distance (hitpoint, Camera.main.transform.position).ToString ();
			if (2 <= Vector3.Distance (hitpoint, Camera.main.transform.position)) {
				if (hit.collider.tag == "ground" && hit.collider.tag != "obs") {
					GameObject cu1 = Instantiate (chair, hitpoint, chair.transform.rotation);
//					all.Add (cu1);
//					cu1.gameObject.GetComponent<individual_item> ().index = all.Count - 1;
					setparent (cu1);
				}
			}
		} 
	}
}

