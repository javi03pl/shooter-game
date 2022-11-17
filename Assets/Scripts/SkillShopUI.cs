using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillShopUI : MonoBehaviour {

	[Header("Front")]
	public Text price;
	public Text skillName;
	public Image skillImage;
	public Button buyButton;
	public Button equipButton;
	public Button equippedButton;
	public int skillNumber;
	public GameObject lockedPanel;
	public Text lockedText;

	[Header("Back")]
	public GameObject infoPanel;
	public Text description;
	
	void Start()
	{
		ResetInfoPanels();
	}
	void Update()
	{
		UpdateUI(skillNumber);
	}
	public void UpdateUI(int skillNumber)
	{
		skillName.text = ShopManager.instance.skillsInShop[skillNumber-1].skillName;
		skillImage.sprite = ShopManager.instance.skillsInShop[skillNumber-1].skillImage;

		if(ShopManager.instance.skillsInShop[skillNumber-1].levelToUnlock > PlayerPrefs.GetInt("level"))
		{
			lockedPanel.SetActive(true);
			lockedText.text = "UNLOCKED AT LEVEL "+ShopManager.instance.skillsInShop[skillNumber-1].levelToUnlock;
		}
		else{
			if(ShopManager.instance.skillsInShop[skillNumber-1].levelToUnlock <= PlayerPrefs.GetInt("level"))
			{
				lockedPanel.SetActive(false);
		if(ShopManager.instance.skillsInShop[skillNumber-1].isPurchased)
		{
			equippedButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(false);
			equipButton.gameObject.SetActive(true);
			price.text ="";
		}

		if(!ShopManager.instance.skillsInShop[skillNumber-1].isPurchased){
			equippedButton.gameObject.SetActive(false);
			equipButton.gameObject.SetActive(false);
			buyButton.gameObject.SetActive(true);
			price.text = "$"+ShopManager.instance.skillsInShop[skillNumber-1].cost;
		}

		if(ShopManager.instance.equippedSkill == null)
		{
			return;
		}

		if(ShopManager.instance.skillsInShop[skillNumber-1].isEquipped || ShopManager.instance.equippedSkill.name == ShopManager.instance.skillsInShop[skillNumber-1].name)
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
		GetInfo(skillNumber);
	}

	public void GetInfo(int skillNumber)
	{
		infoPanel.SetActive(true);
		description.text = ShopManager.instance.skillsInShop[skillNumber-1].description.ToString();
	}

	public void HideInfo()
	{
		infoPanel.SetActive(false);
	}
}
