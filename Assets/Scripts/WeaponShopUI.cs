using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WeaponShopUI : MonoBehaviour {

	[Header("Front")]
	public Text price;
	public Text gunName;
	public Image gunImage;
	public Button buyButton;
	public Button equipButton;
	public Button equippedButton;
	public int gunNumber;
	public GameObject lockedPanel;
	public Text lockedText;

	[Header("Back")]
	public GameObject infoPanel;
	public Text muzzleVelocityText;
	public Text magazineCapacity;
	public Text reloadTime;

	#region singleton
	public static WeaponShopUI instance;
	void Awake()
	{
		if(instance!= null)
		{
			return;
		}else{
			instance = this;
		}
	}
	#endregion singleton
	
	void Start()
	{
		ResetInfoPanels();
	}
	void Update()
	{
		UpdateUI(gunNumber);
	}
	public void UpdateUI(int weaponNumber)
	{
		gunName.text = ShopManager.instance.gunsInShop[weaponNumber-1].gunName;
		gunImage.sprite = ShopManager.instance.gunsInShop[weaponNumber-1].gunImage;
		
		if(ShopManager.instance.gunsInShop[weaponNumber-1].levelToUnlock > PlayerPrefs.GetInt("level"))
		{
			lockedPanel.SetActive(true);
			lockedText.text = "UNLOCKED AT LEVEL "+ShopManager.instance.gunsInShop[weaponNumber-1].levelToUnlock;
		}
		else{
			if(ShopManager.instance.gunsInShop[weaponNumber-1].levelToUnlock <= PlayerPrefs.GetInt("level"))
			{
				lockedPanel.SetActive(false);
				if(ShopManager.instance.gunsInShop[weaponNumber-1].isPurchased)
				{
					equippedButton.gameObject.SetActive(false);
					buyButton.gameObject.SetActive(false);
					equipButton.gameObject.SetActive(true);
					price.text ="";
				}

				if(!ShopManager.instance.gunsInShop[weaponNumber-1].isPurchased){
					equippedButton.gameObject.SetActive(false);
					equipButton.gameObject.SetActive(false);
					buyButton.gameObject.SetActive(true);
					price.text = "$"+ShopManager.instance.gunsInShop[weaponNumber-1].cost;
				}

				if(ShopManager.instance.equippedGun == null)
				{
					return;
				}

				if(ShopManager.instance.gunsInShop[weaponNumber-1].isEquipped || ShopManager.instance.equippedGun.name == ShopManager.instance.gunsInShop[weaponNumber-1].name || weaponNumber-1 == PlayerPrefs.GetInt("equipped gun index"))
				{
					equipButton.gameObject.SetActive(false);
					buyButton.gameObject.SetActive(false);
					equippedButton.gameObject.SetActive(true);
					price.text = "";
				}				
			}

		}

		
	}

	void ResetInfoPanels()
	{
		infoPanel.SetActive(false);
	}

	public void ShowInfo()
	{
		GetInfo(gunNumber);
	}

	public void GetInfo(int weaponNumber)
	{
		infoPanel.SetActive(true);
		muzzleVelocityText.text = ShopManager.instance.gunsInShop[weaponNumber-1].muzzleVelocity.ToString();
		magazineCapacity.text = ShopManager.instance.gunsInShop[weaponNumber-1].projectilesPerMag.ToString();
		reloadTime.text =  ShopManager.instance.gunsInShop[weaponNumber-1].reloadTime.ToString();
	}

	public void HideInfo()
	{
		infoPanel.SetActive(false);
	}
}
