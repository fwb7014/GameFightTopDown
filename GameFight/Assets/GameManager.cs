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
	// Use this for initialization
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
}
