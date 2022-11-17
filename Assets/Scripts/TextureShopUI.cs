using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextureShopUI : MonoBehaviour {
	public int colorNumber;
	public Text colorNameText;
	public Text price;
	public Image colorImage;
	public Button equipButton;
	public Button buyButton;
	public Button equippedButton;
	public GameObject lockedPanel;
	public Text lockedText;


	void Update()
	{
		UpdateUI(colorNumber);
	}
	public void UpdateUI(int colorNumber)
	{
		colorNameText.text = ShopManager.instance.colorsInShop[colorNumber-1].colorName;

		colorImage.sprite = ShopManager.instance.colorsInShop[colorNumber-1].colorImage;
		

		if(ShopManager.instance.colorsInShop[colorNumber-1].levelToUnlock > PlayerPrefs.GetInt("level"))
		{
			lockedPanel.SetActive(true);
			lockedText.text = "UNLOCKED AT LEVEL "+ShopManager.instance.colorsInShop[colorNumber-1].levelToUnlock;
		}
		else{
			if(ShopManager.instance.colorsInShop[colorNumber-1].levelToUnlock <= PlayerPrefs.GetInt("level"))
			{
				lockedPanel.SetActive(false);
		if(ShopManager.instance.colorsInShop[colorNumber-1].isPurchased)
		{
			equippedButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(false);
			equipButton.gameObject.SetActive(true);
			price.text ="";
		}

		if(!ShopManager.instance.colorsInShop[colorNumber-1].isPurchased){
			equippedButton.gameObject.SetActive(false);
			equipButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(true);
			price.text = "$"+ShopManager.instance.colorsInShop[colorNumber-1].cost;
		}

		if(ShopManager.instance.equippedColor == null)
		{
			return;
		}

		if(ShopManager.instance.colorsInShop[colorNumber-1].isEquipped || ShopManager.instance.equippedColor.colorName == ShopManager.instance.colorsInShop[colorNumber-1].colorName)
		{
			equipButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(false);
			equippedButton.gameObject.SetActive(true);
			price.text = "";
		}

	}
	}
	}
}
