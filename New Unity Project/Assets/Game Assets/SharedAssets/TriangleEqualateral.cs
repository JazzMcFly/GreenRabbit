using UnityEngine;
using System.Collections;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Collision.Shapes;


public class TriangleEqualateral : MonoBehaviour {
	
	private static float SR3 = Mathf.Sqrt(3.0f);
		
	public float SideLength = 1.0f;
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

			body.CreateFixture(CreateEqlTriangle(SideLength, Density));
		}
	}
	
	public static PolygonShape CreateEqlTriangle(float sideLength, float density) {
		FVector2[] pointList = new FVector2[3];
		
		pointList[0] = new FVector2(0.0f, sideLength*0.25f*SR3);
		pointList[1] = new FVector2(sideLength*-0.5f, sideLength*-0.25f*SR3);
		pointList[2] = new FVector2(sideLength*0.5f, sideLength*-0.25f*SR3);
		PolygonShape shape = new PolygonShape(new Vertices(pointList), density);
		
		return shape;
	}
	
}
