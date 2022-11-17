using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public bool devMode;

	public Wave[] waves;
	public Enemy enemy;

	LivingEntity playerEntity;
	Transform playerT;

	public static Wave currentWave {get; private set;}
	public static int currentWaveNumber;

	int enemiesRemainingToSpawn;
	int enemiesRemainingAlive;
 	float nextSpawnTime;

	MapGenerator map;

	float timeBetweenCampingChecks = 2;
	float campThresholdDistance = 1.5f;
	float nextCampCheckTime;
	Vector3 campPositionOld;
	bool isCamping;

	bool isDisabled;

	public event System.Action<int> OnNewWave;

	void Start() {
		playerEntity = FindObjectOfType<Player> ();
		playerT = playerEntity.transform;

		nextCampCheckTime = timeBetweenCampingChecks + Time.time;
		campPositionOld = playerT.position;
		playerEntity.OnDeath += OnPlayerDeath;

		currentWaveNumber = 0;

		map = FindObjectOfType<MapGenerator> ();
		NextWave ();
	}

	void Update() {
		if (!isDisabled) {
			if (Time.time > nextCampCheckTime) {
				nextCampCheckTime = Time.time + timeBetweenCampingChecks;

				isCamping = (Vector3.Distance (playerT.position, campPositionOld) < campThresholdDistance);
				campPositionOld = playerT.position;
			}

			if ((enemiesRemainingToSpawn > 0 || currentWave.infinite) && Time.time > nextSpawnTime) {
				enemiesRemainingToSpawn--;
				nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

				StartCoroutine ("SpawnEnemy");
			}
		}

		if (devMode) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				StopCoroutine("SpawnEnemy");
				foreach (Enemy enemy in FindObjectsOfType<Enemy>()) {
					GameObject.Destroy(enemy.gameObject);
				}
				NextWave();
			}
		}
	}

	IEnumerator SpawnEnemy() {
		float spawnDelay = 1;
		float tileFlashSpeed = 4;

		Transform spawnTile = map.GetRandomOpenTile ();
		if (isCamping) {
			spawnTile = map.GetTileFromPosition(playerT.position);
		}
		Material tileMat = spawnTile.GetComponent<Renderer>().material;
		// Color initialColour = Color.white;
		Color initialColour = tileMat.color;
		Color flashColour = Color.red;
		float spawnTimer = 0;

		while (spawnTimer < spawnDelay) {
			
			tileMat.color = Color.Lerp(initialColour,flashColour, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));

			spawnTimer += Time.deltaTime;
			yield return null;
		}

		Enemy spawnedEnemy = Instantiate(enemy, spawnTile.position + Vector3.up, Quaternion.identity) as Enemy;
		spawnedEnemy.OnDeath += OnEnemyDeath;
		spawnedEnemy.SetCharacteristics (currentWave.moveSpeed, currentWave.hitsToKillPlayer, currentWave.enemyHealth, currentWave.skinColour);
	}

	void OnPlayerDeath() {
		isDisabled = true;
	}

	void OnEnemyDeath() {
		enemiesRemainingAlive --;
		if (enemiesRemainingAlive == 0) {
			NextWave();
		}
	}

	void ResetPlayerPosition() {
		playerT.position = map.GetTileFromPosition (Vector3.zero).position + Vector3.up * 3;
	}

	void NextWave() {
		if (currentWaveNumber > 0) {
			AudioManager.instance.PlaySound2D ("Level Complete");
		}
		currentWaveNumber ++;

		if (currentWaveNumber - 1 < waves.Length) {
			currentWave = waves [currentWaveNumber - 1];

			if(PlayerPrefs.GetInt("difficulty") == 1 )
			{
				currentWave.moveSpeed = Random.Range(3f ,4f);
				currentWave.hitsToKillPlayer = Random.Range(4,5);
				currentWave.enemyHealth = Random.Range(1,2);
				currentWave.timeBetweenSpawns = Random.Range(.6f , .9f);
			}
			else if(PlayerPrefs.GetInt("difficulty") == 2 )
			{
				currentWave.moveSpeed = Random.Range(3f , 4f);
				currentWave.hitsToKillPlayer = Random.Range(2,4);
				currentWave.enemyHealth = Random.Range(2,4);
				currentWave.timeBetweenSpawns = Random.Range(.5f , .8f);
			}
			else if(PlayerPrefs.GetInt("difficulty") == 3 )
			{
				currentWave.moveSpeed = Random.Range(4f , 5f);
				currentWave.hitsToKillPlayer = Random.Range(2,3);
				currentWave.enemyHealth = Random.Range(3,6);
				currentWave.timeBetweenSpawns = Random.Range(.4f , .7f);
			}else{
				currentWave.moveSpeed = Random.Range(3f , 5f);;
				currentWave.hitsToKillPlayer = Random.Range(3,6);
				currentWave.enemyHealth = Random.Range(1,4);
				currentWave.timeBetweenSpawns = Random.Range(.2f , .9f);
			}


			currentWave.skinColour = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
			currentWave.enemyCount = Random.Range(20,40);


			enemiesRemainingToSpawn = currentWave.enemyCount;
			enemiesRemainingAlive = enemiesRemainingToSpawn;

			if (OnNewWave != null) {
				OnNewWave(currentWaveNumber);
			}
			ResetPlayerPosition();
		}
	}

	[System.Serializable]
	public class Wave {
		public bool infinite;
		public int enemyCount;
		public float timeBetweenSpawns;
		public float moveSpeed ;
		public int hitsToKillPlayer;
		public float enemyHealth ;
		public Color skinColour;
	}

}
