using UnityEngine;
using System.Collections;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class Chapter1Script : MonoBehaviour {
	
	//EnemyList
	public GameObject EnemyBasic;
	public GameObject EnemyWithLaser;
	public GameObject Boss;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(RunScript());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public IEnumerator RunScript() {
		for(int i = 0; i < 5; i++) {
			Scheduler enemy0 = Scheduler.SpawnEnemy(EnemyBasic, new FVector2((float)(2*i - 5.0f), 10.0f));
			enemy0.AddRelativeMovementEvent(new FVector2(0.0f, -3.0f), 1.0f, 0, i*0.5f);
			enemy0.AddMovementEvent(new FVector2(-8.0f, 12.0f), 1.0f, 1, 2.5f, true); 
			enemy0.AddFireEvent(0.5f, 2, 2.0f + i*0.5f);
		}
		yield return new WaitForSeconds(5.0f);
		
		Scheduler enemyLaserUser = Scheduler.SpawnEnemy(EnemyWithLaser, new FVector2( 0.0f, 10.0f));
		enemyLaserUser.AddRelativeMovementEvent (new FVector2(0.0f, -3.0f), 0.5f, 0, 0.0f, true);
		enemyLaserUser.AddFireEvent(6.0f, 2, 1.0f);
		
		yield return new WaitForSeconds(5.0f);
		
		Scheduler bossSched = Scheduler.SpawnEnemy(Boss, new FVector2(0.0f, 11.0f));
		bossSched.AddRelativeMovementEvent(new FVector2(0.0f, -2.0f), 0.5f, 0, 0.0f, true);
		
	}
	
	public IEnumerator WaitTime(float seconds) {
		yield return new WaitForSeconds(seconds);	
	}
}
