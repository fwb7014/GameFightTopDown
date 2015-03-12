using UnityEngine;
using System.Collections;


public class PlayerSkill : MonoBehaviour {
	public enum SkillKind{NormalAttack,TechSkill}
	public enum TechSkillRange{Circle}

	[System.Serializable]
	public class Skill{
		public SkillKind skillKind;//技能类型
		public float attackRange;//攻击范围
		public int skillid;//技能编号
	}

	[System.Serializable]
	public class NorAttackSkill:Skill
	{
		public float attackDis;
	}

	[System.Serializable]
	public class TechSkill:Skill
	{
		public TechSkillRange skillRange;

	}


	public NorAttackSkill[] normalAttack;
	public TechSkill[] techSkill;

	//其他相关
	private PlayerControl playerControl;
	private PlayerStatus status;
	private GameObject player;  


	public NorAttackSkill getNormalSkill(int skillid){
		for (int i = 0; i<normalAttack.Length; i++) {
			if(normalAttack[i].skillid == skillid)
				return normalAttack[i];
		}
		return null;
	}

	public TechSkill getTechSkill(int skillid){
		for (int i = 0; i<techSkill.Length; i++) {
			if(techSkill[i].skillid == skillid)
				return techSkill[i];
		}
		return null;
	}


	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag (Tags.PLAYER);
		status = player.GetComponent<PlayerStatus> ();
		playerControl = player.GetComponent<PlayerControl> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
