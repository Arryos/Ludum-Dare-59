using System.Collections;
using UnityEngine;

public class ColorChangerBehaviour : MonoBehaviour
{
	[SerializeField]
	EnemyDamagable enemyDamageable;

	[SerializeField]
	TweenBounce tweenBounce;

	[Header("Parameters")]
	[SerializeField]
	private float colorChangePeriod;

	private Coroutine colorChangeCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
		colorChangeCoroutine = StartCoroutine(ColorChangeCountdown());
    }

	private void ChangeColor()
	{
		foreach(Shield shield in enemyDamageable.Shields)
		{
			shield.Frequency = shield.Frequency switch
			{
				Frequencies.Square => Frequencies.Triangle,
				Frequencies.Triangle => Frequencies.Wave,
				_ => Frequencies.Square
			};
		}

		tweenBounce.Bounce();
	}

	private IEnumerator ColorChangeCountdown()
	{
		float elapsedTime = 0f;
		while (elapsedTime < colorChangePeriod)
		{
			elapsedTime += Time.deltaTime;
			yield return null;

			if(elapsedTime >= colorChangePeriod)
			{
				elapsedTime = 0f;
				ChangeColor();
			}
		}
	}
}
