using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TestCortinue : MonoBehaviour {

		private float currTime;

		public class CellEventFuntion
		{
			public float fTime;
			public CallBack callBack;
		}
		public delegate void CallBack();
		public delegate void CallBack<T>(T t);
		public delegate void CallBack<T, K>(T t1, K t2);
		
		public List<CellEventFuntion> eventList = new List<CellEventFuntion>();
		
		//设定一个根据时间调用的队列（每隔一定的时间调用不同的回调函数）
		public void CallFunctionListByTimes(bool flag)
		{
			eventList.Clear();

			//添加新的Event到我们的list中
			CellEventFuntion newFunction = new CellEventFuntion();
			newFunction.fTime = 2.0f;
			newFunction.callBack = Function01;
			
			CellEventFuntion newFunction02 = new CellEventFuntion();
			newFunction02.fTime = 2.0f;
			newFunction02.callBack = Function02;
			
			CellEventFuntion newFunction03 = new CellEventFuntion();
			newFunction03.fTime = 2.0f;
			newFunction03.callBack = Funciton03;
			
			CellEventFuntion newFunction04 = new CellEventFuntion();
			newFunction04.fTime = 5.0f;
			newFunction04.callBack = Function04;
			
			eventList.Add(newFunction);
			eventList.Add(newFunction02);
			eventList.Add(newFunction03);
			eventList.Add(newFunction04);
			
			//进行执行List中所有的Event
			
			//将会受到TimeScale的影响
			//StartCoroutine(_CallFuctionListByTimes());
			//不会受到TimeScale的影响
			currTime = 0;
		Debug.Log ("开始咯 flag="+flag);
			if (flag) {
				StartCoroutine(_CallFunctionListByTimeIgnoreTimeScale());
			} else {
				StartCoroutine(_CallFuctionListByTimes());
			}

		}
		
		private IEnumerator _CallFuctionListByTimes()
		{
			foreach (CellEventFuntion fuction in eventList)
			{
				float fTime = fuction.fTime;
			Debug.Log ("协同开始   预热");
				yield return new WaitForSeconds(fTime);
			Debug.Log ("协同开始   结束");
				if (fuction.callBack != null)
					fuction.callBack();
			}
		}
		
		private IEnumerator _CallFunctionListByTimeIgnoreTimeScale()
		{
			foreach (CellEventFuntion function in eventList)
			{
				float fTime = function.fTime;
			Debug.Log ("协同开始   预热");
				yield return StartCoroutine(_WaitTimeEnd(fTime));
			Debug.Log ("协同开始   结束");
				if (function.callBack != null)
					function.callBack();
				//can be replaced by function.(这段是和使用_WaitTimeEnd一样的效果)
				//float start = Time.realtimeSinceStartup;
				//while (Time.realtimeSinceStartup < start + fTime)
				//{
				//    yield return null;
				//}
			}
		}
		
		private IEnumerator _WaitTimeEnd(float fTime)
		{
			bool flag = true;
		float startTime = Time.realtimeSinceStartup;
		while (Time.realtimeSinceStartup<startTime+fTime)
			{
				yield return null;
			}
		}
		
		
		private void Function01()
		{
			//Debug.Log("--Call Function01**" + Time.time);
		Debug.Log("--Call Function01**" + Time.realtimeSinceStartup+"_"+currTime);
		}
		
		private void Function02()
		{
			//Debug.Log("--Call Function02**" + Time.time);
		Debug.Log("--Call Function02**" + Time.realtimeSinceStartup+"_"+currTime);
		}
		
		private void Funciton03()
		{
			//Debug.Log("--Call Function03**" + Time.time);
		Debug.Log("--Call Function03**" + Time.realtimeSinceStartup+"_"+currTime);
		}
		
		private void Function04()
		{
			//Debug.Log("--Call Function04**" + Time.time);
		Debug.Log("--Call Function04**" + Time.realtimeSinceStartup+"_"+currTime);
		}
		
		public void PauseGame()
		{
			Time.timeScale = 0.0f;
		}
		
		public void Resume()
		{
			Time.timeScale = 1.0f;
		}

	void Update(){
		//Debug.Log (" ===update==========="+Time.deltaTime+"_"+currTime);
		currTime += Time.deltaTime;
	}
		
		void OnDestroy()
		{
			StopAllCoroutines();  
		}
	
}
