using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;


//Armor shell for enemy that blocks/disarms player shots
//Much like a mobile piece of scenery that the enemy is carrying
public class ArmorFan : MonoBehaviour {
	
	public float radius;
	public float maxAngle;
	public int shardCount;	
	
	public float rotationRate;
	
	private Body body;
	private float rectWidth;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
		for(int i = 0; i < shardCount; i++) {
			PolygonShape shape = TriangleIsosceles.CreateIsoTriangle(radius, maxAngle/shardCount, 1.0f);
			
			body.CreateFixture(shape);				
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void SetupPhysics() {
		if(body == null) {
			body = GetComponent<FSBodyComponent>().PhysicsBody;	
		}
	}
}
