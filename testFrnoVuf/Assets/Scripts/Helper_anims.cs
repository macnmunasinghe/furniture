using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper_anims : MonoBehaviour {
	private int transitionCounter_1=0;
	private float transition = 0.0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transition += Time.deltaTime;
		transitionCounter_1++;
		this.gameObject.GetComponent<Image> ().color = Color.Lerp (new Color((float)201/256f,(float)201/256f,(float)234/256,1),new Color((float)201/256f,(float)201/256f,(float)234/256,(float)60/256),transition*1f);
		if(transitionCounter_1 == 60){
			transition = 0.0f;
			transitionCounter_1 = 0;
			this.gameObject.GetComponent<Image> ().color = new Color ((float)201/256f,(float)201/256f,(float)234/256f,1);

		}

	}
}
