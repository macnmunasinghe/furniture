using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDescription : MonoBehaviour {
	SelectedObjs selec;
	int flag1;
	int flag2;
	public Transform ButtonContainer;
	// Use this for initialization
	void Start () {
//		selec = SelectedObjs.Instance2;
		flag2 = selec.mapmodels.Count;
	}
	
	// Update is called once per frame
	void Update () {
		flag1 = selec.mapmodels.Count;

		if (selec.mapmodels.Count > 0) {
			for (int i = 0; i < selec.mapmodels.Count; i++) {
				GameObject Despanel = Instantiate (selec.mapmodels [i].prefab) as GameObject;
				Despanel.SetActive (true);
				Despanel.transform.SetParent (ButtonContainer);
//				btn.GetComponent<RawImage> ().texture = t;
			}
			flag2 = selec.mapmodels.Count;
		} else {
			return;
		}
	}
}
