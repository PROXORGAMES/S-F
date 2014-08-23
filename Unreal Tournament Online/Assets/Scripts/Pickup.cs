using UnityEngine;
using System.Collections;


public class Pickup : MonoBehaviour {
	public enum type : int {Weapon, Hearth, Armor, Ammo};
	public type Type;
	public GameObject WeaponForeActive;
	public int AmmoPlus, ArmorPlus, HearthPlus;
	public Weapon wpn;
	public AudioClip SoundPickup;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		if(col.collider.tag == "Player")
		{
			switch (Type)
			{
			case type.Weapon:
				foreach(GameObject go in GameObject.FindGameObjectsWithTag("Weapon"))
				{
					go.SetActive(false);
				}
				WeaponForeActive.SetActive(true);
				AudioSource.PlayClipAtPoint(SoundPickup, transform.position);
				Destroy(gameObject);
				break;
			case type.Ammo:
		    if(wpn.BulletCount < wpn.MaxBulletCount)
				{
					wpn.BulletCount += AmmoPlus;
				AudioSource.PlayClipAtPoint(SoundPickup, transform.position);
				Destroy(gameObject);
				}
				break;
			case type.Armor:
				Controller cntrl = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
				if(cntrl._Armor < cntrl.MaxArmor)
				{
					cntrl._Armor += ArmorPlus;
				AudioSource.PlayClipAtPoint(SoundPickup, transform.position);
				Destroy(gameObject);
				}
				break;
			case type.Hearth:
				Controller cntrl1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller>();
				if(cntrl1._Hearth < cntrl1.MaxHearth)
				{
					cntrl1._Hearth += HearthPlus;
					AudioSource.PlayClipAtPoint(SoundPickup, transform.position);
					Destroy(gameObject);
				}
				break;
			
			}

		}

		
	}
}

