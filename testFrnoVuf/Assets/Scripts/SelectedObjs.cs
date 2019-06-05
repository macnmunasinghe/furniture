using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedObjs : MonoBehaviour {
	public static SelectedObjs Instance7{ set; get;}
	public int Modelindex;
	public int num_of_resources;
	public List<Models> mapmodels = new List<Models>();
	private void Awake () {
		if (Instance7 == null) {
			Instance7 = this;
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy (gameObject);
		}

	}
	void Start(){
		GameObject[] allobj = Resources.LoadAll<GameObject>("models");
		Debug.Log ("allobj size" + allobj.Length);
	}
	// Update is called once per frame
	void Update () {

	}

}
[System.Serializable]
public class Models{
	public string name;
	public string Description;
	public int price;
	public Texture image;
	public GameObject prefab;
}
