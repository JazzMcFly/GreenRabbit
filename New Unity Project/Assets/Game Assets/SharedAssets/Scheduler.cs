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
	private bool movementActive = false;
	private Body body;

	
	private LinkedList<FireEvent> fireEventList =  new LinkedList<FireEvent>();
	private bool fireActive = false;
	private AIWeapon weapon;
	

	// Use this for initialization
	void Start () {
		SetupPhysics();
		SetupWeapon();
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
	
	public void AddRelativeMovementEvent(FVector2 distanceVector, float traveltime, int interType = 0, float delay = 0.0f, bool guaranteeArrival = false) {
		SetupPhysics();
		FVector2 destination = new FVector2(body.Position.X + distanceVector.X, body.Position.Y + distanceVector.Y);
		AddMovementEvent(destination, traveltime, interType, delay, guaranteeArrival);
	}
	
	private FVector2 GetConstantSpeed(FVector2 startPoint, FVector2 endPoint, float travelTime) {
		FVector2 distanceVector = endPoint - startPoint;
		FVector2 speedVector = new FVector2(distanceVector.X/travelTime, distanceVector.Y/travelTime);	
		return speedVector;
	}
	
	public void AddFireEvent(float fireRate, int shotCount, float delay = 0.0f) {
		SetupPhysics();
		SetupWeapon();
		FireEvent fireEvent = new FireEvent();
		fireEvent.fireRate = fireRate;
		fireEvent.shotCount = shotCount;
		fireEvent.delay = delay;
		fireEventList.AddLast(fireEvent);
		
		if(!fireActive){
			StartFireEvent();	
		}
	}

	private void StartFireEvent() {
		StartCoroutine("ExecuteFireEvent");	
	}
	
	private IEnumerator ExecuteFireEvent() {
		//print ("execute");
		while(fireEventList.First != null) {
			//print ("true");
			fireActive = true;
			yield return StartCoroutine(FireEventHelper(fireEventList.First.Value));
			//print ("still true");
			fireEventList.RemoveFirst();
		}
		//print ("out of loop");
		fireActive = false;
	}	
	
	
	private IEnumerator FireEventHelper(FireEvent fireEvent){

		if(fireEvent.delay > 0.0f) {
			yield return new WaitForSeconds(fireEvent.delay);
		}
		
		for(int i = 0; i < fireEvent.shotCount; i++) {
			weapon.Fire();
			yield return new WaitForSeconds(fireEvent.fireRate);
		}
		
		//shot count of -1 means shoot forever
		if(fireEvent.shotCount == -1) {
			while(true) {
				weapon.Fire();
				yield return new WaitForSeconds(fireEvent.fireRate);
			}
		}
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
	
	private void SetupWeapon() {
		if(weapon == null) {
			weapon = gameObject.GetComponent<AIWeapon>();	
		}
		
		if(weapon == null) {
			print ("still null");	
		}
	}
}
