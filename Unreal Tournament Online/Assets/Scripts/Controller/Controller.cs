using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (CharacterController))]
[RequireComponent (typeof (AudioSource))]
public class Controller : MonoBehaviour {
	public float _speed, SetSpeed, JumpPower, DoubleJumpPower, TimeOutDoubleJump , gravity, _Hearth, _Armor, MaxArmor, MaxHearth;
	public Texture2D LeftBar, Armor, Hearth, Kills, Timer;
	float speed, stepTime;
	public Animator animator;
	public List<Footsteps> footsteps;
	CharacterController controller, cntrl;
	public AudioSource source;
	public float StepTime, StepTimeSet;
	bool go, jump;
	public float anim;
	public Transform _Camera, _Camera2;
	private Vector3 moveDirection = Vector3.zero;
	public GUISkin skin;
	bool grounded, tm;
	float td, timer;
	int kills;
	// Use this for initialization
	void Start () {
		controller = (CharacterController)this.GetComponent<CharacterController>();
		cntrl = controller;
		td = TimeOutDoubleJump;
	}
	
	// Update is called once per frame


	void Update () {
		OnDrawRay();
		_Camera.localPosition = _Camera2.localPosition = new Vector3 (0,anim,0);
	
		if(_Armor > MaxArmor)
			_Armor = MaxArmor;
		if(_Hearth > MaxHearth)
			_Hearth = MaxHearth;

		if (controller.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			
			if (Input.GetButton ("Jump"))
			
				moveDirection.y = JumpPower;


			
		}
		if(!controller.isGrounded && !jump && !grounded && !tm)
		{
			if (Input.GetKeyDown ("space"))
			{
				jump = true;
				moveDirection.y += DoubleJumpPower;
			}


		}


		if(tm)
		{
			td -= Time.deltaTime;
			if(td <= 0)
			{
				tm = false;
				td = TimeOutDoubleJump;
			}
		}
		
		moveDirection.y -= gravity * Time.deltaTime;
		CollisionFlags flags = controller.Move(moveDirection * Time.deltaTime);

		if(Input.GetKey(KeyCode.LeftControl))
		{
			if(anim > 0.05f)
			anim -= 0.04f;
			speed = SetSpeed;
			stepTime = StepTimeSet;
		}
		else
		{
			if(anim < 0.9f)
				anim += 0.04f;
			speed = _speed;
			stepTime = StepTime;
		}


		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && controller.isGrounded)
		{
			//jump = false;
			animator.enabled = true;
			animator.SetBool("Go", true);
			animator.SetBool("Stay", false);
			Weapon wpn;
			wpn = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
			if(!Input.GetMouseButton(0))
				wpn.animator.SetBool("Run", true);
			wpn.animator.SetBool("Idle", false);
			//animator.enabled = true;
			OnDrawRay();
			go = true;
		}
		else
		{
			Weapon wpn;
			wpn = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
			if(wpn == null)
			Debug.Log("null");
			wpn.animator.SetBool("Run", false);
			if(!Input.GetMouseButton(0))
				wpn.animator.SetBool("Idle", true);
			animator.SetBool("Go", false);
			animator.SetBool("Stay", true);
			animator.enabled = false;
			go = false;
		}

		if(_Hearth <= 0)
			Application.LoadLevel(Application.loadedLevel);

	}


	void OnDrawRay()
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, Vector3.down , out hit))
		{
			//Debug.Log(hit.distance);
			if(hit.distance <= 2)
			{
				if(jump)
				{
				tm = true;
				jump = false;
				}
				grounded = true;
			}
			else
			
				grounded = false;
			
		foreach(Footsteps st in footsteps)
		{
		if(hit.transform.tag == st.TagMeterial && go && controller.isGrounded)
			{
				AudioClip au = st.FootSteepsSound[Random.Range(0, st.FootSteepsSound.Length)];
				StartCoroutine(DelayPlay(au));
			}
			}
	}

		RaycastHit hit_2;
		if(Physics.Raycast(transform.position, Vector3.up , out hit_2))
		{
			if(hit_2.distance <= 1.2f)
			{
				moveDirection.y = -JumpPower/3;
			}
		}

	}

	public IEnumerator DelayPlay(AudioClip sound)
	{
		yield return new WaitForSeconds(stepTime);
		if(go)
		source.PlayOneShot(sound);
		StopAllCoroutines();
		yield break;
	}


	void OnGUI()
	{
		GUI.skin = skin;
		GUI.color = Color.red;
		GUI.DrawTexture(new Rect(0-Screen.width/35, 0, Screen.width/6, Screen.height/12), LeftBar);
		timer += Time.deltaTime;
		float tim = Mathf.RoundToInt(timer);
		float min = tim/60;
		float _sec = tim%60;
		string minSec = string.Format("{0:0}:{1:00}", Mathf.Floor(min), _sec);

		GUI.DrawTexture(new Rect(0-Screen.width/35, Screen.height/12, Screen.width/6, Screen.height/12), LeftBar);
		GUI.DrawTexture(new Rect(0-Screen.width/35, Screen.height - Screen.height/10+Screen.height/45, Screen.width/3, Screen.height/10), LeftBar);
		GUI.DrawTexture(new Rect(0+Screen.width/85, Screen.height - Screen.height/15, Screen.width/24, Screen.height/16), Hearth); 

		GUI.color = Color.white;
		GUI.skin.label.fontSize = Screen.height/20;
		GUI.Label(new Rect(0+Screen.width/25, Screen.height/45, Screen.width/5, Screen.height/4), minSec.ToString());
		GUI.Label(new Rect(0+Screen.width/22, Screen.height/10, Screen.width/5, Screen.height/4), kills.ToString());
		GUI.DrawTexture(new Rect(0,  Screen.height/94, Screen.width/28, Screen.height/16), Timer);
		GUI.DrawTexture(new Rect(0,  Screen.height/11, Screen.width/28, Screen.height/16), Kills);
		GUI.color = Color.red;

		GUI.color = Color.white;
		GUI.DrawTexture(new Rect(0+Screen.width/8, Screen.height - Screen.height/15, Screen.width/24, Screen.height/16), Armor);
	    if(_Hearth <= 30)
			GUI.color = Color.red;
		else
			GUI.color = Color.white;
		GUI.skin.label.fontSize = Screen.height/17;
		GUI.Label(new Rect(0+Screen.width/17, Screen.height - Screen.height/15, Screen.width/5, Screen.height/16), (Mathf.RoundToInt(_Hearth)).ToString());
		GUI.color = Color.white;
		GUI.Label(new Rect(0+Screen.width/6, Screen.height - Screen.height/15, Screen.width/5, Screen.height/16), (Mathf.RoundToInt(_Armor)).ToString());



	}

}



[System.Serializable]

public class Footsteps
{
	public string TagMeterial;
	public AudioClip[] FootSteepsSound;
}