using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ButtonCreation : MonoBehaviour {
	public GameObject buttonPrefab;
	public Transform ButtonContainer;

	// Use this for initialization
	void Start () {
		Debug.Log ("names");
		Texture[] textures = Resources.LoadAll<Texture> ("Image");
		Debug.Log (textures.Length);
		foreach(Texture t in textures){
			Debug.Log ("names"+ t.name);
			GameObject btn = Instantiate (buttonPrefab) as GameObject;
			btn.transform.SetParent (ButtonContainer);
			btn.GetComponent<RawImage> ().texture = t;
			btn.GetComponent<Button> ().onClick.AddListener (() => OnButtonclick (t.name));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void OnButtonclick(string t){
		Debug.Log (t);
	}
}
