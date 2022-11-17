using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (GunController))]

public class Player : LivingEntity {

	public float moveSpeed = 5;
	public float lookSensitivity = 10f;
	public static bool isFps;
	[SerializeField]
	public Camera mainCam;
	[SerializeField]
	public Camera fpsCam;
	[SerializeField]
	Camera crosshairsCam;
	public Crosshairs crosshairs;
	public List<Colors> allColors;
	public List<Colors> purchasedColors;
	public List<Colors> unlockedColors;

	Camera viewCamera;
	PlayerController controller;
	GunController gunController;
	Material playerMat;

	
	protected override void Start () {
		base.Start ();
	}

	void Awake() {
		playerMat = GetComponent<Renderer>().material;
		controller = GetComponent<PlayerController> ();
		gunController = GetComponent<GunController>();
		viewCamera = Camera.main;
		FindObjectOfType<Spawner> ().OnNewWave += OnNewWave;

		foreach(Colors color in ShopManager.instance.colorsInShop)
		{ 
			allColors.Add(color);
		}

		playerMat.color = allColors[PlayerPrefs.GetInt("equipped color index")].color;
		playerMat.mainTexture = allColors[PlayerPrefs.GetInt("equipped color index")].material.mainTexture;
	}

	void OnNewWave(int waveNumber) {
		health = startingHealth;
		//gunController.EquipGun(waveNumber - 1);
		gunController.EquipGunFromShopManager();

		// if(gunController.equippedGun == null)
		// {
		// 	Debug.Log("no equipped gun");
		// 	gunController.EquipGun(gunController.allGuns[0]);
		// }
		Time.timeScale = 1f;
	}

	void Update () {
		// Movement input
		Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;
		controller.Move (moveVelocity);
		
		// Look input
		Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.up * gunController.GunHeight);
		float rayDistance;

		if (groundPlane.Raycast(ray,out rayDistance)) {
			Vector3 point = ray.GetPoint(rayDistance);
			//Debug.DrawLine(ray.origin,point,Color.red);
			if(!isFps)
			{
				controller.LookAt(point);
			}
			crosshairs.transform.position = point;
			crosshairs.DetectTargets(ray);
			if ((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 1) {
				if(!isFps)
				{
					gunController.Aim(point);	
				}
							
			}
		}


		// Weapon input
		if (Input.GetMouseButton(0)) {
			gunController.OnTriggerHold();
		}
		if (Input.GetMouseButtonUp(0)) {
			gunController.OnTriggerRelease();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			gunController.Reload();
		}

		if (transform.position.y < -10) {
			TakeDamage(health);
			if(isFps)
			{
				mainCam.enabled = true;
				fpsCam.enabled = false;
			}
		}

		#region fpsController
			//Fps Controller
			if(Input.GetKeyDown(KeyCode.E))
			{
				isFps = (isFps)? false:true;
				if(isFps)
				{
					PlayerPrefs.SetInt("fps",1);
				}else{
					if(!isFps)
					{
						PlayerPrefs.SetInt("fps",0);
					}
				}
			}

			if(isFps)
			{
				PlayerPrefs.SetInt("fps",1);
				mainCam.enabled = false;
				crosshairsCam.enabled = false;
				fpsCam.enabled = true;
				//fpsCrosshairs.SetActive(true);

				//horizontal rotation
				float _yRot = Input.GetAxisRaw("Mouse X");
				Vector3 _rotation = new Vector3(0f,_yRot,0f)*lookSensitivity;
				controller.Rotate(_rotation);

				//camera rotation
				// float _xRot = Input.GetAxisRaw("Mouse Y");
				// Vector3 _cameraRotation = new Vector3(_xRot,0f,0f)*lookSensitivity;
				// controller.RotateCamera(_cameraRotation);

				//player movement
				float _xMov = Input.GetAxisRaw("Horizontal");
				float _zMov = Input.GetAxisRaw("Vertical");
				Vector3 _movHorizontal = transform.right * _xMov;
				Vector3 _movVertical = transform.forward * _zMov;
				Vector3 _velocity = (_movHorizontal + _movVertical).normalized * moveSpeed;	
				controller.Move(_velocity);
			}else{
				if(!isFps)
				{
					PlayerPrefs.SetInt("fps",0);
					fpsCam.enabled = false;
					crosshairsCam.enabled = true;
					mainCam.enabled = true;
				}
			}
		#endregion
		if(health<=0)
		{
			mainCam.enabled = true;
		}
		#region lowlifeUI
		// if((health*100)/startingHealth <= (startingHealth)/Spawner.currentWave.hitsToKillPlayer)
		// {
		// 	viewCamera.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 15;
		// }
		// else{
		// 	viewCamera.GetComponent<VignetteAndChromaticAberration>().chromaticAberration = 0;
		// }
		//float percentageofdamage = (health*100)/startingHealth;
		#endregion lowlifeUI
	}


	public override void Die ()
	{
		if(isFps)
		{
			fpsCam.enabled = false;
			mainCam.enabled = true;
		}
		
		AudioManager.instance.PlaySound ("Player Death", transform.position);
		base.Die ();
	}
}
