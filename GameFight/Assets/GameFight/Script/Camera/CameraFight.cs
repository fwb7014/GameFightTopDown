using UnityEngine;
using System.Collections;

public class CameraFight : MonoBehaviour {

	public float moveSpeed = 5f;
	private Vector3 relVec;

	public bool refreshTarget;
	
	private Transform target;

	void Start () {
		refreshTarget = false;
		initTarget ();
	}

	void initTarget(){
		target = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
		relVec = transform.position -target.position ;
	}

	public void sayHi(){
		Debug.Log ("nihao");
	}




	void Update () {
		if (target == null)
			return;
		if (refreshTarget) {
			refreshTarget = false;
			initTarget();
		}
		Vector3 newPos = Vector3.Lerp (transform.position,relVec+target.position,Time.deltaTime*moveSpeed);
		transform.position = newPos;
	}

}
