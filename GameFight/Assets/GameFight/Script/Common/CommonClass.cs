using UnityEngine;
using System.Collections;

public class CommonClass  {


}

public class ClickKind{
	public const int Normal = 1;
}

public class Tags{
	public  const string FLOOR = "Floor";
	public  const string PLAYER = "Player";
	public const string ENEMY = "Enemy";
	public const string EFS_MON = "efs_mon";
	public const string DETECT = "detect";

}

public class Layers{
	public const int PlayerWeapon = 8;
	public const int Enemy = 9;
	public const int EnemyWeapon = 10;
}


public class HashIds{
	public static int IdleState = Animator.StringToHash("Base Layer.idle");
	public static int MoveState = Animator.StringToHash("Base Layer.move");
	public static int Attack1State = Animator.StringToHash("Base Layer.attack1");
	public static int Attack2State = Animator.StringToHash("Base Layer.attack2");
	public static int Attack3State = Animator.StringToHash("Base Layer.attack3");


	public static int Dead = Animator.StringToHash("dead");
	public static int Skillid = Animator.StringToHash("skillid");
	public static int Speed = Animator.StringToHash("speed"); 
	public static int AttackCount = Animator.StringToHash("attackcount"); 
}
