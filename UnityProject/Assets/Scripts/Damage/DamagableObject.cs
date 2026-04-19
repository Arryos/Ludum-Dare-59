using System;

public class DamagableObject : FrequencyObject
{
	public event EventHandler OnDeath;

	public virtual void Die()
	{
		OnDeath?.Invoke(this, EventArgs.Empty);
	}
}