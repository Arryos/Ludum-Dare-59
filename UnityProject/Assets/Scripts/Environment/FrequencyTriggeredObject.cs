using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FrequencyTriggeredObject : MonoBehaviour
{
	[SerializeField]
	string message = "Thank You For Playing !";
	[SerializeField]
	private float step = 2.5f;
	[SerializeField]
	private TextMeshPro finalText;
	[SerializeField]
	AudioSource sfx;
	[SerializeField]
	Color square;
	[SerializeField]
	Color triangle;
	[SerializeField]
	Color wave;

	int maxCount;
	int count = 0;
	Frequencies upcomingFrequency;
	Camera mainCamera;
	TweenShake shake;

	private void Awake()
	{
		maxCount = message.Length;
		mainCamera = Camera.main;
		shake = mainCamera.GetComponent<TweenShake>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent(out FrequencyObject bullet))
		{
			if (count < maxCount)
			{
				upcomingFrequency = bullet.Frequency;
				mainCamera.transform.position -= mainCamera.transform.forward * step;

				string colorHex="#FFFFFF";

				switch (upcomingFrequency)
				{
					case Frequencies.Square:
						colorHex = square.ToHexString().Substring(0, square.ToHexString().Length - 2);
						break;
					case Frequencies.Triangle:
						colorHex = triangle.ToHexString().Substring(0, triangle.ToHexString().Length - 2);
						break;
					case Frequencies.Wave:
						colorHex = wave.ToHexString().Substring(0, wave.ToHexString().Length - 2);
						break;
					default:
						break;
				}
				finalText.text += $"<color=#{colorHex}>" + message[count] + "</color>";
				//ColorCharacter(count, colorHex);

				count++;
			}
			sfx.Play();
			shake.Shake(0.5f, 0.5f);
		}
	}
	public void ColorCharacter(int index, string colorHex)
	{
		string text = finalText.text;

		if (index < 0 || index >= text.Length) return;

		finalText.text = finalText.text.Replace(finalText.text[count].ToString(),
			$"<color=#{colorHex}>" + finalText.text[count].ToString() + "</color>");
	}
}
