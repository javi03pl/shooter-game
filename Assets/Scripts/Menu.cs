using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

	[Header("Menus")]
	public GameObject mainMenuHolder;
	public GameObject optionsMenuHolder;
	public GameObject shopMenu;
	public GameObject statsMenuHolder;

	[Header("Options")]
	public GameObject resolutionsPanel;
	public GameObject difficultyPanel;
	public GameObject controlsPanel;
	public Slider[] volumeSliders;
	public Toggle[] resolutionToggles;
	public Toggle fullscreenToggle;
	public int[] screenWidths;
	int activeScreenResIndex;

	[Header("Stats")]

	[Header("Total Stats")]
	public Text EnemiesKilled;
	public Text TotalTimeSurvivedText;
	public Text TotalScore;
	public Text totalShots;
	//public Text accuracyText;
	public Text totalMoneyWonText;

	[Header("High Stats")]
	
	public Text HighScore;
	public Text longestTimeSurvived;
	public Text HighestWaveSurvived;

	[Header("Shop Tabs")]
	public Text weaponShopTitle;
	public Text textureShopTitle;
	public Text skillsShopTitle;
	public Text moneyText;
	public Button gunsTab;
	public Button texturesTab;
	public Button AbilitiesTab;
	public GameObject gunShop;
	public GameObject textureShop;
	public GameObject skillsShop;

	Color tabsColor;
	Color highlitedColor;
	[Header("Stats")]
	public GameObject playerTab;
	public GameObject statsTab;
	[Header("PlayerTab")]
	public GameObject UsernameChange;
	public Image levelProgress;
	public Text levelProgressPercent;
	public Text levelText;
	public Text nextLevelExpText;
	public Text currentExpText;
	public Text usernameText;
	public InputField UsernameInput;
	public Text currentUsernamePlaceHolder;
	public Text longerUsernameText;
	public Text experienceTextPlayerTab;
	public Text moneyTextPlayerTab;

	void Start() {
		if(PlayerPrefs.GetInt("hasPlayed12") == 0)
		{
			ResetAll();
			Debug.Log("first play");
			HackerMode();
			PlayerPrefs.SetInt("hasPlayed12" , 1);
		}else if(PlayerPrefs.GetInt("hasPlayed12") == 1)
		{
			Debug.Log("has Played");
		}

		tabsColor = weaponShopTitle.color;


		activeScreenResIndex = PlayerPrefs.GetInt ("screen res index");
		bool isFullscreen = (PlayerPrefs.GetInt ("fullscreen") == 1)?true:false;

		volumeSliders [0].value = AudioManager.instance.masterVolumePercent;
		volumeSliders [1].value = AudioManager.instance.musicVolumePercent;
		volumeSliders [2].value = AudioManager.instance.sfxVolumePercent;

		for (int i = 0; i < resolutionToggles.Length; i++) {
			resolutionToggles [i].isOn = i == activeScreenResIndex;
		}

		fullscreenToggle.isOn = isFullscreen;
		optionsMenuHolder.SetActive (false);
		statsMenuHolder.SetActive(false);
		shopMenu.SetActive(false);
	}

	void Update()
	{
		UpdateStats();
	}
	#region ShopTabs
	public void ShowGunShop()
	{
		textureShop.SetActive(false);
		skillsShop.SetActive(false);
		gunShop.SetActive(true);

		weaponShopTitle.color = Color.white;
		textureShopTitle.color = tabsColor;
		skillsShopTitle.color = tabsColor;
	}

	public void ShowTextureShop()
	{	
		skillsShop.SetActive(false);
		gunShop.SetActive(false);
		textureShop.SetActive(true);

		weaponShopTitle.color = tabsColor;
		textureShopTitle.color = Color.white;
		skillsShopTitle.color = tabsColor;
	}

	public void ShowSkillsShop()
	{
		textureShop.SetActive(false);
		gunShop.SetActive(false);
		skillsShop.SetActive(true);

		weaponShopTitle.color = tabsColor;
		textureShopTitle.color = tabsColor;
		skillsShopTitle.color = Color.white;
	}
	#endregion
	#region SkillsTab
		public void ShowStatsPanel()
		{
			playerTab.SetActive(false);
			statsTab.SetActive(true);
		}
		public void ShowPlayersPanel()
		{
			statsTab.SetActive(false);
			playerTab.SetActive(true);
			HideUsernameChange();
		}

		public void ShowUsernameChange()
		{
			UsernameChange.SetActive(true);
			longerUsernameText.text = "";
			if(PlayerPrefs.GetString("username") == "")
			{
				currentUsernamePlaceHolder.text = "Username";
			}else{
				currentUsernamePlaceHolder.text = PlayerPrefs.GetString("username");	
			}
		}

		public void HideUsernameChange()
		{
			UsernameChange.SetActive(false);
		}

		public void CreateUsername()
		{
			if(UsernameInput.text == "")
			{
				longerUsernameText.text = "The username must contain at least one character";
			}
			else if(UsernameInput.text.Length > 20)
			{
				longerUsernameText.text = "The username must contain less than 20 characters";
			}
			else{
				longerUsernameText.text = "";
				PlayerPrefs.SetString("username",UsernameInput.text);	
				HideUsernameChange();			
			}
		}

	#endregion
	#region PlayerSettings
	public void ShowResolutionsPanel()
	{
		resolutionsPanel.SetActive(true);
	}

	public void HideResolutionsPanel()
	{
		resolutionsPanel.SetActive(false);
	}

	public void ShowDifficultyPanel()
	{
		difficultyPanel.SetActive(true);
	}

	public void HideDifficultyPanel()
	{
		difficultyPanel.SetActive(false);
	}

	public void ShowControlsPanel()
	{
		controlsPanel.SetActive(true);
	}

	public void HideControlsPanel()
	{
		controlsPanel.SetActive(false);
	}

	#endregion
	#region  Menus
	public void Play() {
		SceneManager.LoadScene ("Game");
		ScoreKeeper.score = 0;
		ScoreKeeper.EnemiesKilledinRound = 0;
	}

	public void Quit() {
		PlayerPrefs.Save();
		ShopManager.instance.SavePurchasedGunsIndex();
		ShopManager.instance.SavePurchasedColorsIndex();
		Application.Quit ();
	}

	public void OptionsMenu() {
		statsMenuHolder.SetActive(false);
		shopMenu.SetActive(false);
		optionsMenuHolder.SetActive (true);
		controlsPanel.SetActive(false);
	}

	public void ShopMenu()
	{
		optionsMenuHolder.SetActive(false);
		statsMenuHolder.SetActive(false);
		shopMenu.SetActive(true);
		ShopManager.instance.HideNotEnoughMoneyPanel();
		ShowGunShop();
	}

	public void StatsMenu() {
		optionsMenuHolder.SetActive(false);
		shopMenu.SetActive(false);
		statsMenuHolder.SetActive (true);
		ShowStatsPanel();
	}

	public void MainMenu() {
		mainMenuHolder.SetActive (true);
		optionsMenuHolder.SetActive (false);
	}
	#endregion
	#region UpdateAndReset
	public void ResetAll()
	{
		PlayerPrefs.SetInt("totalkills" , 0);
		PlayerPrefs.SetInt("highscore" , 0);
		PlayerPrefs.SetInt("time survived in history as int" , 0);
		PlayerPrefs.SetString("time survived in history as string" , "00:00:00");
		PlayerPrefs.SetInt("totalscore" , 0);
		PlayerPrefs.SetInt("total shots" , 0);
		PlayerPrefs.SetInt("longest time survived as int" , 0);
		PlayerPrefs.SetString("longest time survived as string" , "00:00:00");
		PlayerPrefs.SetInt("accuracy", 0);
		PlayerPrefs.SetInt("highest wave", 0);
		PlayerPrefs.SetInt("money" , 0);
		PlayerPrefs.SetInt("totalmoney",0);
		PlayerPrefs.SetInt("level",1);
		PlayerPrefs.SetInt("experience",0);
		PlayerPrefs.SetInt("nextlevelexp",100);
		PlayerPrefs.SetString("username","Username");
		ShopManager.instance.ResetGunShop();
		ShopManager.instance.ResetColorShop();
		ShopManager.instance.ResetBulletsShop();
		ShopManager.instance.ResetTilesShop();
		ShopManager.instance.ResetSkillsShop();
	}

	public void ResetStats()
	{
		PlayerPrefs.SetInt("totalkills" , 0);
		PlayerPrefs.SetInt("highscore" , 0);
		PlayerPrefs.SetInt("time survived in history as int" , 0);
		PlayerPrefs.SetString("time survived in history as string" , "00:00:00");
		PlayerPrefs.SetInt("totalscore" , 0);
		PlayerPrefs.SetInt("total shots" , 0);
		PlayerPrefs.SetInt("longest time survived as int" , 0);
		PlayerPrefs.SetString("longest time survived as string" , "00:00:00");
		PlayerPrefs.SetInt("accuracy", 0);
		PlayerPrefs.SetInt("highest wave", 0);
	}

	void HackerMode()
	{
		PlayerPrefs.SetInt("money",100000);
		PlayerPrefs.SetInt("totalmoney",100000);
		PlayerPrefs.SetInt("level",50);
		PlayerPrefs.SetInt("experience",50*100);
		PlayerPrefs.SetInt("nextlevelexp",(50*100)+100);
	}

	public void UpdateStats()
	{
		EnemiesKilled.text = PlayerPrefs.GetInt("totalkills").ToString();
		HighScore.text = PlayerPrefs.GetInt("highscore").ToString();
		TotalTimeSurvivedText.text = PlayerPrefs.GetString("time survived in history as string");
		TotalScore.text = PlayerPrefs.GetInt("totalscore").ToString();
		totalShots.text = PlayerPrefs.GetInt("total shots").ToString();
		longestTimeSurvived.text = PlayerPrefs.GetString("longest time survived as string");
		//accuracyText.text = PlayerPrefs.GetInt("accuracy").ToString()+"%";
		totalMoneyWonText.text = "$"+PlayerPrefs.GetInt("totalmoney").ToString();
		HighestWaveSurvived.text = PlayerPrefs.GetInt("highest wave").ToString();


		moneyText.text = "$"+PlayerPrefs.GetInt("money").ToString();
		//levelProgressPercent.text =	(100-(PlayerPrefs.GetInt("nextlevelexp")-PlayerPrefs.GetInt("experience"))).ToString()+"%";
		float levelProgressAmount = 100-(PlayerPrefs.GetInt("nextlevelexp")-PlayerPrefs.GetInt("experience"));
		levelProgress.fillAmount = levelProgressAmount/100;
		levelText.text = PlayerPrefs.GetInt("level").ToString();
		nextLevelExpText.text = PlayerPrefs.GetInt("nextlevelexp").ToString();
		currentExpText.text = PlayerPrefs.GetInt("experience").ToString();
		if(PlayerPrefs.GetString("username") == "")
		{
			usernameText.text = "Username";
		}
		else{
			usernameText.text = PlayerPrefs.GetString("username");
		}
		experienceTextPlayerTab.text = PlayerPrefs.GetInt("experience").ToString();
		moneyTextPlayerTab.text = "$"+PlayerPrefs.GetInt("money").ToString();
	}
	#endregion
	#region screenresandvolume
	public void SetScreenResolution(int i) {
		if (resolutionToggles [i].isOn) {
			activeScreenResIndex = i;
			float aspectRatio = 16 / 9f;
			Screen.SetResolution (screenWidths [i], (int)(screenWidths [i] / aspectRatio), false);
			PlayerPrefs.SetInt ("screen res index", activeScreenResIndex);
			PlayerPrefs.Save ();
		}
	}

	public void SetFullscreen(bool isFullscreen){
		for (int i = 0; i < resolutionToggles.Length; i++) {
			resolutionToggles [i].interactable = !isFullscreen;
		}

		if (isFullscreen) {
			Resolution[] allResolutions = Screen.resolutions;
			Resolution maxResolution = allResolutions [allResolutions.Length - 1];
			Screen.SetResolution (maxResolution.width, maxResolution.height, true);
		} else {
			SetScreenResolution (activeScreenResIndex);
		}

		PlayerPrefs.SetInt ("fullscreen", ((isFullscreen) ? 1 : 0));
		PlayerPrefs.Save ();
	}

	public void SetMasterVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Master);
	}

	public void SetMusicVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Music);
	}

	public void SetSfxVolume(float value) {
		AudioManager.instance.SetVolume (value, AudioManager.AudioChannel.Sfx);
	}

	public void SetDifficulty(int difficultyNumber)
	{
		PlayerPrefs.SetInt("difficulty",difficultyNumber);
	}
	#endregion
}
