using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileMaterialsShopUI : MonoBehaviour {

	public int TileNumber;
	public Text tileNameText;
	public Text price;
	public Image tileImage;
	public Button equipButton;
	public Button buyButton;
	public Button equippedButton;
	public GameObject lockedPanel;
	public Text lockedText;

	void Update()
	{
		UpdateUI(TileNumber);
	}
	public void UpdateUI(int tileNumber)
	{
		tileNameText.text = ShopManager.instance.tileMaterialsInShop[tileNumber-1].colorName;

		tileImage.sprite = ShopManager.instance.tileMaterialsInShop[tileNumber-1].colorImage;

		if(ShopManager.instance.tileMaterialsInShop[tileNumber-1].levelToUnlock > PlayerPrefs.GetInt("level"))
		{
			lockedPanel.SetActive(true);
			lockedText.text = "UNLOCKED AT LEVEL "+ShopManager.instance.tileMaterialsInShop[TileNumber-1].levelToUnlock;
		}
		else{
			if(ShopManager.instance.tileMaterialsInShop[tileNumber-1].levelToUnlock <= PlayerPrefs.GetInt("level"))
			{
				lockedPanel.SetActive(false);
		if(ShopManager.instance.tileMaterialsInShop[tileNumber-1].isPurchased)
		{
			equippedButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(false);
			equipButton.gameObject.SetActive(true);
			price.text ="";
		}

		if(!ShopManager.instance.tileMaterialsInShop[tileNumber-1].isPurchased){
			equippedButton.gameObject.SetActive(false);
			equipButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(true);
			price.text = "$"+ShopManager.instance.tileMaterialsInShop[tileNumber-1].cost;
		}

		if(ShopManager.instance.equippedTileMaterial == null)
		{
			return;
		}

		if(ShopManager.instance.tileMaterialsInShop[tileNumber-1].isEquipped || ShopManager.instance.equippedTileMaterial.colorName == ShopManager.instance.tileMaterialsInShop[tileNumber-1].colorName)
		{
			equipButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(false);
			equippedButton.gameObject.SetActive(true);
			price.text = "";
		}
		//fixing bug of white tile
		if(tileNumber == 1 && PlayerPrefs.GetInt("equipped tilematerial index") != 0)
		{
			equipButton.gameObject.SetActive(true);
			equippedButton.gameObject.SetActive(false);
		}
	}
	}
	}
}
