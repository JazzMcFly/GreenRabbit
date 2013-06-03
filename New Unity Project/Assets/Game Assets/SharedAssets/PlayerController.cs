using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class PlayerController : MonoBehaviour {
	
	private Body body;
	
	
	public float moveSpeed = 3.0f;
	public float jumpSpeed = 2.0f;
	public float maxJumpTime = 1.0f;
	
	private Weapon primary;
	
	// Use this for initialization
	void Start () {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.FixedRotation = true;
		
		primary = GetComponent<Weapon>();
		
		if(primary = null)
			print ("NO WEAPON");

	}
	
	// Update is called once per frame
	void Update () {
		HorizontalInput();
		PrimaryInput();
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
	
	private void PrimaryInput() {
		if(Input.GetKey(KeyCode.Mouse0)){
			primary.Fire();
			print ("FIRE");
		}
	}
	
	private void SecondaryInput() {
		
	}
}
