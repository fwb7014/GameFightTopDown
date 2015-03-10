using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	private enum ControlState {Idle,Move,Attack};
	//战斗相关
	public Transform target;
	public Vector3 attackDir;
	public Vector3 attackPos;
	//移动相关
	private Vector3 moveDir;
	private float moveSpeed;
	private Vector3 movePos;
	private float speedFactor = 7f;

	//状态
	private ControlState state;

	private Animator anim;
	private PlayerStatus status;
	private PlayerSkill skill;
	private GameSetting gameSetting;

	public int playerState;// 1 移动

	//



	//技能释放
	private bool useSkill;


	void Awake(){
		anim = GetComponent<Animator> ();
		status = GetComponent<PlayerStatus> ();
		skill = GetComponent<PlayerSkill> ();
	}


	// Use this for initialization
	void Start () {
		gameSetting = GameSetting.instance;
	}


	
	// Update is called once per frame
	void Update () {
		if (Input.anyKeyDown) {
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rayHit;

			if(Physics.Raycast(r,out rayHit,100f)){
				if(rayHit.collider.tag == Tags.FLOOR){
					float dis = Vector3.Distance(transform.position,rayHit.point);
					Instantiate(GameSetting.instance.clickFxNormal,new Vector3(rayHit.point.x,rayHit.point.y+0.02f,rayHit.point.z),rayHit.collider.transform.rotation);
					speedFactor = status.RunSpeedFactor;
					moveSpeed = status.runSpeed;
					movePos = rayHit.point;
					playerState = 1;
					state = ControlState.Move;
			}
			}
		}
		if(target != null && playerState==2){
			movePos = Vector3.zero;
			state = ControlState.Attack;
		}

		if (state == ControlState.Attack && playerState==2) {
			attackDir = target.position-transform.position;
			attackDir[1] = 0f;
			transform.rotation = Quaternion.LookRotation(attackDir);
			anim.SetInteger(HashIds.Skillid,2);
		}
		if (state == ControlState.Move && playerState==1) {
			moveDir = movePos - transform.position;
			moveDir[1] = 0f;
			transform.rotation = Quaternion.LookRotation(moveDir);
			float dis = Vector3.Distance(transform.position,movePos);
			if(dis > 0.02f){
				if(speedFactor>1f){
					speedFactor -=Time.deltaTime*status.speedFactorFade;
				}
				//speedFactor-=(speedFactor>=status.WalkSpeedFactor?status.speedFactorFade:0f)*Time.deltaTime;
				anim.SetInteger(HashIds.Skillid,1);
			}else{
				state = ControlState.Idle;
				playerState = 0;
			}
			anim.SetFloat(HashIds.Speed,speedFactor);
			transform.position+=transform.forward*Time.deltaTime*moveSpeed;
		}
		if (state == ControlState.Idle&& playerState==0) {
			moveSpeed = 0f;
			speedFactor = 0f;
			anim.SetInteger(HashIds.Skillid,0);
			anim.SetFloat(HashIds.Speed,speedFactor);
		}
	}
}
