using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;

public class Health : MonoBehaviour {
	
	public float maxHealth = 100.0f;
	public bool invulnrable = false;
	public float spawnInvulnerableTime = 0.0f;
	public bool isDead = false;
	
	private float currHealth;
	private float invulnerableTimer;
	
	private Body body;
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
		currHealth = maxHealth;
		
		if(spawnInvulnerableTime > 0.0f) {
			invulnrable = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(invulnrable) {
			spawnInvulnerableTime -= Time.deltaTime;
			if(spawnInvulnerableTime <= 0.0f) {
				invulnrable = false;	
			}
		}
		if(isDead) {
			GameObject.Destroy(gameObject);	
		}
	}
	
	public float GetCurrentHealth() {
		return currHealth;	
	}
	
	public float GetMaxHealth() {
		return maxHealth;	
	}
	
	public bool IsDead() {
		return isDead;	
	}
	
	public bool Damage(float damage) {
		if(!invulnrable) {
			currHealth -= damage;
			if(currHealth <= 0.0f) {
				isDead = true;
			}
		}
		return !invulnrable;
	}
	
	public void Heal(float healz) {
		currHealth += healz;
		if(currHealth >= maxHealth) {
			currHealth = maxHealth;	
		}
	}
	
	public void OverHeal(float healz) {
		currHealth += healz;	
	}
	
	public void HealToMax() {
		currHealth = maxHealth;	
	}
	
	public void Kill() {
		currHealth = 0.0f;
		isDead = true;
	}
	
	private void SetupPhysics() {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.OnCollision += OnCollisionEvent;
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		
		GameObject objectB = fixtureB.Body.UserFSBodyComponent.gameObject;
		
		if(objectB.tag == "Bullet") {
			ProjectileBasic bulletInfo = objectB.GetComponent<ProjectileBasic>();
			if(bulletInfo != null) {
				Damage(bulletInfo.GetDamage());
			} else {
				print("Object is tagged as \"Bullet\" but does not have the associated component"); 	
			}
		} else if(objectB.tag == "Explosion") {
			Explosion explosionInfo = objectB.GetComponent<Explosion>();
			if(explosionInfo != null) {
				Damage(explosionInfo.GetDamage());	
			} else {
				print ("Object is tagged as \"Explosion\" but does not have the associated component");	
			}
		}
		return true;	
	}

}