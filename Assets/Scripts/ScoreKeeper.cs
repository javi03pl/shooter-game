using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class ScoreKeeper : MonoBehaviour {

	public static int score;
	public static int totalScore;
	public static int EnemiesKilledinRound;
	public static int totalTimeSurvivedInHistory;
	public static int TimeSurvived;
	public static int moneyDivider;
	//public GameObject newLevelUI;
	[HideInInspector]
	public int streakCount ;
	[HideInInspector]
	public int scoreToAdd;
	public Text streakCountText;
	float streakExpiryTime = 1f;
	float lastEnemyKillTime;

	void Start() {
		Enemy.OnDeathStatic += OnEnemyKilled;
		FindObjectOfType<Player> ().OnDeath += OnPlayerDeath;
	}

	void Update()
	{
		//streak count
		if(streakCount>0)
		{
			streakCountText.gameObject.SetActive(true);
			streakCountText.text = "x"+streakCount.ToString();
		}else{
			streakCountText.gameObject.SetActive(false);
		}

		
	}

	public string ConvertTimeToString(float time)
	{
		int seconds = (int)time % 60;
		int minutes = (int)(time/60) % 60;
		int hours = (int)(time/3600) % 24;

		string niceTime = string.Format("{0:0}:{1:00}:{2:00}", hours , minutes, seconds);
		return niceTime;
	}

		public string ConvertTimeToStringAsMinutes(float time)
	{
		int seconds = (int)time % 60;
		int minutes = (int)(time/60) % 60;

		string niceTime = string.Format("{1:00}:{2:00}" , minutes, seconds);
		return niceTime;
	}

	void OnEnemyKilled() {
		//adding score
		EnemiesKilledinRound ++;
		PlayerPrefs.SetInt("totalkills" , PlayerPrefs.GetInt("totalkills") + 1);

		if (Time.time < lastEnemyKillTime + streakExpiryTime) {
			streakCount++;
		} else if(Time.time > lastEnemyKillTime + streakExpiryTime) {
			streakCount = 0;
		}

		lastEnemyKillTime = Time.time;

		scoreToAdd = 1 + 2 * streakCount; 
		score += scoreToAdd;
		PlayerPrefs.SetInt("totalscore" , PlayerPrefs.GetInt("totalscore") + scoreToAdd);
	}

	void OnPlayerDeath() {
		Enemy.OnDeathStatic -= OnEnemyKilled;
		moneyDivider = 5;
		streakCountText.gameObject.SetActive(false);
		//highscore
		if(PlayerPrefs.GetInt("highscore") < score)
		{
			PlayerPrefs.SetInt("highscore" , score);
		}
		//highest wave
		if(PlayerPrefs.GetInt("highest wave")<Spawner.currentWaveNumber)
		{
			PlayerPrefs.SetInt("highest wave", Spawner.currentWaveNumber);
		}

		PlayerPrefs.SetInt("experience",PlayerPrefs.GetInt("experience") + EnemiesKilledinRound/5);
		Debug.Log(PlayerPrefs.GetInt("experience"));
		if(PlayerPrefs.GetInt("experience") >= PlayerPrefs.GetInt("nextlevelexp"))
		{
			PlayerPrefs.SetInt("level",PlayerPrefs.GetInt("level")+1);
			PlayerPrefs.SetInt("nextlevelexp",PlayerPrefs.GetInt("nextlevelexp")+100);
			PlayerPrefs.SetInt("money",PlayerPrefs.GetInt("money")+100);
		}

		//longest time survived score
		TimeSurvived = (int)Time.timeSinceLevelLoad;
			if(PlayerPrefs.GetInt("longest time survived as int") < TimeSurvived)
			{
				PlayerPrefs.SetInt("longest time survived as int" , TimeSurvived);
				PlayerPrefs.SetString("longest time survived as string" , ConvertTimeToString(PlayerPrefs.GetInt("longest time survived as int")));
			}

			PlayerPrefs.SetInt("time survived in history as int" , PlayerPrefs.GetInt("time survived in history as int")+TimeSurvived); 
			PlayerPrefs.SetString("time survived in history as string" , ConvertTimeToString(PlayerPrefs.GetInt("time survived in history as int")));
	}
	
}
