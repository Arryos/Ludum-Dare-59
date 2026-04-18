using UnityEngine;

public class DamagingObject : FrequencyObject
{
	[field: SerializeField] public DamageTargets Target { get; private set; } = DamageTargets.Player;
}