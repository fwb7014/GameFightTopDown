using UnityEngine;
using System.Collections;
/**
 * 武器
 * */
public class WeaponControl : MonoBehaviour {


	private GameObject player;
	private PlayerStatus status;
	private BoxCollider collider;
	private PlayerControl control;


	void Awake(){
		collider = GetComponent<BoxCollider> ();
	}
	void Start(){
		player = GameObject.FindGameObjectWithTag (Tags.PLAYER);
		status = player.GetComponent<PlayerStatus> ();
		control = player.GetComponent<PlayerControl> ();
	}



	
	// Update is called once per frame
	void Update () {

	}



	public int calculateDamage(){
		return 10; 
	}
}
