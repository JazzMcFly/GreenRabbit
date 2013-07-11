using UnityEngine;
using System.Collections;

public class HealthGUI : MonoBehaviour {
	
	//All of these have to be the percentage of the total screen
	public float screenPositionX;
	public float screenPositionY;
	
	public float maxHeight;
	public float width;
	
	public Texture healthTexture;
	
	private Health objectHealth;
	private Rect textureBox;
	
	// Use this for initialization
	void Start () {
		objectHealth = GetComponent<Health>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	/// Draws the health bar for the player.
	/// </summary>
	
	void OnGUI () {
		int screenHeight = Screen.height;
		int screenWidth = Screen.width;
		float actualHeight = objectHealth.GetCurrentHealth() / objectHealth.GetMaxHealth() * (maxHeight*screenHeight);
		float offsetY = maxHeight*screenHeight - actualHeight;
		textureBox.Set(screenPositionX*screenWidth, (screenPositionY*screenHeight + offsetY), width*screenWidth, actualHeight);
		GUI.DrawTexture(textureBox, healthTexture);
	}
}
