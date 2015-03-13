using UnityEngine;
using System.Collections;

public class CameraFight : MonoBehaviour {

	public float moveSpeed = 5f;
	private Vector3 relVec;
	
	private Transform target;

	void Start () {
		target = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
		relVec = transform.position -target.position ;


	}

	void Update () {
		if (target == null)
			return;
		Vector3 newPos = Vector3.Lerp (transform.position,relVec+target.position,Time.deltaTime*moveSpeed);
		transform.position = newPos;
	}

}
