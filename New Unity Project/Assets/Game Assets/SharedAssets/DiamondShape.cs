using UnityEngine;
using System.Collections;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;

public class DiamondShape : MonoBehaviour {
		
	public float Height = 1.0f;
	public float Width = 0.5f;
	public float Density = 1.0f;
	
	private Body body;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void SetupPhysics() {
		if(body == null) {
			body = GetComponent<FSBodyComponent>().PhysicsBody;
			body.CreateFixture(CreateDiamond(Height, Width, Density));
		}
	}
	
	public static PolygonShape CreateDiamond(float height, float width, float density) {
		if(height <= 0.0f || width <= 0.0f) {
			print ("INVALID SIZE VALUE FOR DIAMOND CLASS");	
		}	
		FVector2[] pointList = new FVector2[4];
		pointList[0] = new FVector2(0.0f, height/2.0f);
		pointList[1] = new FVector2(-width/2.0f, 0.0f);
		pointList[2] = new FVector2(0.0f, -height/2.0f);
		pointList[3] = new FVector2(width/2.0f, 0.0f);
		PolygonShape shape = new PolygonShape(new Vertices(pointList), density);	
		return shape;
	}
	
}
