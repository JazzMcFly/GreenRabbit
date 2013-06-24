using UnityEngine;
using System.Collections;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class Chapter1Script : MonoBehaviour {
	
	//EnemyList
	public GameObject EnemyBasic;
	
	// Use this for initialization
	void Start () {
		for(int i = 0; i < 5; i++) {
			Scheduler enemy0 = Scheduler.SpawnEnemy(EnemyBasic, new FVector2((float)(2*i - 5.0f), 10.0f));
			enemy0.AddMovementEvent(new FVector2((float) (2*i) - 4.0f, 4.0f), 2.0f, 0, i*0.5f);
			enemy0.AddMovementEvent(new FVector2(-8.0f, 12.0f), 1.0f, 1, 2.5f, true); 
			enemy0.AddFireEvent(0.5f, 2, 2.0f + i*0.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
