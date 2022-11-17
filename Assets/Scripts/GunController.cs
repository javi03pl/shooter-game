using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GunController : MonoBehaviour {

	public Transform weaponHold;
	//public Gun[] allGuns;
	public List<Gun> allGuns;

	public List <Gun> purchasedGuns;

	public List <Gun> unlockedGuns;
	public Gun equippedGun;

	#region singleton
	public static GunController instance;

	void Awake()
	{
		if(instance!= null)
		{
			Debug.LogError("More than one gun controller");
			return;
		}else{
			instance = this;
		}
		
		foreach(Gun gun in ShopManager.instance.gunsInShop)
		{
			allGuns.Add(gun);
		}
	}
	#endregion singleton
	
	public void EquipGun(Gun gunToEquip) {
		if (equippedGun != null) {
			Destroy(equippedGun.gameObject);
		}
		equippedGun = Instantiate (gunToEquip, weaponHold.position,weaponHold.rotation) as Gun;
		equippedGun.transform.parent = weaponHold;
	}


	public void EquipGunFromShopManager()
	{
		EquipGun(allGuns[PlayerPrefs.GetInt("equipped gun index")]);
	}

	public void EquipGun(int weaponIndex) {
		EquipGun (allGuns[weaponIndex]);
	}
	
	public void OnTriggerHold() {
		if (equippedGun != null) {
			equippedGun.OnTriggerHold();
		}
	}

	public void OnTriggerRelease() {
		if (equippedGun != null) {
			equippedGun.OnTriggerRelease();
		}
	}

	public float GunHeight {
		get {
			return weaponHold.position.y;
		}
	}

	public void Aim(Vector3 aimPoint) {
		if (equippedGun != null) {
			equippedGun.Aim(aimPoint);
		}
	}

	public void Reload() {
		if (equippedGun != null) {
			equippedGun.Reload();
		}
	}

}