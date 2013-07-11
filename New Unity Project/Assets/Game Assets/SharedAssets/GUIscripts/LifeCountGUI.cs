using UnityEngine;
using System.Collections;

public class LifeCountGUI : MonoBehaviour {
	
	
	public float screenPositionX;
	public float screenPositionY;
	public float height;
	public float width;
	
	private PlayerStats pStats;
	
	// Use this for initialization
	void Start () {
		pStats = gameObject.GetComponent<PlayerStats>();
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		int screenHeight = Screen.height;
		int screenWidth = Screen.width;
		
		GUI.TextField(new Rect(screenPositionX*screenWidth, screenPositionY*screenHeight, width*screenWidth, height*screenHeight), 
			"Lives : " + pStats.LifeCount);
		
	}
}
