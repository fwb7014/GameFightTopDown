using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginSceneScript : MonoBehaviour {
	public bool isClickLoadGame;
	public GameObject loadGamePannel;
	public Text curLoad;
	public Text maxLoad;
	public Slider slider;

	private int maxLoadNum;
	private AsyncOperation async;//异步加载详情

	void Awake(){
		maxLoadNum = 100;
		isClickLoadGame = false;
		slider.maxValue = maxLoadNum;
		slider.minValue = 0;
		curLoad.text = 0 +"";
		maxLoad.text = maxLoadNum+"";
	}

	public void OnClickStartGame(){
		isClickLoadGame = true;
		StartCoroutine ("LoadGameAsync");
	}

	IEnumerator LoadGameAsync(){
		async = Application.LoadLevelAsync ("FightScene");
		yield  return async;
	}

	void Update(){
		if (isClickLoadGame) {
			loadGamePannel.SetActive(true);
		} else {
			loadGamePannel.SetActive(false);
		}
		if (async != null) {
			int curLoadCount = (int)(async.progress * 100f);
			curLoad.text = curLoadCount+"";
			slider.minValue = curLoadCount;
		}
	}
}
