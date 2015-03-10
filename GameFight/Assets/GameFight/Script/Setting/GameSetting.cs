using UnityEngine;
using System.Collections;

public class GameSetting : MonoBehaviour {
	public static GameSetting instance; 
	public Texture2D cursorNormal;

	public GameObject clickFxNormal;


	void Awake () {
		instance = this;
	}

	public void SetClickPos(int clickKind){
		if (clickKind == 0) {
					
		}
	}
}
