using UnityEngine;
using System.Collections;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision.Shapes;
using FVector2 = Microsoft.Xna.Framework.FVector2;

//this script should be attached to a GameObject that has a body attached but no fixtures.
//Lasers should be kinematic objects!!!!

public class Laser : MonoBehaviour {
	
	public static int edgeCount = 5;
	
	public float warmupTime = 0.0f;
	public float lifeSpan = 2.0f;
	public float width = 0.25f;
	private float length = 50.0f;
	public float damage = 1000.0f;
	
	public float startingAngle = 0.0f;
	public float sweepAngle = 0.0f;
	public float sweepDelay = 0.0f;
	public float sweepTime = 0.0f;
	
	//Stuff needed for rotation and movement of the laser
	private GameObject anchor;
	public bool followAnchor = true;
	public float offsetRadius = 0.0f;
	private Body anchorBody;
	
	private Body body;
	
	
	// Use this for initialization
	void Start () {
		//yield return StartCoroutine(FadeIn(warmupTime));
		SetupPhysics();
		SetAngle(startingAngle);
		StartCoroutine(SetupLifeSpan(lifeSpan));
		StartCoroutine(Sweep(sweepAngle, sweepTime));
		 
	}
	
	// Update is called once per frame
	void Update () {
		if(followAnchor == true ) {
			UpdatePositionToAnchor();	
		}
	
	}
	
	/// <summary>
	/// Sweep the laser with specified degreesToSweep and sweepTime.
	/// </summary>
	/// <param name='degreesToSweep'>
	/// Degrees to sweep.
	/// </param>
	/// <param name='sweepTime'>
	/// Sweep time.
	/// </param>
	public IEnumerator Sweep(float degreesToSweep, float sweepTime) {
		if(degreesToSweep > 0.0f || degreesToSweep < 0.0f) {
			float angularVelocityInRadians = degreesToSweep / 180.0f * Mathf.PI / sweepTime;
			yield return new WaitForSeconds(sweepDelay);

			body.AngularVelocity = angularVelocityInRadians;

			yield return new WaitForSeconds(sweepTime);
		
			body.AngularVelocity = 0.0f;
		}
	}
	
	public void SetAngle(float angleInDegrees) {
		body.Rotation = angleInDegrees / 180.0f * Mathf.PI;	
	}
	
	private void UpdatePositionToAnchor() {
		if(anchor != null){
			body.Position = new FVector2(anchor.transform.position.x, anchor.transform.position.y);	
		}
	}
	
	
	private IEnumerator FadeIn(float waitTime) {
		yield return new WaitForSeconds(waitTime);
	}
	
	public void SetAnchor(GameObject host) {
		anchor = host;
		
	}
	
	/// <summary>
	/// Setups the life span. Destroys itself when it the timer runs out the laser destroys itself. Later should call a FadeOut sequence rather than destruction
	/// </summary>
	/// <param name='lifeSpan'>
	/// Life span.
	/// </param>
	private IEnumerator SetupLifeSpan(float lifeSpan) {
		yield return new WaitForSeconds(lifeSpan);
		GameObject.Destroy(gameObject);
	}
	
	/// <summary>
	/// Setups the physics. The default directionf of this laser is pointing straight down!! 0 degrees is pointing straight down!!
	/// </summary>
	private void SetupPhysics() {
		if(body == null) {
			body = GetComponent<FSBodyComponent>().PhysicsBody;
			Vertices laserPoints = PolygonTools.CreateCapsule(length, width, edgeCount);
			laserPoints.Translate(new FVector2(0.0f, -(offsetRadius + length/2.0f)));
			 
			Fixture fix = body.CreateFixture(new PolygonShape(laserPoints, 1.0f));
			CollisionPresets.SetAsEnemyShot(fix);
			body.OnCollision += OnCollisionEvent;
			
		}
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
	
		Health health = fixtureB.Body.UserFSBodyComponent.gameObject.GetComponent<Health>();
		if(health != null) {
			print ("LAZER DAMAGE");
			health.Damage(damage);
		}
		return false;
	}
}
