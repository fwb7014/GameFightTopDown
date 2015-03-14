using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SkillScript : MonoBehaviour {
	[HideInInspector]
	public float cdTime;
	private float currCdTime;
	private Image imageMask;
	private Button btn;
	private bool touchAble;

	void Awake(){
		touchAble = false;
		imageMask = gameObject.transform.GetChild(0).GetComponent<Image> ();
		Debug.Log (imageMask.transform.name);
		btn = GetComponent<Button> ();
	}

	void Start(){
		currCdTime = cdTime;
	}

	public void hashClickBtn(){
		currCdTime = cdTime;
	}

	void Update(){
		if (cdTime != 0) {
			if (currCdTime > 0) {
				touchAble = false;
				currCdTime -= Time.deltaTime;
				if(currCdTime <0){
					currCdTime = 0;
				}
				imageMask.fillAmount = currCdTime/cdTime;
			} else {
				touchAble = true;
			}
		}
		if (touchAble) {
			btn.enabled = true;		
		} else {
			btn.enabled = false;	
		}
	}
}
