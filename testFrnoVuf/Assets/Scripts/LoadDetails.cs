using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadDetails : MonoBehaviour {
	public Text name;
	public Text description;
	public Text price;
	public RawImage image;
	// Use this for initialization
	SelectedObjs sele;
	void Start () {
		sele = SelectedObjs.Instance7;
	}
	
	// Update is called once per frame
	void Update () {
		if(this.gameObject.activeSelf){
//			cu1.gameObject.GetComponent<individual_item> ().index = all.Count - 1;
			int index = sele.Modelindex;
			name.text = sele.mapmodels [index].name.ToString();
			description.text  = sele.mapmodels [index].Description.ToString();
			price.text  ="Price  USD "+ sele.mapmodels [index].price.ToString()+" /=";
			image.GetComponent<RawImage> ().texture = sele.mapmodels [index].image;
		}	
	}
}
