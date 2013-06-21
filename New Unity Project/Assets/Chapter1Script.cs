using UnityEngine;
using System.Collections;
using FVector2 = Microsoft.Xna.Framework.FVector2;


public class Chapter1Script : MonoBehaviour {
	
	//EnemyList
	public GameObject EnemyBasic;
	
	// Use this for initialization
	void Start () {
		Scheduler enemy0 = Scheduler.SpawnEnemy(EnemyBasic, new FVector2(0.0f, 10.0f));
		enemy0.AddMovementEvent(new FVector2(3.0f, 2.0f), 2.0f);
		enemy0.AddMovementEvent(new FVector2(-4.0f, 10.0f), 1.0f, 1, 2.5f, true); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
