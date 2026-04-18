using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : Singleton<MonoBehaviour>
{
	public PlayerDamagable PlayerDamagable { get; set; }
	public Frequencies PlayerFrequency => PlayerDamagable.Frequency;

	public Action onPlayerFrequencyChanged;
}
