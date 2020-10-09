using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
	[SerializeField]
	private GameObject heart1, heart2, heart3;

	public void KalanHaklariKontrolEt(int kalanHak)
	{
		switch (kalanHak)
		{
			case 3:
				heart1.SetActive(true);
				heart2.SetActive(true);
				heart3.SetActive(true);
				break;

			case 2:
				heart1.SetActive(true);
				heart2.SetActive(true);
				heart3.SetActive(false);
				break;

			case 1:
				heart1.SetActive(true);
				heart2.SetActive(false);
				heart3.SetActive(false);
				break;

			case 0:
				heart1.SetActive(false);
				heart2.SetActive(false);
				heart3.SetActive(false);
				break;

		}

	}
}
