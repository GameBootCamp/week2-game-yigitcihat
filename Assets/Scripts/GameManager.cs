﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
	public static GameManager gm;

	public bool gameIsOver = false;

	public GameObject player;

	public enum gameStates { Playing, Death, GameOver, BeatLevel };
	public gameStates gameState = gameStates.Playing;

	public int score = 0;
	public bool canBeatLevel = false;
	public int beatLevelScore = 0;

	public GameObject mainCanvas;
	public Text mainScoreDisplay;
	public Text lifeDisplay;
	public GameObject gameOverCanvas;
	public Text gameOverScoreDisplay;

	public GameObject beatLevelCanvas;

	public AudioSource backgroundMusic;
	public AudioClip gameOverSFX;

	public AudioClip beatLevelSFX;

	private Health playerHealth;
	Life life;

	void Start()
	{
		if (gm == null)
			gm = gameObject.GetComponent<GameManager>();

		if (player == null)
		{
			player = GameObject.FindWithTag("Player");
		}

		playerHealth = player.GetComponent<Health>();

		// setup score display
		Collect(0);

		// make other UI inactive
		gameOverCanvas.SetActive(false);
		if (canBeatLevel)
			beatLevelCanvas.SetActive(false);
	}

	void Update()
	{
        life.KalanHaklariKontrolEt(int.Parse(playerHealth.healthPoints.ToString()));
		switch (gameState)
		{
			case gameStates.Playing:
				if (playerHealth.isAlive == false)
				{
					// update gameState
					gameState = gameStates.Death;

					// set the end game score
					gameOverScoreDisplay.text = mainScoreDisplay.text;

					// switch which GUI is showing		
					mainCanvas.SetActive(false);
					gameOverCanvas.SetActive(true);
				}
				else if (canBeatLevel && score >= beatLevelScore)
				{
					// update gameState
					gameState = gameStates.BeatLevel;

					// hide the player so game doesn't continue playing
					player.SetActive(false);

					// switch which GUI is showing			
					mainCanvas.SetActive(false);
					beatLevelCanvas.SetActive(true);
				}
				break;
			case gameStates.Death:
				backgroundMusic.volume -= 0.01f;
				if (backgroundMusic.volume <= 0.0f)
				{
					AudioSource.PlayClipAtPoint(gameOverSFX, gameObject.transform.position);

					gameState = gameStates.GameOver;
				}
				break;
			case gameStates.BeatLevel:
				backgroundMusic.volume -= 0.01f;
				if (backgroundMusic.volume <= 0.0f)
				{
					AudioSource.PlayClipAtPoint(beatLevelSFX, gameObject.transform.position);

					gameState = gameStates.GameOver;
				}
				break;
			case gameStates.GameOver:
				// nothing
				break;
		}

	}


	public void Collect(int amount)
	{
		score += amount;
		if (canBeatLevel)
		{
			mainScoreDisplay.text = score.ToString() + " of " + beatLevelScore.ToString();
		}
		else
		{
			mainScoreDisplay.text = score.ToString();
		}

	}
	public void targetHit(int scoreAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		score += scoreAmount;
		mainScoreDisplay.text = score.ToString();

	}
	public void damageHit(int DamageAmount)
	{
		// increase the score by the scoreAmount and update the text UI
		playerHealth.healthPoints -= DamageAmount;
		print(playerHealth.healthPoints);
		lifeDisplay.text = playerHealth.healthPoints.ToString();

	}
}
