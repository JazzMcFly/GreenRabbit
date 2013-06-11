using UnityEngine;
using System.Collections;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;

public class TriangleIsosceles : MonoBehaviour {

	//private static float SR2 = Mathf.Sqrt(2.0f);
		
	public float SideLength = 1.0f;
	public float Angle = 15.0f;
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
			body.CreateFixture(CreateIsoTriangle(this.SideLength, this.Angle, this.Density));
		}
	}
	
	public static PolygonShape CreateIsoTriangle(float sideLength, float angle, float density){
		if(sideLength <= 0.0f) {
			print ("SIDE LENGTH FOR ISOSCELES TRIANGLE CLASS MUST BE POSITIVE");
		}			
		if(angle <= 0.0f || angle >= 180.0f) {
			print ("INVALID ANGLE VALUE FOR ISOSCELES TRIANGLE CLASS");	
		}
		FVector2[] pointList = new FVector2[3];
			
		float theta = Mathf.PI/360.0f*angle; //using half the angle size
			
		float halfHeight = sideLength*Mathf.Cos(theta) / 2.0f; 
		float halfWidth = sideLength*Mathf.Sin(theta);
			
		pointList[0] = new FVector2(0.0f, halfHeight);
		pointList[1] = new FVector2(-halfWidth, -halfHeight);
		pointList[2] = new FVector2(halfWidth, -halfHeight);
		PolygonShape shape = new PolygonShape(new Vertices(pointList), density);
		
		return shape;
	}
}
