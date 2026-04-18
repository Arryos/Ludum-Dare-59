using DG.Tweening;
using UnityEngine;

public class TweenBounce : MonoBehaviour
{
	[SerializeField]
	float startScale;
	public void Bounce()
	{
		transform.localScale = Vector3.one * startScale;

		transform.DOScale(1f, 0.5f)
				 .SetEase(Ease.OutBack);
	}
}
