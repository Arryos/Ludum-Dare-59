using System;
using UnityEngine;


[CreateAssetMenu(fileName = "SO_Wave", menuName = "ScriptableObjects/SO_Wave", order = 1)]
public class SO_Wave : ScriptableObject
{
	[System.Serializable]
    public enum Waves
	{
		Square,
		Triangle,
		Sinusoid,
		
	}

	[SerializeField] private Waves m_wave;

	public event Action<Waves> OnValueChanged;

	// When value is modified trigger event to force update value where needed
	public void Set(Waves p_value)
	{
		if (m_wave != p_value)
		{
			m_wave = p_value;
			OnValueChanged?.Invoke(m_wave);
		}
	}

	public Waves Get()
	{
		return m_wave;
	}

}
