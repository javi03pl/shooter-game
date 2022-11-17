using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {

	public void BuyGun(int gunNumber)
	{
		ShopManager.instance.BuyGun(gunNumber - 1);
	}

	public void EquipGun(int gunNumber)
	{
		ShopManager.instance.EquipGun(gunNumber - 1);
	}

	public void BuyColor(int textureNumber)
	{
		ShopManager.instance.BuyColor(textureNumber-1);
	}

	public void EquipColor(int textureNumber)
	{
		ShopManager.instance.EquipColor(textureNumber-1);
		
	}

	public void BuyBullet(int bulletNumber)
	{
		ShopManager.instance.BuyBullet(bulletNumber-1);
	}

	public void EquipBullet(int bulletNumber)
	{
		ShopManager.instance.EquipBullet(bulletNumber-1);
	}

	public void BuyTileMaterial(int tileNumber)
	{
		ShopManager.instance.BuyTileMaterial(tileNumber-1);
	}

	public void EquipTileMaterial(int tileNumber)
	{
		ShopManager.instance.EquipTileMaterial(tileNumber-1);
	}

	public void BuySkill(int skillNumber)
	{
		ShopManager.instance.BuySkill(skillNumber-1);
	}

	public void EquipSkill(int skillNumber)
	{
		ShopManager.instance.EquipSkill(skillNumber-1);
	}

}
