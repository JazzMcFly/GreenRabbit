using UnityEngine;
using System.Collections;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;

public class ArmorShard : MonoBehaviour {

	public float length;
	public float angle;
	
	private Body body;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void SetupPhysics() {
		if(body == null) {
			body = GetComponent<FSBodyComponent>().PhysicsBody;	
			body.OnCollision += OnCollisionEvent;
			
			Vertices vertices = TriangleIsosceles.CreateVertexList(length, angle);
			FVector2 firstPoint = vertices.NextVertex(0);
			vertices.Translate(new FVector2(-firstPoint.X, -firstPoint.Y));
			vertices.Rotate(Mathf.PI/180.0f * angle);
		
			PolygonShape polyTemp = new PolygonShape(vertices, 1.0f);
			body.CreateFixture(polyTemp);
		}
	}
	
	public void Resize(float width, float height) {

	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		

		
		return true;	
	}
}
