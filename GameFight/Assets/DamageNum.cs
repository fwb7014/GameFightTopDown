using UnityEngine;
using System.Collections;

public class DamageNum : MonoBehaviour {
	private TextMesh txtMesh;
	public float upSpeed = 0.3f;
	public float finishDelay = 1f;
	private float finishDelaySelf;
	public float fadeSpeed = 2f;
	private Vector3 dir;

	void Awake(){
		gameObject.SetActive (false);
		txtMesh = GetComponent<TextMesh> ();
		finishDelaySelf = finishDelay;
	}

	public void ShowNum(Vector3 pos,int DamageNum,Vector3 _dir){
		txtMesh.text = DamageNum.ToString ();
		dir = _dir;
		gameObject.SetActive (true);
		transform.position = pos + Vector3.up * 0.2f;
	}
	
	void Update () {
		finishDelay -= Time.deltaTime * fadeSpeed;
		if (finishDelay < 0f) {
			finishDelay = finishDelaySelf;
			gameObject.SetActive(false);
		}
		transform.position += (Vector3.up + dir) * Time.deltaTime * upSpeed;
	}
}
