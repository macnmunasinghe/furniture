using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeftSidepanelAnim : MonoBehaviour {

//	public RectTransform panel;
	public GameObject panel;
	public Button rel_butt;
	public GameObject slider;
	Animator anim;
	// Use this for initialization
	void Start () {
		anim = this.gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void hidepanel(){
//		anim.SetBool ("enable", false);
//		panel.SetActive (false);
		StartCoroutine("hideanimCon");
	}

	IEnumerator hideanimCon(){
		anim.SetBool ("enable", false);
		rel_butt.GetComponent<Button> ().interactable = false;
		yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);//+anim.GetCurrentAnimatorStateInfo(0).normalizedTime
		panel.SetActive (false);
		rel_butt.GetComponent<Button> ().interactable = true;
		if(slider){
			slider.SetActive (true);
		}
	}

}
