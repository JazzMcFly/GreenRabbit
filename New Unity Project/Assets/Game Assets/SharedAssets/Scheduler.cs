using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Dynamics;


public delegate void ScheduleEvent();

public class Scheduler : MonoBehaviour {
	
	private Body body;

	// Use this for initialization
	void Start () {
		SetupPhysics();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//print ("body position" + body.Position);
	}
	
	private IEnumerator MovementEventHelper(FVector2 destination, float travelTime, int interpType = 0, float delay = 0.0f, bool guaranteeArrival = false ){
		print ("currentPosition: " + body.Position);
		
		if(delay > 0.0f) {
			yield return new WaitForSeconds(delay);
		}
		FVector2 speed = GetConstantSpeed(body.Position, destination, travelTime);
		body.LinearVelocity = speed;
		
		//Wait for movement to finish
		yield return new WaitForSeconds(travelTime);
		
		//We're done, stop the object
		body.LinearVelocity = FVector2.Zero;
		
		if(guaranteeArrival) {
			body.Position = destination;
		}
	}
	
	public void MovementEvent(FVector2 destination, float travelTime, int interpType = 0, float delay = 0.0f, bool guaranteeArrival = false ){
		SetupPhysics();
		StartCoroutine(MovementEventHelper(destination, travelTime, interpType, delay, guaranteeArrival));
	}
	
	private FVector2 GetConstantSpeed(FVector2 startPoint, FVector2 endPoint, float travelTime) {
			FVector2 distanceVector = endPoint - startPoint;
		print ("startPoint: " + startPoint);
		print ("startPoint: " + endPoint);
		print ("distance Vector: " + distanceVector);
		
			FVector2 speedVector = new FVector2(distanceVector.X/travelTime, distanceVector.Y/travelTime);
		
			return speedVector;
	}
	
	//Wrapper that allows me to get rid of that nasty return statement
	public IEnumerator WaitTime(float seconds) {
		yield return new WaitForSeconds(seconds);	
	}
	
	public static Scheduler SpawnEnemy(GameObject objectToSpawn, FVector2 position ) {
		GameObject newObj = (GameObject) Instantiate(objectToSpawn, new Vector3(position.X, position.Y, 0.0f), Quaternion.identity);
		newObj.AddComponent("Scheduler");
		
		return newObj.GetComponent<Scheduler>();
	}
	
	private void SetupPhysics() {
		if(body == null) {
			body = gameObject.GetComponent<FSBodyComponent>().PhysicsBody;	
		}
	}
}
