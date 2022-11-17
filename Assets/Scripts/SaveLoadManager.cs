using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager {
	#region GunSerialization
	public static void SavePurchasedGuns()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedguns.dat",FileMode.Create);

		GunIndexer gunindexer = new GunIndexer();
		
		for (int i = 0; i < ShopManager.instance.purchasedGunsIndex.Count ; i++)
		{
			gunindexer.purchasedGunIndexes.Add(ShopManager.instance.purchasedGunsIndex[i]);
		}

		//Debug.Log(gunindexer.purchasedGunIndexes);

		bf.Serialize(stream , gunindexer);
		stream.Close();
	}

	public static void LoadPurchasedGuns()
	{
		
		if(File.Exists(Application.persistentDataPath + "/purchasedguns.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedguns.dat",FileMode.Open);
			GunIndexer gunindexer = (GunIndexer)bf.Deserialize(stream);
			stream.Close();

			foreach(Gun gun in ShopManager.instance.gunsInShop)
			{
				for (int i = 0; i < gunindexer.purchasedGunIndexes.Count; i++)
				{
					ShopManager.instance.gunsInShop[gunindexer.purchasedGunIndexes[i]].isPurchased = true;
					//Debug.Log("loaded" + gunindexer.purchasedGunIndexes.Count + "from 1");
				}
			}
			//Debug.Log("loaded" + gunindexer.purchasedGunIndexes.Count + "from 2");
			
			
		}else{
			Debug.LogError("File doesnt`t exist");
			return;
		}
	}

	public static void ResetGunShop()
	{
		GunIndexer gunindexer = new GunIndexer();
		
		foreach(Gun gun in ShopManager.instance.gunsInShop)
		{
			if(gun.isPurchased)
			{
				gun.isPurchased = false;
			}
			
		}

		ShopManager.instance.purchasedGunsIndex.Clear();
		gunindexer.purchasedGunIndexes.Clear();

		ShopManager.instance.purchasedGunsIndex.Add(0);
		PlayerPrefs.SetInt("equipped gun index" , 0);

		SavePurchasedGuns();
		LoadPurchasedGuns();
	}
	#endregion GunSerialization
	
	#region ColorSerialization
	public static void SavePurchasedColors()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedcolors.dat",FileMode.Create);

		ColorIndexer colorindexer = new ColorIndexer();
		
		for (int i = 0; i < ShopManager.instance.purchasedColorsIndex.Count ; i++) //if it doesnt work , try setting it to purchasedgunsindex
		{
			colorindexer.purchasedColorsIndexes.Add(ShopManager.instance.purchasedColorsIndex[i]);
		}

		//Debug.Log(gunindexer.purchasedGunIndexes);

		bf.Serialize(stream , colorindexer);
		stream.Close();
	}

	public static void LoadPurchasedColors()
	{
		
		if(File.Exists(Application.persistentDataPath + "/purchasedcolors.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedcolors.dat",FileMode.Open);
			ColorIndexer colorindexer = (ColorIndexer)bf.Deserialize(stream);
			stream.Close();

			foreach(Colors color in ShopManager.instance.colorsInShop)
			{
				for (int i = 0; i < colorindexer.purchasedColorsIndexes.Count; i++)
				{
					ShopManager.instance.colorsInShop[colorindexer.purchasedColorsIndexes[i]].isPurchased = true;
					//Debug.Log("loaded" + colorindexer.purchasedColorsIndexes.Count + "from 1");
				}
			}
			//Debug.Log("loaded" + colorindexer.purchasedColorsIndexes.Count + "from 2");
			
			
		}else{
			Debug.LogError("File doesnt`t exist");
			return;
		}
	}

	public static void ResetColorShop()
	{
		ColorIndexer colorindexer = new ColorIndexer();
		
		foreach(Colors color in ShopManager.instance.colorsInShop)
		{
			

			if(color.isPurchased)
			{
				color.isPurchased = false;
			}
			
		}

		ShopManager.instance.purchasedColorsIndex.Clear();
		colorindexer.purchasedColorsIndexes.Clear();

		ShopManager.instance.purchasedColorsIndex.Add(0);
		PlayerPrefs.SetInt("equipped color index" , 0);

		SavePurchasedColors();
		LoadPurchasedColors();
	}
	#endregion ColorSerialization
	#region BulletSerialization
			public static void SavePurchasedBullets()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedbullets.dat",FileMode.Create);

		BulletIndexer bulletindexer = new BulletIndexer();
		
		for (int i = 0; i < ShopManager.instance.purchasedBulletsIndex.Count ; i++)
		{
			bulletindexer.purchasedBulletsIndexes.Add(ShopManager.instance.purchasedBulletsIndex[i]);
		}

		//Debug.Log(gunindexer.purchasedGunIndexes);

		bf.Serialize(stream , bulletindexer);
		stream.Close();
	}

	public static void LoadPurchasedBullets()
	{
		
		if(File.Exists(Application.persistentDataPath + "/purchasedbullets.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedbullets.dat",FileMode.Open);
			BulletIndexer bulletindexer = (BulletIndexer)bf.Deserialize(stream);
			stream.Close();

			foreach(Bullets bullet in ShopManager.instance.bulletsInShop)
			{
				for (int i = 0; i < bulletindexer.purchasedBulletsIndexes.Count; i++)
				{
					ShopManager.instance.bulletsInShop[bulletindexer.purchasedBulletsIndexes[i]].isPurchased = true;
					//Debug.Log("loaded" + colorindexer.purchasedColorsIndexes.Count + "from 1");
				}
			}
			//Debug.Log("loaded" + colorindexer.purchasedColorsIndexes.Count + "from 2");
			
			
		}else{
			Debug.LogError("File doesnt`t exist");
			SavePurchasedBullets();
			return;
		}
	}

	public static void ResetBulletShop()
	{
		BulletIndexer bulletindexer = new BulletIndexer();
		
		foreach(Bullets bullet in ShopManager.instance.bulletsInShop)
		{
			

			if(bullet.isPurchased)
			{
				bullet.isPurchased = false;
			}
			
		}

		ShopManager.instance.purchasedBulletsIndex.Clear();
		bulletindexer.purchasedBulletsIndexes.Clear();

		ShopManager.instance.purchasedBulletsIndex.Add(0);
		PlayerPrefs.SetInt("equipped bullet index" , 0);

		SavePurchasedBullets();
		LoadPurchasedBullets();
	}
	#endregion BulletSerialization
	#region TileMaterialSerialization
			public static void SavePurchasedTileMaterials()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedtilematerials.dat",FileMode.Create);

		TileMaterialIndexer tileindexer = new TileMaterialIndexer();
		
		for (int i = 0; i < ShopManager.instance.purchasedTileMaterialsIndex.Count ; i++)
		{
			tileindexer.purchasedTileMaterialsIndexes.Add(ShopManager.instance.purchasedTileMaterialsIndex[i]);
		}

		//Debug.Log(gunindexer.purchasedGunIndexes);

		bf.Serialize(stream , tileindexer);
		stream.Close();
	}

	public static void LoadPurchasedTileMaterials()
	{
		
		if(File.Exists(Application.persistentDataPath + "/purchasedtilematerials.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedtilematerials.dat",FileMode.Open);
			TileMaterialIndexer tileindexer = (TileMaterialIndexer)bf.Deserialize(stream);
			stream.Close();

			foreach(TileMaterials tile in ShopManager.instance.tileMaterialsInShop)
			{
				for (int i = 0; i < tileindexer.purchasedTileMaterialsIndexes.Count; i++)
				{
					ShopManager.instance.tileMaterialsInShop[tileindexer.purchasedTileMaterialsIndexes[i]].isPurchased = true;
					//Debug.Log("loaded" + colorindexer.purchasedColorsIndexes.Count + "from 1");
				}
			}
			//Debug.Log("loaded" + colorindexer.purchasedColorsIndexes.Count + "from 2");
			
			
		}else{
			Debug.LogError("File doesnt`t exist");
			SavePurchasedTileMaterials();
			return;
		}
	}

	public static void ResetTileShop()
	{
		TileMaterialIndexer tileindexer = new TileMaterialIndexer();
		
		foreach(TileMaterials tile in ShopManager.instance.tileMaterialsInShop)
		{
			if(tile.isPurchased)
			{
				tile.isPurchased = false;
			}
			
		}

		ShopManager.instance.purchasedTileMaterialsIndex.Clear();
		tileindexer.purchasedTileMaterialsIndexes.Clear();

		ShopManager.instance.purchasedTileMaterialsIndex.Add(0);
		PlayerPrefs.SetInt("equipped tilematerial index" , 0);

		SavePurchasedTileMaterials();
		LoadPurchasedTileMaterials();
	}
	#endregion TileMaterialSerialization
	#region SkillSerialization
			public static void SavePurchasedSkills()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedtilematerials.dat",FileMode.Create);

		SkillIndexer skillindexer = new SkillIndexer();
		
		for (int i = 0; i < ShopManager.instance.purchasedSkillsIndex.Count ; i++)
		{
			skillindexer.purchasedSkillsIndexes.Add(ShopManager.instance.purchasedSkillsIndex[i]);
		}

		//Debug.Log(gunindexer.purchasedGunIndexes);

		bf.Serialize(stream , skillindexer);
		stream.Close();
	}

	public static void LoadPurchasedSkills()
	{
		
		if(File.Exists(Application.persistentDataPath + "/purchasedskills.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream stream = new FileStream(Application.persistentDataPath+"/purchasedskills.dat",FileMode.Open);
			SkillIndexer skillindexer = (SkillIndexer)bf.Deserialize(stream);
			stream.Close();

			foreach(Skills skill in ShopManager.instance.skillsInShop)
			{
				for (int i = 0; i < skillindexer.purchasedSkillsIndexes.Count; i++)
				{
					ShopManager.instance.skillsInShop[skillindexer.purchasedSkillsIndexes[i]].isPurchased = true;
					//Debug.Log("loaded" + colorindexer.purchasedColorsIndexes.Count + "from 1");
				}
			}
			//Debug.Log("loaded" + colorindexer.purchasedColorsIndexes.Count + "from 2");
			
			
		}else{
			Debug.LogError("File doesnt`t exist");
			SavePurchasedSkills();
			return;
		}
	}

	public static void ResetSkillShop()
	{
		SkillIndexer skillindexer = new SkillIndexer();
		
		foreach(Skills skill in ShopManager.instance.skillsInShop)
		{
			if(skill.isPurchased)
			{
				skill.isPurchased = false;
			}
			
		}

		ShopManager.instance.purchasedSkillsIndex.Clear();
		skillindexer.purchasedSkillsIndexes.Clear();

		SavePurchasedSkills();
		LoadPurchasedSkills();
	}
	#endregion SkillSerialization
}
 [Serializable]
 public class GunIndexer {
	public List<int> purchasedGunIndexes = new List<int>();

}
[Serializable]
public class ColorIndexer{
	public List<int> purchasedColorsIndexes = new List<int>();
}

[Serializable]
public class BulletIndexer{
	public List<int> purchasedBulletsIndexes = new List<int>();
}

[Serializable]
public class TileMaterialIndexer	{
	public List<int> purchasedTileMaterialsIndexes = new List<int>();
}

[Serializable]
public class SkillIndexer	{
	public List<int> purchasedSkillsIndexes = new List<int>();
}