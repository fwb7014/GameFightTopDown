using UnityEngine;
using System.Collections;

public class EnemyAi : MonoBehaviour {
	[HideInInspector]
	public Monsters monster;
	[HideInInspector]
	public Hp_bar barScript;
	[HideInInspector]
	public Transform hpBar;

	//战斗相关
	public Transform target;


	public bool life;//是否活着
	private float deadTime;
	private Animator anim;
	private EnemyStatus status;
	private CapsuleCollider collider;
	

	void Awake(){
		life = true;
		monster = GameObject.FindGameObjectWithTag (Tags.EFS_MON).GetComponent<Monsters>();
		status = GetComponent<EnemyStatus> ();
		anim = GetComponent<Animator> ();
		collider = GetComponent<CapsuleCollider> ();
		target = GameObject.FindGameObjectWithTag (Tags.PLAYER).transform;
	}


	// Use this for initialization
	void Start () {
		hpBar = monster.CreatHpbar (status.barSize, false, true);
		barScript = hpBar.GetComponent<Hp_bar> ();
		barScript.Damaged (status.maxHp, status.hp, transform, status.height, -1);
	}
	
	// Update is called once per frame
	void Update () {
		if (life) {

			if(target == null){
				return;
			}
			Vector3 newDir = target.position - transform.position;
			transform.rotation = Quaternion.LookRotation (newDir);
			float distance = Vector3.SqrMagnitude(newDir);
			if(distance>1f){
				transform.position+=transform.forward*Time.deltaTime*status.walkSpeed;
				anim.SetInteger(HashIds.Skillid,1);
			}else{
				anim.SetInteger(HashIds.Skillid,0);
			}
		} else {
			transform.position += Vector3.down * Time.deltaTime * 0.08f;
			deadTime += Time.deltaTime;
			if (deadTime > 0.8f && hpBar!=null) {
				Destroy(hpBar.gameObject);
			}
			if (deadTime > 3) {
				Destroy(gameObject);	
			}		
		}
	}

	public void Damaged(int _damage,Vector3 targetPosition,int skillid){
		if (!life) {
			return;		
		}
		Vector3 selfAttackDir = transform.position - targetPosition;
		Vector3 selfForceDir = Vector3.zero;
		float selfForce = 0;
		switch (skillid) {
		case 1:
			selfForceDir = selfAttackDir;
			selfForce =50f;
			break;
		case 2:
			selfForceDir = selfAttackDir;
			selfForce =50f;
			break;
		case 3:
			selfForceDir = selfAttackDir;
			selfForce =110f;
			break;
			case 4:
			selfForceDir = selfAttackDir;
			selfForce =85f;
			break;
		}

		if (_damage != 0) {
			status.hp-=_damage;
			if (status.hp >= 0) {
				barScript.Damaged (status.maxHp, status.hp, transform, status.height, -1);
			}
			monster.SetDamageNum(transform.position+status.height*Vector3.up,_damage,selfAttackDir);
		}
		Debug.Log ("受到伤害 selfForce="+selfForce+"    selfForceDir="+selfForceDir);
		if (selfForce != 0 && selfForceDir != Vector3.zero) {
			rigidbody.AddForce(selfForceDir.normalized*selfForce);
		}

		if(status.hp <= 0){
			life = false;
			Dead();
		}
	}
	void Dead(){
		anim.SetBool (HashIds.Dead, true);
		collider.enabled = false;
	}
}
