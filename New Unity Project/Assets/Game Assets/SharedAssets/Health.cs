using UnityEngine;
using System.Collections;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FVector2 = Microsoft.Xna.Framework.FVector2;

//Callbacks
public delegate void OnDamageTakenEvent(float damageReceived);
public delegate void OnDeathEvent();

public class Health : MonoBehaviour {
	
	public float maxHealth = 100.0f;
	public float regenRate = 0.0f;
	public float regenDelay = 1.0f;
	public bool invulnrable = false;
	public float spawnInvulnerableTime = 0.0f;
	
	[HideInInspector]
	public bool isDead = false;
	
	private float currHealth;
	private float invulnerableTimer;
	
	private float regenTimer;
	
	private Body body;
	
	//CallBack lists
	private Hashtable eventTable = new Hashtable();
	
	
	// Use this for initialization
	void Start () {
		SetupPhysics();
		currHealth = maxHealth;
		
		if(spawnInvulnerableTime > 0.0f) {
			invulnrable = true;
		}
		//Test to make sure callbacks work :)
		//this.OnDamageTaken += AnnounceDamage;
		//this.OnDeath += AnnounceDeath;
		//this.OnDeath += ReannounceDeath;
		
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
		
		//Regeneration for health
		if(regenTimer > 0.0f) {
			regenTimer -= Time.deltaTime;	
		} else {
			Heal (regenRate * Time.deltaTime);	
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
			ExecuteDamageTakenEvents(damage);
			ResetRegenTimer();
			if(currHealth <= 0.0f) {
				Kill ();
			}
		}
		return isDead;
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
		ExecuteDeathEvents();
	}
	
	public void ResetRegenTimer() {
		regenTimer = regenDelay;	
	}
	
    public event OnDamageTakenEvent OnDamageTaken
    {
        add
        {
			eventTable["DamageEvent"] = (OnDamageTakenEvent)eventTable["DamageEvent"] + value;
        }
        remove
        {
			eventTable["DamageEvent"] = (OnDamageTakenEvent)eventTable["DamageEvent"] - value;
        }
    }	
	
	private void ExecuteDamageTakenEvents(float damageAmount) {
		OnDamageTakenEvent temp = (OnDamageTakenEvent)eventTable["DamageEvent"];
		if(temp != null){
			temp(damageAmount);
		}
	}	
	
    public event OnDeathEvent OnDeath
    {
        add
        {
			eventTable["DeathEvent"] = (OnDeathEvent)eventTable["DeathEvent"] + value;
        }
        remove
        {
			eventTable["DeathEvent"] = (OnDeathEvent)eventTable["DeathEvent"] - value;
        }
    }
	
	private void ExecuteDeathEvents() {
		OnDeathEvent temp = (OnDeathEvent)eventTable["DeathEvent"];
		if(temp != null) {
			temp();
		}
	}	
	
	
	//makes sure that we have the body info to do most these functions. (Sometimes start doesn't finish before another function is called.
	//It that situation, body if often still null.
	private void SetupPhysics() {
		body = GetComponent<FSBodyComponent>().PhysicsBody;
		body.OnCollision += OnCollisionEvent;
	}
	
	private bool OnCollisionEvent(Fixture fixtureA, Fixture fixtureB, Contact contact) {
		
	/*	GameObject objectB = fixtureB.Body.UserFSBodyComponent.gameObject;
		
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
		}*/
		return true;	
	}
/*	
	private void AnnounceDeath() {
		print ("This Enemy is Dead");	
	}
	private void ReannounceDeath() {
		print ("Still Dead");	
	}
	private void AnnounceDamage(float damageAmount) {
		print ("I hurt this much: " + damageAmount);	
	}
*/
}
