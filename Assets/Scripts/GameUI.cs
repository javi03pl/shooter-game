using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	[Header("Menus")]
	public GameObject allUI;
	public Image fadePlane;
	public GameObject gameOverUI;
	public GameObject NewHighScoreUI;
	public GameObject newLevelUI;
	public GameObject PauseMenu;

	[Header("Game Info")]
	public RectTransform newWaveBanner;
	public Text newWaveTitle;
	public Text newWaveEnemyCount;
	public Text scoreUI;

	[Header("Game Over")]
	public Text EnemiesKilledUI;
	public Text gameOverScoreUI;
	public Text moneyWon;
	public RectTransform healthBar;

	[Header("PauseMenu")]
	public Text currentScore;
	public Text currentWave;
	public Text currentEnemiesKilled;
	public Button pauseButton;
	[HideInInspector]
	public bool isOverPauseButton = false;
	Spawner spawner;
	Player player;

	void Start () {
		allUI.SetActive (true);
		player = FindObjectOfType<Player> ();
		player.OnDeath += OnGameOver;
		ScoreKeeper.EnemiesKilledinRound = 0;
		ScoreKeeper.score = 0;
		StartCoroutine(Fade(Color.black, Color.clear , 1));
		PauseMenu.SetActive(false);
	}

	void Awake() {
		spawner = FindObjectOfType<Spawner> ();
		spawner.OnNewWave += OnNewWave;
	}

	void Update() {
		scoreUI.text = ScoreKeeper.score.ToString();
		
		EnemiesKilledUI.text = ScoreKeeper.EnemiesKilledinRound.ToString();
		float healthPercent = 0;
		if (player != null) {
			healthPercent = player.health / player.startingHealth;
		}
		healthBar.localScale = new Vector3 (healthPercent, 1, 1);
	}

	void OnNewWave(int waveNumber) {
		string[] numbers = { "One", "Two", "Three", "Four", "Five","Seven" , "Eight" , "Nine" , "Ten", "Eleven" , "Twelve" , "Thirteen" , "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen", "Twenty","Twenty-One" , "Twenty-Two" , "Twenty-Three" , "Twenty-Four"  , "Twenty-Five"  , "Twenty-Six"  , "Twenty-Seven" , "Twenty-Eight", "Twenty-Nine"  , "Thirty"  , "Thirty-One" , "Thirty-Two", "Thirty-Three" , "Thirty-Four", "Thirty-Five" , "Thirty-Six", "Thirty-Seven" , "Thirty-Eight" , "Thirty-Nine", "Forty" , "Forty-One" , "Forty-Two", "Forty-Three", "Forty-Four", "Forty-Five", "Forty-Six", "Forty-Seven", "Forty-Eight", "Forty-Nine", "Fifty" ,"Fifty-One" ,"Fifty-Two","Fifty-Three","Fifty-Four" ,"Fifty-Five" ,"Fifty-Six","Fifty-Seven" ,"Fifty-Eight","Fifty-Nine" , "Sixty","Sixty-One","Sixty-Two","Sixty-Three","Sixty-Four","Sixty-Five","Sixty-Six","Sixty-Seven","Sixty-Eight","Sixty-Nine","Seventy","Seventy-One" ,"Seventy-Two","Seventy-Three","Seventy-Four","Seventy-Five","Seventy-Six","Seventy-Seven","Seventy-Eight","Seventy-Nine" ,"Eighty","Eighty-One","Eighty-Two","Eighty-Three","Eighty-Four","Eighty-Five","Eighty-Six","Eighty-Seven","Eighty-Eight","Eighty-Nine","Ninety","Ninety-One","Ninety-Two","Ninety-Three","Ninety-Four","Ninety-Five","Ninety-Six","Ninety-Seven","Ninety-Eight","Ninety-Nine","One Hundred"};
		if(waveNumber>100)
		{
			newWaveTitle.text = "- Wave " + waveNumber + " -";
		}else{
			newWaveTitle.text = "- Wave " + numbers [waveNumber - 1] + " -";
		}
		

		string enemyCountString = ((spawner.waves [waveNumber - 1].infinite) ? "Infinite" : spawner.waves [waveNumber - 1].enemyCount + "");
		newWaveEnemyCount.text = "Enemies: " + enemyCountString;

		StopCoroutine ("AnimateNewWaveBanner");
		StartCoroutine ("AnimateNewWaveBanner");
	}
		
	void OnGameOver() {
		Cursor.visible = true;
		pauseButton.gameObject.SetActive(false);
		PauseMenu.SetActive(false);
		StartCoroutine(Fade(Color.clear, new Color(0,0,0,.9f),1));

		StartCoroutine(AnimateTextToValue(gameOverScoreUI , ScoreKeeper.score));
		StartCoroutine(AnimateTextToValue(EnemiesKilledUI , ScoreKeeper.EnemiesKilledinRound));


		PlayerPrefs.SetInt("money" , PlayerPrefs.GetInt("money")+ScoreKeeper.score/ScoreKeeper.moneyDivider);
		PlayerPrefs.SetInt("totalmoney",PlayerPrefs.GetInt("totalmoney")+ScoreKeeper.score/ScoreKeeper.moneyDivider);
		//StartCoroutine(AnimateGameOverMoneyWon());
		moneyWon.text = "+$"+(ScoreKeeper.score/ScoreKeeper.moneyDivider).ToString();

		if(PlayerPrefs.GetInt("highscore") < ScoreKeeper.score)
		{
			NewHighScoreUI.SetActive(true);
		}else if(PlayerPrefs.GetInt("highscore") > ScoreKeeper.score){
			NewHighScoreUI.SetActive(false);
		}

		scoreUI.gameObject.SetActive(false);
		healthBar.transform.parent.gameObject.SetActive (false);
		gameOverUI.SetActive (true);
	}

	public void PlayHoverEffect()
	{
		AudioManager.instance.PlaySound2D("Hover");
	}

	IEnumerator AnimateNewWaveBanner() {

		float delayTime = 1.5f;
		float speed = 3f;
		float animatePercent = 0;
		int dir = 1;

		float endDelayTime = Time.time + 1 / speed + delayTime;

		while (animatePercent >= 0) {
			animatePercent += Time.deltaTime * speed * dir;

			if (animatePercent >= 1) {
				animatePercent = 1;
				if (Time.time > endDelayTime) {
					dir = -1;
				}
			}

			newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp (-170, 45, animatePercent);
			yield return null;
		}

	}
		
	IEnumerator Fade(Color from, Color to, float time) {
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp(from,to,percent);
			yield return null;
		}
	}

	IEnumerator AnimateGameOverMoneyWon()
	{
		moneyWon.text = "+$0";
		int currentMoney = 0;

		while(currentMoney < ScoreKeeper.score/ScoreKeeper.moneyDivider)
		{
			currentMoney++;
			gameOverScoreUI.text = "+$" + currentMoney.ToString();
			AudioManager.instance.PlaySound2D("Hover");
			yield return null;
		}
	}


	IEnumerator AnimateTextToValue(Text textToAnimate , int valueToReach)
	{
		gameOverScoreUI.text = "0";
		int firstNumber = 0;

		while(firstNumber < valueToReach)
		{
			firstNumber++;
			textToAnimate.text = firstNumber.ToString();
			AudioManager.instance.PlaySound2D("Hover");
			yield return null;
		}
	}

	public void ShowPauseMenu()
	{
		Time.timeScale = 0;
		pauseButton.gameObject.SetActive(false);
		PauseMenu.SetActive(true);
		Cursor.visible = true;
		currentScore.text = ScoreKeeper.score.ToString();
		currentWave.text = Spawner.currentWaveNumber.ToString();
		currentEnemiesKilled.text = ScoreKeeper.EnemiesKilledinRound.ToString();
	}

	public void Resume()
	{
		PauseMenu.SetActive(false);
		pauseButton.gameObject.SetActive(true);
		Time.timeScale = 1;
	}

	public void ShowCursorOverPauseButton()
	{
		isOverPauseButton = true;
	}

	public void HideCursorOverPauseButton()
	{
		isOverPauseButton = false;
	}

	// UI Input
	public void StartNewGame() {
		Time.timeScale = 1;
		SceneManager.LoadScene ("Game");
	}

	public void ReturnToMainMenu() {
		Cursor.visible = true;
		Time.timeScale = 1;
		SceneManager.LoadScene ("Menu");
	}
}
