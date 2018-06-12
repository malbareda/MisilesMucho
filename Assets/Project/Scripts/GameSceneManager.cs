using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
	#region Constants
	private float ENEMY_INTERVAL_MAX = 2.0f;
	private float ENEMY_INTERVAL_MIN = 0.2f;
	private float TIME_TO_MINIMUM_INTERVAL = 30.0f;	
	#endregion Constants

	#region Unity Fields
	public Camera mainCamera;
	public Text scoreText;
	public Text gameOverText;
	public PlayerController player;
	public GameObject enemyPrefab;
	#endregion Unity Fields

	#region Fields
	private int score;
	private float gameTimer;
	private float enemyTimer;
	private bool gameOver;
	#endregion Fields

	#region Methods
	public void Start ()
	{
		Time.timeScale = 1;
		gameOverText.enabled = false;

		enemyTimer = ENEMY_INTERVAL_MAX;

		player.OnHitEnemy += OnGameOver;
	}

	public void Update ()
	{
		if (gameOver)
		{
			if (Input.GetKeyDown("r"))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}

			scoreText.enabled = false;
			gameOverText.enabled = true;

			gameOverText.text = "Game over! Total score: " + score + "\nPress R to restart";

			return;
		}

		gameTimer += Time.deltaTime;
		enemyTimer -= Time.deltaTime;
		if (enemyTimer <= 0)
		{
		    float intervalPercentage = Mathf.Min(gameTimer / TIME_TO_MINIMUM_INTERVAL, 1);
		    enemyTimer = ENEMY_INTERVAL_MAX - (ENEMY_INTERVAL_MAX - ENEMY_INTERVAL_MIN) * intervalPercentage;

		    GameObject enemy = GameObject.Instantiate<GameObject>(enemyPrefab);
		    enemy.transform.SetParent(this.transform);
            

		    float randomAngle = Random.Range(0, 360) * Mathf.Deg2Rad;
		    float distance = 15;

		    enemy.transform.position = new Vector3
		    (
		    	player.transform.position.x + Mathf.Cos(randomAngle) * distance,
		    	-3.6f,
		    	player.transform.position.z + Mathf.Sin(randomAngle) * distance
		    );

		    enemy.GetComponent<EnemyController>().Direction = -(player.transform.position - enemy.transform.position).normalized;

		    enemy.GetComponent<EnemyController>().OnKill += OnKillEnemy;
		}
	}

	public void OnKillEnemy ()
	{
		score += 100;
		scoreText.text = "Score: " + score;
	}

	public void OnGameOver ()
	{
		gameOver = true;

		Time.timeScale = 0;
	}

    /*public static Vector3 getPlayerPosition()
    {
        return player.transform.position;
    }*/

	#endregion Methods
}