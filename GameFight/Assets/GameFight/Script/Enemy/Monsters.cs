using UnityEngine;
using System.Collections;

public class Monsters : MonoBehaviour {
	public Material hp_bar;
	public GameObject damageTipPfb;

	public Transform[] damageTips = new Transform[10];
	private int damageTipsIndex = 0;


	void Awake(){
		Debug.Log ("damageTipPfb="+damageTipPfb);
		for (int i = 0; i<10; i++) {   
			damageTips[i] = (Instantiate(damageTipPfb,Vector3.up*4f,Quaternion.identity) as GameObject).transform;		
		}
	}

	public void SetDamageNum (Vector3 pos,int damage,Vector3 dir){
		this.damageTips [damageTipsIndex].GetComponent<DamageNum> ().ShowNum (pos, damage, dir);
		damageTipsIndex = (damageTipsIndex + 1) % 10;
	}


	
	public Transform CreatHpbar(Vector2 _size, bool _turretmode, bool _ally)
	{
		GameObject gameObject = new GameObject("hp_bar");
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		Mesh mesh = new Mesh();
		gameObject.AddComponent<MeshRenderer>();
		mesh.vertices = new Vector3[]
		{
			new Vector3(-_size.x,0f , _size.y) * 0.5f,
			new Vector3(_size.x, 0f, _size.y) * 0.5f,
			new Vector3(-_size.x,-0.02f,  -_size.y) * 0.5f,
			new Vector3(_size.x, -0.02f, -_size.y) * 0.5f
		};
		float num = 0f;
		if (_ally)
		{
			num = 0.75f;
		}
		mesh.uv = new Vector2[]
		{
			new Vector2(0f, 0.25f + num),
			new Vector2(0.5f, 0.25f + num),
			new Vector2(0f, num),
			new Vector2(0.5f, num)
		};
		Renderer renderer = gameObject.renderer;
		renderer.receiveShadows = false;
		renderer.castShadows = false;
		renderer.sharedMaterial = this.hp_bar;
		mesh.triangles = new int[]
		{
			0,
			1,
			2,
			2,
			1,
			3
		};
		mesh.RecalculateNormals();
		meshFilter.mesh = mesh;
		gameObject.transform.parent = transform;
		gameObject.AddComponent ("Hp_bar");
		return gameObject.transform;
	}
}
