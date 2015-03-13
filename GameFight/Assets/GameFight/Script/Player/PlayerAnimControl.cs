using UnityEngine;
using System.Collections;

public class PlayerAnimControl : MonoBehaviour {
	private GameObject player;
	private PlayerStatus status;
	private PlayerControl control;
	private PlayerSkill skill;
	private Transform playerTransform;
	private AudioSource audioSource;
	
	private Vector3 drawPos;
	private float drayRadiu;


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag (Tags.PLAYER);
		status = player.GetComponent<PlayerStatus> ();
		control = player.GetComponent<PlayerControl> ();
		audioSource = player.GetComponent<AudioSource> ();
		playerTransform = player.transform.root;
		skill = control.skill;
	}
	
	// Update is called once per frame
	void Update () {
	} 

	//skillname
	//用于触发伤害
	public void AttackEvent(int skillid){
		PlayerSkill.Skill _skill = null;
		switch (skillid) {
		case 1:
			_skill = skill.getNormalSkill(skillid);
			break;
		case 2:
			_skill = skill.getNormalSkill(skillid);
			break;
		case 3:
			_skill = skill.getNormalSkill(skillid);
			break;
		case 4:
			_skill = skill.getTechSkill(skillid);
			break;
		case 11:
			_skill = skill.getNormalSkill(skillid);
			break;
		case 12:
			_skill = skill.getNormalSkill(skillid);
			break;
		case 13:
			_skill = skill.getNormalSkill(skillid);
			break;
		case 14:
			_skill = skill.getTechSkill(skillid);
			break;
		}


		if (_skill != null) {
			triggerSkill (_skill);	
		}

	}

	void triggerSkill(PlayerSkill.Skill skill){
		PlayerSkill.SkillKind kind = skill.skillKind;
		if (kind == PlayerSkill.SkillKind.NormalAttack) {
			PlayerSkill.NorAttackSkill normalSkill = (PlayerSkill.NorAttackSkill)skill;	
			handleNormalSkill(normalSkill);
		}else if(kind == PlayerSkill.SkillKind.TechSkill){
			PlayerSkill.TechSkill techSkill = (PlayerSkill.TechSkill)skill;
			handleTechSkill(techSkill);
		}
	}

	// 处理普通攻击技能
	void handleNormalSkill(PlayerSkill.NorAttackSkill normalSkill){

		if (normalSkill.audioClip != null) {
			audioSource.PlayOneShot(normalSkill.audioClip);		
		}


		float attackRange = normalSkill.attackRange;
		Vector3 pos = playerTransform.position + status.height * 0.5f * Vector3.up + playerTransform.forward * status.height/3f;
		Collider[] cols= Physics.OverlapSphere (pos,attackRange);
		int skillid = normalSkill.skillid;
		drawPos = pos;
		drayRadiu = attackRange;

		for (int i = 0; i<cols.Length; i++) {
			if(cols[i] == null || cols[i].gameObject == null){
				continue;
			}

			GameObject obj = cols[i].gameObject;
			if(obj.tag == Tags.ENEMY){
				EnemyAi enemy = obj.GetComponent<EnemyAi>();
				if(enemy == null)
					continue;
				enemy.Damaged(10,playerTransform.position,skillid);
			}
		}
	}
	/**
	 * 处理技能
	 * */
	void handleTechSkill(PlayerSkill.TechSkill techSkill){

		if (techSkill.audioClip != null) {
			audioSource.PlayOneShot(techSkill.audioClip);		
		}

		PlayerSkill.TechSkillRange rangeType = techSkill.skillRange;
		float attackRange = techSkill.attackRange;
		int skillid = techSkill.skillid;
		if (rangeType == PlayerSkill.TechSkillRange.Circle) {
			Vector3 pos = playerTransform.position + status.height * 0.5f * Vector3.up;
			Collider[] cols= Physics.OverlapSphere (pos,attackRange);
			for (int i = 0; i<cols.Length; i++) {
				if(cols[i] == null || cols[i].gameObject == null){
					continue;
				}
				GameObject obj = cols[i].gameObject;
				if(obj.tag == Tags.ENEMY){
					EnemyAi enemy = obj.GetComponent<EnemyAi>();
					if(enemy == null)
						continue;
					enemy.Damaged(15,playerTransform.position,skillid);
				}
			}
		}
	}

	void OnDrawGizmos(){
		if (drawPos != null && drawPos != Vector3.zero) {
			Gizmos.color = new Color (1f,0f,0f,0.5f);
			Gizmos.DrawSphere (drawPos,drayRadiu);
		}
	}
	
}
