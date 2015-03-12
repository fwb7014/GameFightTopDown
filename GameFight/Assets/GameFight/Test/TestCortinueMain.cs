using UnityEngine;
using System.Collections;

public class TestCortinueMain : MonoBehaviour {

	public GameObject obj;

	private GameObject myObj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnGUI(){
		if (GUILayout.Button ("Timescale=1 正常协同")) {
			if(myObj != null){
				Destroy(myObj);
			}
			myObj = Instantiate(obj,Vector3.zero,Quaternion.identity) as GameObject;
			TestCortinue cor = myObj.GetComponent<TestCortinue>();
			cor.CallFunctionListByTimes(false);
					
		}
		if (GUILayout.Button ("Timescale=1 忽略正常协同")) {
			if(myObj != null){
				Destroy(myObj);
			}
			myObj = Instantiate(obj,Vector3.zero,Quaternion.identity) as GameObject;
			TestCortinue cor = myObj.GetComponent<TestCortinue>();
			cor.CallFunctionListByTimes(true);
		}

		if (GUILayout.Button ("Timescale=0.1 正常协同")) {
			if(myObj != null){
				Destroy(myObj);
			}
			Time.timeScale = 0.1f;
			myObj = Instantiate(obj,Vector3.zero,Quaternion.identity) as GameObject;
			TestCortinue cor = myObj.GetComponent<TestCortinue>();
			cor.CallFunctionListByTimes(false);
		}
		if (GUILayout.Button ("Timescale=0.1 忽略正常协同")) {
			if(myObj != null){
				Destroy(myObj);
			}
			Time.timeScale = 0.1f;
			myObj = Instantiate(obj,Vector3.zero,Quaternion.identity) as GameObject;
			TestCortinue cor = myObj.GetComponent<TestCortinue>();
			cor.CallFunctionListByTimes(true);
		}

	}
}
