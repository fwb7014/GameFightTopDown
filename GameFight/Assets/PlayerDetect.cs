using UnityEngine;
using System.Collections;

public class PlayerDetect : MonoBehaviour {
	public PlayerControl control;
	private SphereCollider collider;
	private Rigidbody myRigidbody;
	private bool AttackForce;


	void Awake(){
		control = transform.root.GetComponent<PlayerControl> ();
		collider = GetComponent<SphereCollider> ();
		myRigidbody = transform.root.rigidbody;
	}

	// Use this for initialization
	void Start () {
		AttackForce = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		GameObject obj = other.gameObject;
		if (obj.tag == Tags.ENEMY) {
			control.playerState = 2; 
			control.target = obj.transform;
			AttackOn(obj.transform);

		}
	}
	void OnTriggerExit(Collider other){
		GameObject obj = other.gameObject;
		if (obj.tag == Tags.ENEMY && control.target == obj.transform) {
			control.target = null;
			AttackForce = true;
		}
	}

	void AttackOn(Transform target){
		Vector3 attackDir = target.position - transform.root.position;
		attackDir [1] = 0f;
		//transform.root.rotation = Quaternion.LookRotation (attackDir);
		if (AttackForce) {
			myRigidbody.AddForce (attackDir * 110f);
			AttackForce = false;
		}
	}
}
