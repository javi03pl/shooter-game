using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ShopManager : MonoBehaviour {
	#region Guns
	[SerializeField]
	[Header("Guns")]
	public List<Gun> gunsInShop;
	public Gun equippedGun;
	public List<Gun> purchasedGuns;
	public List<int> purchasedGunsIndex;
	public Text costBanner;
	public GameObject noMoneyUI;
	#endregion Guns
	[Header("Textures")]
	#region Textures
		public List<Colors> colorsInShop;
		public List<Colors> purchasedColors;
		public List<int> purchasedColorsIndex;
		public Colors equippedColor;
		public GameObject noMoneyUITexture;
	#endregion Textures
	[Header("Bullets")]
	#region bullets
		public List<Bullets> bulletsInShop;
		public List<Bullets> purchasedBullets;
		public List<int> purchasedBulletsIndex;
		public Bullets equippedBullet;
		public GameObject noMoneyUIBullet;
	#endregion bullets
	[Header("Tiles")]
	#region TileMaterials
		public List<TileMaterials> tileMaterialsInShop;
		public List<TileMaterials> purchasedTileMaterials;
		public List<int> purchasedTileMaterialsIndex;
		public TileMaterials equippedTileMaterial;
		public GameObject noMoneyUITileMaterial;
	#endregion TileMaterials
	#region Skills
		public List<Skills> skillsInShop;
		public List<Skills> purchasedSkills;
		public List<int> purchasedSkillsIndex;
		public Skills equippedSkill;
		public GameObject noMoneyUISkills;
	#endregion Skills
	#region singleton
	public static ShopManager instance;
	void Awake()
	{
		if(instance!= null)
		{
			Debug.LogError("More than one instance of the shop manager");
			return;
		}else{
			instance = this;
		}
		
		foreach(Gun gun in ShopManager.instance.gunsInShop)
		{
			if(gun.isPurchased)
			{
				ShopManager.instance.purchasedGuns.Add(gun);
			}
		}

		foreach(Colors color in ShopManager.instance.colorsInShop)
		{
			if(color.isPurchased)
			{
				ShopManager.instance.purchasedColors.Add(color);
			}
		}

		foreach(TileMaterials tile in ShopManager.instance.tileMaterialsInShop)
		{
			if(tile.isPurchased)
			{
				ShopManager.instance.purchasedTileMaterials.Add(tile);
			}
		}

		foreach(Bullets bullet in ShopManager.instance.bulletsInShop)
		{
			if(bullet.isPurchased)
			{
				ShopManager.instance.purchasedBullets.Add(bullet);
			}
		}

		foreach(Skills skill in ShopManager.instance.skillsInShop)
		{
			if(skill.isEquipped)
			{
				ShopManager.instance.purchasedSkills.Add(skill);
			}
		}

		LoadPurchasedGunsIndex();
		LoadPurchasedColorsIndex();
		LoadPurchasedBulletsIndex();
		LoadPurchasedTilesIndex();
		LoadPurchasedSkillsIndex();
	}
	#endregion singleton

	void Start()
	{	
		EquipGun(PlayerPrefs.GetInt("equipped gun index"));
		EquipColor(PlayerPrefs.GetInt("equipped color index"));
		EquipTileMaterial(PlayerPrefs.GetInt("equipped tilematerial index"));
		EquipBullet(PlayerPrefs.GetInt("equipped bullet index"));
		EquipSkill(PlayerPrefs.GetInt("equipped skill index"));

		costBanner.gameObject.SetActive(false);
	}
	IEnumerator ShowCostForGuns(int gunIndex)
	{
		costBanner.gameObject.SetActive(true);
		costBanner.text = "-$"+gunsInShop[gunIndex].cost.ToString();
		yield return new WaitForSeconds(1.5f);
		costBanner.gameObject.SetActive(false);
	}

	IEnumerator ShowCostForColors(int colorIndex)
	{
		costBanner.gameObject.SetActive(true);
		costBanner.text = "-$"+colorsInShop[colorIndex].cost.ToString();
		yield return new WaitForSeconds(1.5f);
		costBanner.gameObject.SetActive(false);
	}
	
	public void DisplayCostForGuns(int gunIndex)
	{
		StartCoroutine(ShowCostForGuns(gunIndex));
	}

	public void DisplayCostForColors(int colorIndex)
	{
		StartCoroutine(ShowCostForColors(colorIndex));
	}
	#region Guns
	public void BuyGun(int gunIndex)
	{
		if(PlayerPrefs.GetInt("level")>= gunsInShop[gunIndex].levelToUnlock)
		{
				if(!gunsInShop[gunIndex].isPurchased)
			{
					if(PlayerPrefs.GetInt("money") >= gunsInShop[gunIndex].cost)
				{
					purchasedGuns.Add(gunsInShop[gunIndex]);
					PlayerPrefs.SetInt("money" , PlayerPrefs.GetInt("money") - gunsInShop[gunIndex].cost);
					gunsInShop[gunIndex].isPurchased = true;
					purchasedGunsIndex.Add(gunIndex);
					DisplayCostForGuns(gunIndex);
					SavePurchasedGunsIndex();
					Debug.Log(gunsInShop[gunIndex].name+" purchased");
					AudioManager.instance.PlaySound2D("Buy");
				}
				else if(PlayerPrefs.GetInt("money") < gunsInShop[gunIndex].cost)
				{
					ShowNotEnoughMoneyPanel();
				}
			}
		}
	}

	public void EquipGun(int gunIndex)
	{
		if(equippedGun == null)
		{
			equippedGun = gunsInShop[gunIndex];
			PlayerPrefs.SetInt("equipped gun index" , gunIndex);
			Debug.Log("first gun"+"("+gunsInShop[gunIndex].name+")");
		}else if(equippedGun != null)
		{
			if(equippedGun.name == gunsInShop[gunIndex].name)					//if the gun is already equipped
			{
				Debug.Log("gun already equipped");
			}else{
				equippedGun = gunsInShop[gunIndex];
				PlayerPrefs.SetInt("equipped gun index" , gunIndex);
				Debug.Log(gunsInShop[gunIndex].name + " Equipped");
			}
		}
	}

	public void ShowNotEnoughMoneyPanel()
	{
		noMoneyUI.SetActive(true);
	}

	public void HideNotEnoughMoneyPanel()
	{
		noMoneyUI.SetActive(false);
	}
	#endregion Guns
	
	#region Textures

		public void BuyColor(int colorIndex)
		{
			if(PlayerPrefs.GetInt("level")>=colorsInShop[colorIndex].levelToUnlock)
			{
				if(!colorsInShop[colorIndex].isPurchased)
				{
					if(PlayerPrefs.GetInt("money") >= colorsInShop[colorIndex].cost)
					{
						purchasedColors.Add(colorsInShop[colorIndex]);
						PlayerPrefs.SetInt("money" , PlayerPrefs.GetInt("money") - colorsInShop[colorIndex].cost);
						colorsInShop[colorIndex].isPurchased = true;
						purchasedColorsIndex.Add(colorIndex);
						SavePurchasedColorsIndex();
						DisplayCostForColors(colorIndex);
						Debug.Log(colorsInShop[colorIndex].name+" purchased");
						AudioManager.instance.PlaySound2D("Buy");
					}
					else if(PlayerPrefs.GetInt("money") < colorsInShop[colorIndex].cost)
					{
						ShowNotEnoughMoneyPanelForTextures();
					}
				}
			} 
			
		}
		public void EquipColor(int colorIndex)
		{
			if(equippedColor == null)
		{
			equippedColor = colorsInShop[colorIndex];
			PlayerPrefs.SetInt("equipped color index" , colorIndex);
			Debug.Log("first color "+"("+colorsInShop[colorIndex].colorName+") equipped");
		}else if(equippedColor != null)
		{
			if(equippedColor == colorsInShop[colorIndex])					//if the color is already equipped
			{
				Debug.Log("color already equipped");
			}else{
				equippedColor = colorsInShop[colorIndex];
				PlayerPrefs.SetInt("equipped color index" , colorIndex);
				Debug.Log(colorsInShop[colorIndex] + " Equipped");
			}
		}
		}


		public void ShowNotEnoughMoneyPanelForTextures()
	{
		noMoneyUITexture.SetActive(true);
	}

	public void HideNotEnoughMoneyPanelForTextures()
	{
		noMoneyUITexture.SetActive(false);
	}
	#endregion Textures

	#region bullets
		public void BuyBullet(int bulletIndex)
		{
			if(PlayerPrefs.GetInt("level")>=bulletsInShop[bulletIndex].levelToUnlock)
			{
				if(!bulletsInShop[bulletIndex].isPurchased)
				{
					if(PlayerPrefs.GetInt("money") >= bulletsInShop[bulletIndex].cost)
					{
						purchasedBullets.Add(bulletsInShop[bulletIndex]);
						PlayerPrefs.SetInt("money" , PlayerPrefs.GetInt("money") - bulletsInShop[bulletIndex].cost);
						bulletsInShop[bulletIndex].isPurchased = true;
						purchasedBulletsIndex.Add(bulletIndex);
						SavePurchasedBulletsIndex();
						DisplayCostForColors(bulletIndex);
						Debug.Log(bulletsInShop[bulletIndex].name+" purchased");
						AudioManager.instance.PlaySound2D("Buy");
					}
					else if(PlayerPrefs.GetInt("money") < bulletsInShop[bulletIndex].cost)
					{
						ShowNotEnoughMoneyPanelForTextures();
					}
				}
			}
			
		}
		public void EquipBullet(int bulletIndex)
		{
			if(equippedBullet == null)
		{
			equippedBullet = bulletsInShop[bulletIndex];
			PlayerPrefs.SetInt("equipped bullet index" , bulletIndex);
			Debug.Log("first bullet "+"("+bulletsInShop[bulletIndex].bulletName+") equipped");
		}else if(equippedBullet != null)
		{
			if(equippedBullet == bulletsInShop[bulletIndex])					//if the color is already equipped
			{
				Debug.Log("bullet already equipped");
			}else{
				equippedBullet = bulletsInShop[bulletIndex];
				PlayerPrefs.SetInt("equipped bullet index" , bulletIndex);
				Debug.Log(bulletsInShop[bulletIndex] + " Equipped");
			}
		}
		}


		// 	public void ShowNotEnoughMoneyPanelForTextures()
		// {
		// 	noMoneyUITexture.SetActive(true);
		// }

		// public void HideNotEnoughMoneyPanelForTextures()
		// {
		// 	noMoneyUITexture.SetActive(false);
		// }

	#endregion bullets

	#region TileMaterials
		public void BuyTileMaterial(int tileMaterialIndex)
		{
			if(PlayerPrefs.GetInt("level")>=tileMaterialsInShop[tileMaterialIndex].levelToUnlock)
			{
				if(!tileMaterialsInShop[tileMaterialIndex].isPurchased)
				{
					if(PlayerPrefs.GetInt("money") >= tileMaterialsInShop[tileMaterialIndex].cost)
					{
						purchasedTileMaterials.Add(tileMaterialsInShop[tileMaterialIndex]);
						PlayerPrefs.SetInt("money" , PlayerPrefs.GetInt("money") - tileMaterialsInShop[tileMaterialIndex].cost);
						tileMaterialsInShop[tileMaterialIndex].isPurchased = true;
						purchasedTileMaterialsIndex.Add(tileMaterialIndex);
						SavePurchasedTilesIndex();
						DisplayCostForColors(tileMaterialIndex);
						Debug.Log(tileMaterialsInShop[tileMaterialIndex].name+" purchased");
						AudioManager.instance.PlaySound2D("Buy");
					}
					else if(PlayerPrefs.GetInt("money") < tileMaterialsInShop[tileMaterialIndex].cost)
					{
						ShowNotEnoughMoneyPanelForTextures();
					}
				}
			}
			
		}
		public void EquipTileMaterial(int tileMaterialIndex)
		{
			if(equippedTileMaterial == null)
		{
			equippedTileMaterial = tileMaterialsInShop[tileMaterialIndex];
			PlayerPrefs.SetInt("equipped tilematerial index" , tileMaterialIndex);
			Debug.Log("first tile material "+"("+tileMaterialsInShop[tileMaterialIndex].colorName+") equipped");
		}else if(equippedTileMaterial != null)
		{
			if(equippedTileMaterial == tileMaterialsInShop[tileMaterialIndex])					//if the color is already equipped
			{
				Debug.Log("tile material already equipped");
			}else{
				equippedTileMaterial = tileMaterialsInShop[tileMaterialIndex];
				PlayerPrefs.SetInt("equipped tilematerial index" , tileMaterialIndex);
				Debug.Log(tileMaterialsInShop[tileMaterialIndex] + " Equipped");
			}
		}
		}
	#endregion TileMaterials
	
	#region Skills
		public void BuySkill(int skillIndex)
		{
			if(PlayerPrefs.GetInt("level")>=skillsInShop[skillIndex].levelToUnlock)
			{
				if(!skillsInShop[skillIndex].isPurchased)
				{
					if(PlayerPrefs.GetInt("money") >= skillsInShop[skillIndex].cost)
					{
						purchasedSkills.Add(skillsInShop[skillIndex]);
						PlayerPrefs.SetInt("money" , PlayerPrefs.GetInt("money") - skillsInShop[skillIndex].cost);
						skillsInShop[skillIndex].isPurchased = true;
						purchasedSkillsIndex.Add(skillIndex);
						SavePurchasedSkillsIndex();
						DisplayCostForColors(skillIndex);
						Debug.Log(skillsInShop[skillIndex].skillName+" purchased");
						AudioManager.instance.PlaySound2D("Buy");
					}
					else if(PlayerPrefs.GetInt("money") < skillsInShop[skillIndex].cost)
					{
						ShowNotEnoughMoneyPanelForSkills();
					}
				}				
			}

		}
		public void EquipSkill(int skillIndex)
		{
			if(equippedSkill == null)
		{
			equippedSkill = skillsInShop[skillIndex];
			PlayerPrefs.SetInt("equipped skill index" , skillIndex);
			Debug.Log("first skill "+"("+skillsInShop[skillIndex].skillName+") equipped");
		}else if(equippedSkill != null)
		{
			if(equippedSkill == skillsInShop[skillIndex])					//if the color is already equipped
			{
				Debug.Log("skill already equipped");
			}else{
				equippedSkill = skillsInShop[skillIndex];
				PlayerPrefs.SetInt("equipped skill index" , skillIndex);
				Debug.Log(skillsInShop[skillIndex] + " Equipped");
			}
		}
		}

		public void ShowNotEnoughMoneyPanelForSkills()
		{
			noMoneyUISkills.SetActive(true);
		}

		public void HideNotEnoughMoneyPanelForskills()
		{
			noMoneyUISkills.SetActive(false);
		}
	#endregion Skills

	#region SaveLoadManager
	public void SavePurchasedGunsIndex()
	{
		SaveLoadManager.SavePurchasedGuns();
	}

	public void LoadPurchasedGunsIndex()
	{
		SaveLoadManager.LoadPurchasedGuns();
	}

	public void ResetGunShop()
	{
		SaveLoadManager.ResetGunShop();
	}

	public void SavePurchasedColorsIndex()
	{
		SaveLoadManager.SavePurchasedColors();
	}

	public void LoadPurchasedColorsIndex()
	{
		SaveLoadManager.LoadPurchasedColors();
	}

	public void ResetColorShop()
	{
		SaveLoadManager.ResetColorShop();
	}

	public void SavePurchasedBulletsIndex()
	{
		SaveLoadManager.SavePurchasedBullets();
	}

	public void LoadPurchasedBulletsIndex()
	{
		SaveLoadManager.LoadPurchasedBullets();
	}

	public void ResetBulletsShop()
	{
		SaveLoadManager.ResetBulletShop();
	}

	public void SavePurchasedTilesIndex()
	{
		SaveLoadManager.SavePurchasedTileMaterials();
	}

	public void LoadPurchasedTilesIndex()
	{
		SaveLoadManager.LoadPurchasedTileMaterials();
	}

	public void ResetTilesShop()
	{
		SaveLoadManager.ResetTileShop();
	}

	public void SavePurchasedSkillsIndex()
	{
		SaveLoadManager.SavePurchasedSkills();
	}

	public void LoadPurchasedSkillsIndex()
	{
		SaveLoadManager.LoadPurchasedSkills();
	}

	public void ResetSkillsShop()
	{
		SaveLoadManager.ResetSkillShop();
	}
	#endregion SaveLoadManager
}
