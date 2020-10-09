using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHit : MonoBehaviour
{
	// Start is called before the first frame update

	// target impact on game
	public int DamageAmount = 1;


	// when collided with another gameObject
	void OnCollisionEnter(Collision newCollision)
	{
		// exit if there is a game manager and the game is over
		if (GameManager.gm)
		{
			if (GameManager.gm.gameIsOver)
				return;
		}

		// only do stuff if hit by a projectile
		if (newCollision.gameObject.tag == "Player" || newCollision.gameObject.name == "Sword")
		{

			// if game manager exists, make adjustments based on target properties
			if (GameManager.gm)
			{
				GameManager.gm.damageHit(DamageAmount);
			}

			Destroy(newCollision.gameObject);
		}
	}
}
