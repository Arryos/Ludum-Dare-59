using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : Singleton<GameManager>
{
	[field: SerializeField]
	public PlayerDamagable PlayerDamagable { get; private set; }

	public Frequencies PlayerFrequency => PlayerDamagable.Frequency;

	public void GameOver()
	{
		Debug.Log("Game Over");
	}

}
