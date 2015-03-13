using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public enum ControlState {Idle,Move,Attack,XuanFeng};
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
	public ControlState state;

	private Animator anim;
	private PlayerStatus status;
	public PlayerSkill skill;
	private GameSetting gameSetting;

	public int localSkillId;//本地技能id  

	public int clickSkillCount;

	//



	//技能释放
	private bool useSkill;



	void Awake(){
		clickSkillCount = 0;
		anim = GetComponent<Animator> ();
		status = GetComponent<PlayerStatus> ();
		skill = GetComponent<PlayerSkill> ();
	}

	void OnEnable(){
	}


	// Use this for initialization
	void Start () {
		gameSetting = GameSetting.instance;
	}

	    
	
	// Update is called once per frame
	void Update () {
		if (clickSkillCount == 0 && Input.anyKeyDown) {

			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rayHit;

			if(Physics.Raycast(r,out rayHit,100f)){
				if(rayHit.collider.tag == Tags.FLOOR){
					float dis = Vector3.Distance(transform.position,rayHit.point);
					Instantiate(GameSetting.instance.clickFxNormal,new Vector3(rayHit.point.x,rayHit.point.y+0.02f,rayHit.point.z),rayHit.collider.transform.rotation);
					speedFactor = status.RunSpeedFactor;
					moveSpeed = status.runSpeed;
					movePos = rayHit.point;
					localSkillId = 1;//移动
			}
			}
		}


		//状态切换
		float animSkillId = anim.GetInteger (HashIds.Skillid);
		int newSkillid = -1;
		switch (localSkillId) { //2和1 都对应 localskill 1
			case 0:
				if(animSkillId == 0 || animSkillId == 1 ||animSkillId == 2||animSkillId == 3){
					newSkillid = 0;
					target = null;
					state = ControlState.Idle;
				}
				break;
			case 1:
				if(animSkillId == 0 || animSkillId == 1 ||animSkillId == 2||animSkillId == 3){
					newSkillid = 1;
					state = ControlState.Move;
				}
				break;
			case 2:
				newSkillid = 1;
				state = ControlState.Move;
				break;
			case 3:
				if(animSkillId == 0 || animSkillId == 1 ||animSkillId == 2||animSkillId == 3){
					newSkillid = 3;
					state = ControlState.Attack;
				}
				break;
			case 4:
				if(animSkillId == 0 || animSkillId == 1 ||animSkillId == 2||animSkillId == 3){
					newSkillid = 4;
					state = ControlState.XuanFeng;
				}else if(animSkillId == 4){
					newSkillid = 0;
					localSkillId = 0;
					state = ControlState.Idle;
				}
				break;
		}
		if (newSkillid != -1) {
			anim.SetInteger(HashIds.Skillid,newSkillid);


			if (state == ControlState.Idle) {
				movePos = Vector3.zero;
				moveSpeed = 0f;
				speedFactor = 0f;
				anim.SetFloat(HashIds.Speed,speedFactor);
			}

			if(state == ControlState.Attack){
				movePos = Vector3.zero;
				moveSpeed = 0f;
				speedFactor = 0f;
			}
			
			if (state == ControlState.Move) {
				if(movePos != Vector3.zero){
					moveDir = movePos - transform.position;
					moveDir[1] = 0f;
					transform.rotation = Quaternion.LookRotation(moveDir);
					float dis = Vector3.Distance(transform.position,movePos);
					if(dis > 0.1f){
						if(speedFactor>1f){
							speedFactor -=Time.deltaTime*status.speedFactorFade;
						}
					}else{
						movePos = Vector3.zero;
						localSkillId = 0;
					}
					anim.SetFloat(HashIds.Speed,speedFactor);
					transform.position+=transform.forward*Time.deltaTime*moveSpeed;
				}
			}

		}

		if (target != null) {   
			EnemyAi ai = target.GetComponent<EnemyAi>();
			if(ai == null || !ai.life){
				target = null;
				localSkillId = 0;
			}else{
				PlayerSkill.NorAttackSkill norSkill = skill.normalAttack[0];
				float norDis = Vector3.SqrMagnitude(target.transform.position-transform.position);

				if(localSkillId == 2){
					attackDir = target.position-transform.position;
					attackDir[1] = 0f;
					transform.rotation = Quaternion.LookRotation(attackDir);

					if(norDis<norSkill.attackDis*norSkill.attackDis){
						Debug.Log (norDis+"_"+norSkill.attackDis*norSkill.attackDis+"@@@@");
						localSkillId = 3;
					}else{
						Debug.Log (norDis+"_"+norSkill.attackDis*norSkill.attackDis);
					}
				}
				if(localSkillId ==3 ||localSkillId ==1 || localSkillId ==0){
					if(norDis>norSkill.attackDis*norSkill.attackDis){
						target = null;
					}
				}
			}
		}
		if (target == null) {
			//Debug.Log ("没有目标");		
		}



		//Debug.Log (target+"_"+state+"_"+playerState);
//		if(target != null){
//			if(playerState==3 ){
//				movePos = Vector3.zero;
//				state = ControlState.Attack;
//			}
//
//			if(state == ControlState.Attack && playerState==3){
//				attackDir = target.position-transform.position;
//				attackDir[1] = 0f;
//				transform.rotation = Quaternion.LookRotation(attackDir);
//				anim.SetInteger(HashIds.Skillid,2);
//			}
//
//			//计算和普通攻击之间的距离
//			PlayerSkill.NorAttackSkill norSkill = skill.normalAttack[0];
//			float norDis = Vector3.SqrMagnitude(target.transform.position-transform.position);
//
//			if(playerState ==2){ 
//				if(norDis<norSkill.attackDis*norSkill.attackDis){
//					playerState =3;
//				}
//			}
//			if(playerState == 0 || playerState == 1){
//				if(norDis>norSkill.attackDis*norSkill.attackDis){
//					target = null;
//				}
//			} 
//
//			//检查攻击距离
//			if(playerState  ==3){
//				PlayerSkill.Skill currentSkill = skill.getCurrentSkill();
//				if(currentSkill != null){
//					if(currentSkill.skillKind == PlayerSkill.SkillKind.NormalAttack){
//						PlayerSkill.NorAttackSkill curSkill = (PlayerSkill.NorAttackSkill)currentSkill;
//						float dis = Vector3.SqrMagnitude(target.transform.position-transform.position);
//						//Debug.Log (dis+"_"+curSkill.attackDis +"###########");
//						if(dis>curSkill.attackDis*curSkill.attackDis){
//							target = null;
//							playerState = 0; 
//							state = ControlState.Idle;
//						}else{
//							playerState =3; 
//						}
//					}
//				}
//			}
//		}

//		if (target == null && (playerState == 2 || playerState ==3)) {
//			state = ControlState.Idle;
//			playerState = 0;	
//		}
//
//		if (state == ControlState.Move && playerState==1) {
//			if(movePos == Vector3.zero)
//				return ;
//
//			moveDir = movePos - transform.position;
//			moveDir[1] = 0f;
//			transform.rotation = Quaternion.LookRotation(moveDir);
//			float dis = Vector3.Distance(transform.position,movePos);
//			if(dis > 0.1f){
//				if(speedFactor>1f){
//					speedFactor -=Time.deltaTime*status.speedFactorFade;
//				}
//				//speedFactor-=(speedFactor>=status.WalkSpeedFactor?status.speedFactorFade:0f)*Time.deltaTime;
//				anim.SetInteger(HashIds.Skillid,1);
//			}else{
//				movePos = Vector3.zero;
//				state = ControlState.Idle;
//				playerState = 0;
//			}
//			anim.SetFloat(HashIds.Speed,speedFactor);
//			transform.position+=transform.forward*Time.deltaTime*moveSpeed;
//		}
//		if (state == ControlState.Idle&& playerState==0) {
//			moveSpeed = 0f;
//			speedFactor = 0f;
//			anim.SetInteger(HashIds.Skillid,0);
//			anim.SetFloat(HashIds.Speed,speedFactor);
//		}

	}

	public void OnButtonClickBegin(){
		clickSkillCount++;;
	}

	public void OnButtonClickEnd(){
		localSkillId = 4;
		clickSkillCount--;
	}

}
