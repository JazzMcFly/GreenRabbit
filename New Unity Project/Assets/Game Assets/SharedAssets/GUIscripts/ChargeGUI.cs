using UnityEngine;
using System.Collections;

public class ChargeGUI : MonoBehaviour {
	
	public int width = 15;
	public int height = 5;
	public int offSetX = 0;
	public int offSetY = 10;
	public Texture[] stages;
		
	public GameObject target;
	public GameObject weaponObject; //the weapon associated with the UI

	private Weapon weapon;
	
	private Vector3 pixelCoordinates = new Vector3(0.0f,0.0f,0.0f);
	private Rect temp;
	private int maxWidth = 100;
	
	// Use this for initialization
	void Start () {
		maxWidth = width;
		weapon = weaponObject.GetComponent<Weapon>();
	}
	
	// Update is called once per frame
	void Update () {
		//Find the location of player on the screen
		pixelCoordinates = Camera.main.WorldToScreenPoint(target.transform.position);
		
		//Set the bar width to the appropriate amount of charge
		width = (int) (weapon.GetCurrentChargePercentage() * maxWidth);
		if(width > maxWidth) {
			width = maxWidth;	
		}
		
		temp.Set(pixelCoordinates.x - maxWidth/2 + offSetX, 
			Camera.main.GetScreenHeight() - pixelCoordinates.y - offSetY, 
			width, 
			height);
		
	}
	
	void OnGUI() {
		if(weapon.GetCurrentCharge() >= weapon.GetMaxCharge()) {
			GUI.DrawTexture(temp,(Texture) stages[2]);	
		} else if(weapon.GetCurrentCharge() >= weapon.GetMinCharge()) {
			GUI.DrawTexture(temp,(Texture) stages[1]);				
		} else {
			GUI.DrawTexture(temp,(Texture) stages[0]);				
		}
	}
	
	public void SetWidth(int w) {
		width = w;
	}
	
	
		
}
