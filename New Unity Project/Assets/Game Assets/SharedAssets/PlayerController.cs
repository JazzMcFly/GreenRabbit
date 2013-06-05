using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class PlayerController : MonoBehaviour {
	
	public float moveSpeed = 3.0f;
	public float jumpSpeed = 2.0f;
	public float maxJumpTime = 1.0f;
	
	private Weapon[] weaponList;
	private Body body;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.FixedRotation = true;
		
		weaponList = GetComponentsInChildren<Weapon>();
		
		if(weaponList.Length == 0)
			print ("NO WEAPON");

	}
	
	// Update is called once per frame
	void Update () {
			//	body.Rotation += 0.05f;

		HorizontalInput();
		UpdateShotAngle();
	}
	
	private void HorizontalInput() {
		
		float temp = body.LinearVelocity.Y;
		
		if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
			if(Input.GetKey(KeyCode.A)) {
				body.LinearVelocity = new FVector2(-moveSpeed, temp);
			}
			if(Input.GetKey(KeyCode.D)) {
				body.LinearVelocity = new FVector2(moveSpeed, temp);
			}
		} else {
			body.LinearVelocity = new FVector2(0.0f, temp);		
		}
	}
	
	private void UpdateShotAngle() {
		Ray ray  = Camera.main.ScreenPointToRay(Input.mousePosition);
		Vector3 temp3 = ray.origin - gameObject.transform.position;
		FVector2 temp2 = new FVector2(temp3.x, temp3.y);
		temp2.Normalize();
		
		foreach(Weapon weap in weaponList) {
			weap.SetDirection(temp2);
		}
	}
}
