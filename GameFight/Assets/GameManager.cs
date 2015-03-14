using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public Transform[] enemySpawn ;
	public int maxEnmeyCount;
	public int currentEnemyCount;
	public int totalEnemyCount;

	private int enmeySpawnIndex;

	public GameObject enemyPrefab;

	public int createEnmeyMaxCount;//每次生成最多的个数
	private float createEnemyTime;
	public float createEnemyTimeOrigin = 3f;

	//玩家相关
	public GameObject[] playrs;
	public int playerIndex = 0;
	public GameObject currentPlayer;
	public Transform playerSwawn;


	private CameraFight cameraFight;
	private FightSkillUI fightSkillUi;




	void Awake(){
		cameraFight = Camera.main.GetComponent<CameraFight> ();
		fightSkillUi = Camera.main.GetComponent<FightSkillUI> ();  
		currentPlayer = initPlayer (playerSwawn.position);
	}


	void Start () {
		enmeySpawnIndex = 0;
		currentEnemyCount = 0;
		createEnemyTime = createEnemyTimeOrigin;
	}
	
	// Update is called once per frame
	void Update () {

		currentEnemyCount = GameObject.FindGameObjectsWithTag (Tags.ENEMY).Length;

		if (totalEnemyCount > 0 && currentEnemyCount < maxEnmeyCount) {
			createEnemyTime -= Time.deltaTime;
		}

		if (createEnemyTime < 0) {
			if(totalEnemyCount>0){
				createEnemyTime = createEnemyTimeOrigin;
				int count = Mathf.Min(totalEnemyCount,createEnmeyMaxCount);
				while(count>0){
					count --;
					totalEnemyCount--;
					createEnemy();
				}
			}		
		}
	}

	void createEnemy(){
		Vector3 pos = enemySpawn [enmeySpawnIndex].position;
		pos [1] = 0f;
		Instantiate (enemyPrefab, pos, Quaternion.identity);
		enmeySpawnIndex = (enmeySpawnIndex + 1) % enemySpawn.Length;
	}
	GameObject initPlayer(Vector3 pos){
		GameObject gobj = Instantiate (playrs [playerIndex], pos, Quaternion.identity) as GameObject;
		//开始 对技能的ui初始化
		PlayerSkill skill = gobj.GetComponent<PlayerSkill> ();
		fightSkillUi.initPlayerSkillUi (skill.techSkill);
		return gobj;
	}
	public void changePlayer(int playerIndex){
		if (this.playerIndex == playerIndex) {
			return ;		
		}
		this.playerIndex = playerIndex;
		Vector3 selfPos = currentPlayer.transform.position;
		currentPlayer.SetActive (false);
		Destroy (currentPlayer);
		GameObject gobj = initPlayer (selfPos);
		cameraFight.refreshTarget = true;
		this.currentPlayer = gobj;
	}
}
