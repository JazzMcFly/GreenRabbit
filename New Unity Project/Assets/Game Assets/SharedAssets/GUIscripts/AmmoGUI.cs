using UnityEngine;
using System.Collections;

public class AmmoGUI : MonoBehaviour {

	public float screenPositionX;
	public float screenPositionY;
	public float maxHeight;
	public int width;
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
		float actualHeight = pStats.GetAmmoCount(weapontype) / (float) pStats.GetMaxAmmoCount(weapontype) * maxHeight;
		
		float offsetY = maxHeight - actualHeight;
		textureBox.Set(screenPositionX, screenPositionY + offsetY, width, actualHeight);
		GUI.DrawTexture(textureBox, ammoTexture);
	}
}
