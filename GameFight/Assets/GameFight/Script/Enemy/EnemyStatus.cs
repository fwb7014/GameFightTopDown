using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyStatus : MonoBehaviour {

	public float height = 1.5f;
	public Vector2 barSize = new Vector2(0.8f,0.2f);
	public float walkSpeed;
	public EnemyKind kind;//类型


	//服务器数据
	public int force = 5;
	public int smart = 5;
	public int defence = 5;
	public int level = 1;
	//计算的数据
	public int gjl;
	public int fyl;
	public int hp;
	public int maxHp;
	public int bjl;
	public int sbl;

	// Use this for initialization
	void Start () {
		getStatusNumber ();
	}
	
	// Update is called once per frame
	void Update () {

	}


	//成长属性
	public enum EnemyKind{Normal1_1,Boss1_1};
	[System.Serializable]
	public class EnemyBase{
		public EnemyKind kind;
		public float forceFactor;
		public float smartFactor;
		public float defenceFactor;
		public float hpFactor;
	}
	public EnemyBase[] enemybase;

	public void getStatusNumber(){
		Dictionary<string,int> result = null;
		EnemyBase baseEn = null;
		for (int i = 0; i<enemybase.Length; i++) {
			if(enemybase[i].kind == kind){
				baseEn = enemybase[i];
				break;
			}
		}
		if (baseEn == null)
			return ;
		hp = maxHp = (int)(level * (10 + baseEn.hpFactor) + defence * (10 + baseEn.defenceFactor));
		gjl = (int)(force * 4);
		fyl = (int)(defence * 4);
		bjl = (int)(smart * (3.5 + baseEn.smartFactor) + force * (0.2 + baseEn.smartFactor));
		sbl = (int)(smart * (2 + baseEn.smartFactor) + defence * (0.2 + baseEn.smartFactor));
	}
}
