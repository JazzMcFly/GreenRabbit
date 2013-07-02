using UnityEngine;
using System.Collections;

public class Spoils : MonoBehaviour {
	
	public GameObject [] spoilTypes;
	public int [] spoilAmounts;
	
	// Use this for initialization
	void Start () {
		Health health = GetComponent<Health>();
		health.OnDeath += OnDeathEvent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private void OnDeathEvent() {
		Vector3 pos = gameObject.transform.position;
		for(int i = 0; i < spoilTypes.Length; i++) {
			for(int count = 0; count < spoilAmounts[i]; count++) {
				Vector2 offset = Random.insideUnitCircle;
				Instantiate(spoilTypes[i], new Vector3(pos.x + offset.x, pos.y + offset.y, pos.z), gameObject.transform.rotation);
			}
		}
	}
}
