using UnityEngine;
using System.Collections;

public class ProjectileCluster : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
		if(CountChildren(gameObject.transform) <= 0){
			Destroy(gameObject);	
		}
	}
	
	private int CountChildren(Transform trans) {
		int childCount = 0;
		
		foreach (Transform c in trans) {
			childCount++;
		}
		
		return childCount;
	}
}
