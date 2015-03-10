using UnityEngine;
using System.Collections;

public class EnemyAi : MonoBehaviour {
	[HideInInspector]
	public Monster monster;
	[HideInInspector]
	public Hp_bar barScript;
	[HideInInspector]
	public Transform hpBar;

	//战斗相关
	public Transform target;

	private EnemyStatus status;

	void Awake(){
		monster = GameObject.FindGameObjectWithTag (Tags.EFS_MON).GetComponent<Monster>();
		status = GetComponent<EnemyStatus> ();
		target = GameObject.FindGameObjectWithTag (Tags.PLAYER).transform;
	}


	// Use this for initialization
	void Start () {
		hpBar = monster.CreatHpbar (status.barSize, false, true);
		barScript = hpBar.GetComponent<Hp_bar> ();
		barScript.Damaged (status.maxHp, status.hp, transform, status.height, 0);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newDir = target.position - transform.position;
		transform.rotation = Quaternion.LookRotation (newDir);
	}
}
