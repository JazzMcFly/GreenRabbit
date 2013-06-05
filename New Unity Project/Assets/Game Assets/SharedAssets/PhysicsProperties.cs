using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class PhysicsProperties : MonoBehaviour {

	public bool hasGravity = false;
	public bool pushable = false;
	
	private Body body;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void SetupPhysics() {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		if(!hasGravity){
			body.IgnoreGravity = true;
			body.GravityScale = 0.0f;
		}
		
		body.IsSensor = !pushable;
	}
}
