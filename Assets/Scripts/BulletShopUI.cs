using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletShopUI : MonoBehaviour {

	public int bulletNumber;
	public Text bulletNameText;
	public Text price;
	public Image bulletImage;
	public Button equipButton;
	public Button buyButton;
	public Button equippedButton;
	public GameObject lockedPanel;
	public Text lockedText;


	void Update()
	{
		UpdateUI(bulletNumber);
	}
	public void UpdateUI(int bulletNumber)
	{
		bulletNameText.text = ShopManager.instance.bulletsInShop[bulletNumber-1].bulletName;

		bulletImage.sprite = ShopManager.instance.bulletsInShop[bulletNumber-1].bulletImage;

		if(ShopManager.instance.bulletsInShop[bulletNumber-1].levelToUnlock > PlayerPrefs.GetInt("level"))
		{
			lockedPanel.SetActive(true);
			lockedText.text = "UNLOCKED AT LEVEL "+ShopManager.instance.bulletsInShop[bulletNumber-1].levelToUnlock;
		}
		else{
			if(ShopManager.instance.bulletsInShop[bulletNumber-1].levelToUnlock <= PlayerPrefs.GetInt("level"))
			{
				lockedPanel.SetActive(false);
		if(ShopManager.instance.bulletsInShop[bulletNumber-1].isPurchased)
		{
			equippedButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(false);
			equipButton.gameObject.SetActive(true);
			price.text ="";
		}

		if(!ShopManager.instance.bulletsInShop[bulletNumber-1].isPurchased){
			equippedButton.gameObject.SetActive(false);
			equipButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(true);
			price.text = "$"+ShopManager.instance.bulletsInShop[bulletNumber-1].cost;
		}

		if(ShopManager.instance.equippedBullet == null)
		{
			return;
		}

		if(ShopManager.instance.bulletsInShop[bulletNumber-1].isEquipped || ShopManager.instance.equippedBullet.bulletName == ShopManager.instance.bulletsInShop[bulletNumber-1].bulletName)
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
