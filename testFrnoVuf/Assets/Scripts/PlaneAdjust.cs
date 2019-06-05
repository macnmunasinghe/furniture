using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class PlaneAdjust : MonoBehaviour {
	public Slider slider;
	public Transform plane;
	public Text tx;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public void ChangeHeight(float val){
		GameObject gr = GameObject.FindGameObjectWithTag("ground");
		if(gr){
				
			gr.transform.position = gr.transform.up*val;
			tx.text = gr.transform.position.y.ToString();
		}

//		plane.transform.position = plane.transform.up*val;//
	}
}
