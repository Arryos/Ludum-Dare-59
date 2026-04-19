using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
	[SerializeField]
	private SO_Wave wave;
	[SerializeField]
	private SO_Float so_scroll;

	[SerializeField]
	private List<GameObject> screens = new List<GameObject>();

	[SerializeField]
	private Slider slider;

	//Liste Animators
	[SerializeField]
	private List<Animator> animatorWaves = new List<Animator>();

	private void OnEnable()
	{
		wave.OnValueChanged += ChangeWaveState;
		so_scroll.OnValueChanged += ChangeValueSlider;
	}

	private void OnDisable()
	{
		wave.OnValueChanged -= ChangeWaveState;
		so_scroll.OnValueChanged -= ChangeValueSlider;
	}

	private void Start()
	{
		foreach (GameObject cache in screens)
		{
			cache.SetActive(true);
		}
		screens[1].SetActive(false);

		foreach (Animator anim in animatorWaves)
		{
			anim.speed = 0;
		}
		animatorWaves[1].speed = 1;
	}

	private void ChangeWaveState(SO_Wave.Waves waves)
	{
		if(waves == SO_Wave.Waves.Sinusoid)
		{
			// highlight sinusoid wave on screen - hide others
			foreach(GameObject cache in screens)
			{
				cache.SetActive(true);
			}
			screens[0].SetActive(false);

			// stop animations and continue sin anim
			foreach(Animator anim in animatorWaves)
			{
				anim.speed = 0;
			}
			animatorWaves[0].speed = 1;

		}
		else if(waves == SO_Wave.Waves.Triangle)
		{
			foreach (GameObject cache in screens)
			{
				cache.SetActive(true);
			}
			screens[1].SetActive(false);

			foreach (Animator anim in animatorWaves)
			{
				anim.speed = 0;
			}
			animatorWaves[1].speed = 1;
		}
		else
		{
			foreach (GameObject cache in screens)
			{
				cache.SetActive(true);
			}
			screens[2].SetActive(false);

			foreach (Animator anim in animatorWaves)
			{
				anim.speed = 0;
			}
			animatorWaves[2].speed = 1;
		}
	}

	private void ChangeValueSlider(float p_value)
	{
		slider.value = p_value;
	}
}
