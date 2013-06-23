using UnityEngine;
using System.Collections;

public class HealthGUI : MonoBehaviour {
	
	public float screenPositionX;
	public float screenPositionY;
	
	public float maxHeight;
	public int width;
	
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
		float actualHeight = objectHealth.GetCurrentHealth() / objectHealth.GetMaxHealth() * maxHeight;
		float offsetY = maxHeight - actualHeight;
		textureBox.Set(screenPositionX, screenPositionY + offsetY, width, actualHeight);
		GUI.DrawTexture(textureBox, healthTexture);
	}
}
