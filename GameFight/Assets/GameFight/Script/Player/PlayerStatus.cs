using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public float runSpeed = 1.2f;
	[HideInInspector]
	public float walkSpeed;
	public float runToWalkDis = 3f;//跑步的最小距离
	public float RunSpeedFactor = 7.5f;
	public float WalkSpeedFactor = 1.5f;
	public float speedFactorFade = 1.2f;


	void Awake(){
		walkSpeed = runSpeed * 0.5f;
	}

	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
