using UnityEngine;
using System.Collections;


public class Weapon : MonoBehaviour {
	
	public enum Mode { NormalGun, ExlopsionGun, SniperGun, ShootGun };
	public Mode GunMode;
	public Animator animator;
	public float SpeedFire, spread, DistanceFire;
	public int BulletCount, MaxBulletCount;
	float _speedFire;
	public AudioSource source;
	public AudioClip Shoot, Empty;
	public GameObject fire;
	public Transform FirePoint;
	Core core;
	public Texture2D Aim;
	public float width, height;
	public Texture2D Icon, IconAcive, IconNoBullet;
	int number, nb, i, d;
	bool end, _fire;

	// Use this for initialization
	void Start () {
		core = GameObject.FindGameObjectWithTag("Player").GetComponent<Core>();
		switch (GunMode)
		{
		case Mode.NormalGun:
			i = 1;
			break;
		case Mode.ShootGun:
			i = 8;
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if(BulletCount > MaxBulletCount)
			BulletCount = MaxBulletCount;

		if(core.wpns.Length > 0 && number < core.wpns.Length)
		{
			if(core.wpns[number] == null && !end)
			{
				core.wpns[number] = gameObject;
				nb = number;
				core.Active[number] = IconAcive;
				core.Normal[number] = Icon;
				core.NoBullet[number] = IconNoBullet;
				end = true;
			}
			number++;
		
		}

		core.CurentWeapon = nb;
		if(BulletCount <= 0)
		{
			BulletCount = 0;
			core.noBullet[nb] = true;
		}

		core.aim = Aim;
		core.w = width;
		core.h = height;
		core.Bullets = BulletCount;
		_speedFire -= Time.deltaTime;



		if(Input.GetMouseButton(0))
		{


			
			if(_speedFire <= 0)
			{
				if(BulletCount > 0)
				{
					animator.SetBool("Idle", false);
					animator.SetBool("Fire", true);
					animator.SetBool("Run", false);
				source.PlayOneShot(Shoot);
				_speedFire = SpeedFire;
				GameObject go;
				go = Instantiate(fire, FirePoint.position, FirePoint.rotation) as GameObject;
				BulletCount--;
				go.transform.parent = transform;
					d = 0;
					_fire = true;
				}
			

			}
		}
		else
		{

			animator.SetBool("Fire", false);
			if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			animator.SetBool("Idle", false);
			else
			animator.SetBool("Idle", true);

		}

		if(_fire)
		{
			if(d < i)
			{
				RaycastHit hit;
				Ray ray  = Camera.main.ScreenPointToRay(new Vector3(Random.Range(Screen.width/2+spread, Screen.width/2-spread), Random.Range(Screen.height/2+spread, Screen.height/2-spread), 0));
				

				if(Physics.Raycast(ray.origin  , ray.direction, out hit))
				{
					if(hit.distance <= DistanceFire)
					{
					if(hit.transform.tag == core._NormalWeapons.BrickTag)
					{
						Instantiate(core._NormalWeapons.BrickImpact[Random.Range(0, core._NormalWeapons.BrickImpact.Length)], hit.point, Quaternion.Euler(transform.forward));
						PlayAudio(core._NormalWeapons.BrickSound[Random.Range(0, core._NormalWeapons.BrickSound.Length)], hit.point);
					}
					if(hit.transform.tag == core._NormalWeapons.MetalTag)
					{
						Instantiate(core._NormalWeapons.MetalImpact[Random.Range(0, core._NormalWeapons.MetalImpact.Length)], hit.point, Quaternion.Euler(transform.forward));
						PlayAudio(core._NormalWeapons.MetalSound[Random.Range(0, core._NormalWeapons.MetalSound.Length)], hit.point);
					}
					if(hit.transform.tag == core._NormalWeapons.WoodTag)
					{
						PlayAudio(core._NormalWeapons.WoodSound[Random.Range(0, core._NormalWeapons.WoodSound.Length)], hit.point);
						Instantiate(core._NormalWeapons.WoodImpact[Random.Range(0, core._NormalWeapons.WoodImpact.Length)], hit.point, Quaternion.Euler(transform.forward));
					}
					if(hit.transform.tag == core._NormalWeapons.EnemyTag)
					{
						PlayAudio(core._NormalWeapons.BrickSound[Random.Range(0, core._NormalWeapons.BrickSound.Length)], hit.point);
						Instantiate(core._NormalWeapons.BrickImpact[Random.Range(0, core._NormalWeapons.BrickImpact.Length)], hit.point, Quaternion.Euler(transform.forward));
					}
					
				}
				d++;
				}
			}
			else
				_fire = false;
		}

	}




	void PlayAudio(AudioClip au, Vector3 position)
	{
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.transform.position = position;
		go.GetComponent<Collider>().enabled = go.GetComponent<MeshRenderer>().enabled = false;
		go.AddComponent<AudioSource>().volume = core._NormalWeapons.volume;
		go.GetComponent<AudioSource>().minDistance = core._NormalWeapons.MaxDistance;
		go.GetComponent<AudioSource>().maxDistance = core._NormalWeapons.MaxDistance;
		go.audio.PlayOneShot(au);
		Destroy(go, 1.5f);
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, DistanceFire);
	}

}
