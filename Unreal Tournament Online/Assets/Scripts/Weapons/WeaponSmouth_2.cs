using UnityEngine;
using System.Collections;

public class WeaponSmouth_2 : MonoBehaviour {
	private Vector3 def;
	public float smooth;
	public float amount;
	public float MaxAmount;
	// Use this for initialization
	void Start () {
		def = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {

		float factorX = -Input.GetAxis("Mouse X") * amount;
		float factorY = -Input.GetAxis("Mouse Y") * amount;
			transform.GetComponent<Animator>().enabled = false;
		if (factorX > MaxAmount)
			factorX = MaxAmount;
		
		if (factorX < -MaxAmount)
			factorX = -MaxAmount;
		
		if (factorY > MaxAmount)
			factorY = MaxAmount;
		
		if (factorY < -MaxAmount)
			factorY = -MaxAmount;

		Vector3 end = new Vector3(def.x+factorX, def.y+factorY, def.z);
		transform.localPosition = Vector3.Lerp(transform.localPosition, end, Time.deltaTime * smooth);

		if(transform.position.x == end.x && transform.position.y == end.y)
			transform.GetComponent<Animator>().enabled = true;
	}
}
