using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class FightSkillUI : MonoBehaviour {
	public GameObject [] skillsContain;
	public GameObject skillPrefab;

	void Start(){
		GameObject player = GameObject.FindGameObjectWithTag (Tags.PLAYER);
	}

	public void initPlayerSkillUi(PlayerSkill.TechSkill[] techSkill){
		if (skillsContain == null || skillsContain.Length == 0)
			return;

		PlayerControl playerControl = GameObject.FindGameObjectWithTag(Tags.PLAYER).GetComponent<PlayerControl> ();
		int maxSkillCount = skillsContain.Length;
		int playerSkills = techSkill.Length;
		for (int i = 0; i<maxSkillCount; i++) {
			if(skillsContain[i].transform.childCount>0)
				Destroy(skillsContain[i].transform.GetChild(0).gameObject);
			skillsContain[i].SetActive(false);
		}
		for (int i = 0; i<Mathf.Min(playerSkills,maxSkillCount); i++) {
			Sprite sprite = techSkill[i].textureUiSprite;
			GameObject obj = Instantiate(skillPrefab,Vector3.zero,Quaternion.identity) as GameObject;
			SkillScript script = obj.GetComponent<SkillScript>();
			script.cdTime = techSkill[i].cdTime;
			obj.transform.SetParent (skillsContain[i].transform,false);
			skillsContain[i].SetActive(true);
			Image img = obj.GetComponent<Image>();
			img.overrideSprite = sprite;
			Button btn = obj.GetComponent<Button>();
			int skillid = techSkill[i].skillid;
			btn.onClick.AddListener(delegate() {
				playerControl.OnButtonClickSkill(skillid);
				//script.hashClickBtn();
			});
		}
	}
}
