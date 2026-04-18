using System;

public class DamagableObject : FrequencyObject
{
	public event EventHandler OnDeath;

	protected virtual void Die()
	{
		OnDeath?.Invoke(this, EventArgs.Empty);
	}
}