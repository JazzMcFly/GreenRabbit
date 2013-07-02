using UnityEngine;
using System.Collections;

public class BossCenterWeapMovement : MonoBehaviour {
	
	public float xOffset;
	
	public float yRotation;
	
	public float delay;
	
	// Use this for initialization
	void Start () {
		StartCoroutine(Movement());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	private IEnumerator Movement() {
		yield return new WaitForSeconds(0.2f);
		
		int sign = -1;
		while(true) {
			Vector3 tempPos = gameObject.transform.position;
			gameObject.transform.position = new Vector3(sign*xOffset + tempPos.x, tempPos.y, tempPos.z);
			gameObject.transform.Rotate(new Vector3(0.0f, yRotation, 0.0f));
			sign *= -1;
			yield return new WaitForSeconds(delay);
		}
	}
}
