using UnityEngine;
using System.Collections;

public class AmmoGUI : MonoBehaviour {

	public float screenPositionX;
	public float screenPositionY;
	public float maxHeight;
	public float width;
	public Texture ammoTexture;
	public WeaponPlayer.WeaponInput weapontype;
	
	private PlayerStats pStats;
	private Rect textureBox;
	
	// Use this for initialization
	void Start () {
		pStats = GetComponent<PlayerStats>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	/// <summary>
	/// Draws the Ammo bar for the player.
	/// </summary>
	
	void OnGUI () {
	
		
		int screenHeight = Screen.height;
		int screenWidth = Screen.width;
		float actualHeight = pStats.GetAmmoCount(weapontype) / (float) pStats.GetMaxAmmoCount(weapontype) * (maxHeight*screenHeight);
		float offsetY = maxHeight*screenHeight - actualHeight;
		textureBox.Set(screenPositionX*screenWidth, (screenPositionY*screenHeight + offsetY), width*screenWidth, actualHeight);
		GUI.DrawTexture(textureBox, ammoTexture);
	}
}
