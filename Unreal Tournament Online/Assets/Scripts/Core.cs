using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Core : MonoBehaviour {
	public GUISkin GUI_skin;
	public Texture2D LeftBar, RightBar, Ammo;
	[HideInInspector]
	public Texture2D aim;
	[HideInInspector]
	public Color color;
	[HideInInspector]
	public float w, h;
	public NormalWeapons _NormalWeapons;
	[HideInInspector]
	public bool ToRed, ToGreen;
	[HideInInspector]
	public int CurentWeapon;
	[HideInInspector]
	public int Bullets;
	[HideInInspector]
	public GameObject[] wpns;
	public int CountOfWeapons;
	[HideInInspector]
	public Texture2D[] Main,Normal, Active, NoBullet;
	int i;
	[HideInInspector]
	public bool[] noBullet;
	float td = 0.7f;
	int oner;
	bool change;
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		Screen.lockCursor = true;
		wpns = new GameObject[CountOfWeapons];
		Main = new Texture2D[CountOfWeapons];
		Normal = new Texture2D[CountOfWeapons];
		Active = new Texture2D[CountOfWeapons];
		NoBullet = new Texture2D[CountOfWeapons];
		noBullet = new bool[CountOfWeapons];
	}
	
	// Update is called once per frame
	void Update () {

	   if(change)
		{
		wpns[CurentWeapon].GetComponent<Animator>().SetBool("Take", true);
		wpns[CurentWeapon].GetComponent<Animator>().SetBool("Idle", false);
		td-=Time.deltaTime; 
		if(td <= 0)
		{
		wpns[CurentWeapon].SetActive(false);
		wpns[oner].SetActive(true);
		//wpns[oner].GetComponent<Animator>().SetBool("Give", true);
		change = false;
		}
		}
			if(Input.GetKeyDown("1") && wpns[0] != null && CurentWeapon != 0)
		    {
			td = 0.1f;
				oner = 0;
				change = true;
			}
		if(Input.GetKeyDown("2") && wpns[1] != null && CurentWeapon != 1)
		{
			td = 0.1f;
			oner = 1;
			change = true;
		}
		if(Input.GetKeyDown("3") && wpns[2] != null && CurentWeapon != 2)
		{
			td = 0.1f;
		oner = 2;
		change = true;
		}

		if(Input.GetKeyDown("4") && wpns[3] != null && CurentWeapon != 3)
		{
			td = 0.1f;
			oner = 3;
			change = true;
		}

		if(i < wpns.Length)
		{
			if(!noBullet[i])
			{
				if(CurentWeapon == i)
					Main[i] = Active[i];
				else
					Main[i] = Normal[i];
			}
			else
				Main[i] = NoBullet[i];
			i++;
		}
		else
			i = 0;
		
	}

	void OnGUI()
	{
		GUI.skin = GUI_skin;
		if(ToRed)
		GUI.color = Color.red;
		else
			if(ToGreen)
				GUI.color = Color.red;
		else
	    GUI.color = Color.red;
		GUI.DrawTexture(new Rect(Screen.width/2-w/2,Screen.height/2-h/2,w,h), aim);
		GUI.color = Color.white;


		CurentWeapon = GUI.SelectionGrid(new Rect(Screen.width/2 - Screen.width/6, Screen.height - Screen.height/10, Screen.width/3, Screen.height/12), CurentWeapon, Main ,Main.Length);
		GUI.color = Color.red;
		GUI.DrawTexture(new Rect(Screen.width-Screen.width/5+Screen.width/56, Screen.height - Screen.height/10+Screen.height/45, Screen.width/5, Screen.height/10), LeftBar);   
		GUI.color = Color.white;
		GUI.skin.label.fontSize = Screen.height/17;
		GUI.DrawTexture(new Rect(Screen.width-Screen.width/8, Screen.height - Screen.height/14, Screen.width/22, Screen.height/14), Ammo);   
		GUI.Label(new Rect(Screen.width-Screen.width/14, Screen.height - Screen.height/16, Screen.width/5, Screen.height/10), Bullets.ToString());   
	}
}

[System.Serializable]
public class NormalWeapons
{
	public string BrickTag, MetalTag, WoodTag, EnemyTag, FreindTag;
	public AudioClip[] BrickSound, MetalSound, WoodSound, EnemySound;
	public GameObject[] BrickImpact, MetalImpact, WoodImpact, EnemyImpact;
	public float volume, MaxDistance;
}

