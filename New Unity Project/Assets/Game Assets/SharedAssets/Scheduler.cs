using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FVector2 = Microsoft.Xna.Framework.FVector2;
using FarseerPhysics.Dynamics;


public delegate void ScheduleEvent();

public struct MovementEvent {
	public float travelTime;
	public float delay;
	public int interpType;
	public FVector2 destination;
	public bool guaranteeArrival;
}

public struct FireEvent {
	public float fireRate;
	public float delay;
	public int shotCount;
	
}

public class Scheduler : MonoBehaviour {

	private LinkedList<MovementEvent> movementEventList = new LinkedList<MovementEvent>();
	//private LinkedList<float> movementEventTimers = new LinkedList<float>(); //maybe...
	private bool movementActive = false;
	
	//private LinkedList<FireEvent> fireEventList =  new LinkedList<FireEvent>();
	//private LinkedList<float> fireEventTimers = new LinkedList<float>();
	
	private Body body;

	// Use this for initialization
	void Start () {
		SetupPhysics();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//print ("body position" + body.Position);
	}
	
	private void StartMovementEvent() {
		StartCoroutine("ExecuteMovementEvent");	
	}
	
	private IEnumerator ExecuteMovementEvent() {
		//print ("execute");
		while(movementEventList.First != null) {
			//print ("true");
			movementActive = true;
			yield return StartCoroutine(MovementEventHelper(movementEventList.First.Value));
			//print ("still true");
			movementEventList.RemoveFirst();
		}
		//print ("out of loop");
		movementActive = false;
	}
	
	private IEnumerator MovementEventHelper(MovementEvent move){
		//print ("currentPosition: " + body.Position);
		//print ("should be moving");

		if(move.delay > 0.0f) {
			yield return new WaitForSeconds(move.delay);
		}
		FVector2 speed = GetConstantSpeed(body.Position, move.destination, move.travelTime);
		body.LinearVelocity = speed;
		//Wait for movement to finish
		yield return new WaitForSeconds(move.travelTime);
		
		//We're done, stop the object
		body.LinearVelocity = FVector2.Zero;
		
		if(move.guaranteeArrival) {
			body.Position = move.destination;
		}
	}
	
	public void AddMovementEvent(FVector2 destination, float travelTime, int interpType = 0, float delay = 0.0f, bool guaranteeArrival = false ){
		SetupPhysics();
		MovementEvent move = new MovementEvent();
		move.destination = destination;
		move.travelTime = travelTime;
		move.interpType = interpType;
		move.delay = delay;
		move.guaranteeArrival = guaranteeArrival;
		movementEventList.AddLast(move);
		
		if(!movementActive) {
			StartMovementEvent();	
		}
	}
	
	
	
	private FVector2 GetConstantSpeed(FVector2 startPoint, FVector2 endPoint, float travelTime) {
		FVector2 distanceVector = endPoint - startPoint;
		//print ("startPoint: " + startPoint);
		//print ("startPoint: " + endPoint);
		//print ("distance Vector: " + distanceVector);
		FVector2 speedVector = new FVector2(distanceVector.X/travelTime, distanceVector.Y/travelTime);	
		return speedVector;
	}
	
	public void FireEvent() {
		SetupPhysics();	
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
