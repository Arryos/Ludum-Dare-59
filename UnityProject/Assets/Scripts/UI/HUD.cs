using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
	[SerializeField]
	private SO_Wave wave;

	[SerializeField]
	private List<GameObject> screens = new List<GameObject>();

	[SerializeField]
	private List<GameObject> cursors = new List<GameObject>();

	private void OnEnable()
	{
		wave.OnValueChanged += ChangeWaveState;
	}

	private void OnDisable()
	{
		wave.OnValueChanged -= ChangeWaveState;
	}


	private void ChangeWaveState(SO_Wave.Waves waves)
	{
		if(waves == SO_Wave.Waves.Sinusoid)
		{
			// set cursor on sinusoid
			foreach(GameObject cursor in cursors)
			{
				cursor.SetActive(false);
			}
			cursors[0].SetActive(true);
			// highlight sinusoid wave on screen - hide others
			foreach(GameObject cache in screens)
			{
				cache.SetActive(true);
			}
			screens[0].SetActive(false);

		}
		else if(waves == SO_Wave.Waves.Triangle)
		{
			foreach (GameObject cursor in cursors)
			{
				cursor.SetActive(false);
			}
			cursors[1].SetActive(true);

			foreach (GameObject cache in screens)
			{
				cache.SetActive(true);
			}
			screens[1].SetActive(false);
		}
		else
		{
			foreach (GameObject cursor in cursors)
			{
				cursor.SetActive(false);
			}
			cursors[2].SetActive(true);

			foreach (GameObject cache in screens)
			{
				cache.SetActive(true);
			}
			screens[2].SetActive(false);
		}
	}
}
