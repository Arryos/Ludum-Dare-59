using Unity.AI.Navigation;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.AI;

public class NavigationFrequencyAdapter : MonoBehaviour
{
	[Header("Agents")]
	[SerializeField]
	NavMeshAgent[] agents;

	[Header("Surfaces")]
	[SerializeField]
	NavMeshSurface squareSurface;
	[SerializeField]
	NavMeshSurface triangleSurface;
	[SerializeField]
	NavMeshSurface waveSurface;

	private void OnEnable()
	{
		GameManager.Instance.PlayerDamagable.onPlayerFrequencyChanged += ConnectNewSurfaceToAgents;
	}
	private void OnDisable()
	{
		GameManager.Instance.PlayerDamagable.onPlayerFrequencyChanged -= ConnectNewSurfaceToAgents;
	}

	private void ConnectNewSurfaceToAgents(Frequencies frequency)
	{
		string currentSurfaceName = "";
		switch (frequency)
		{
			case Frequencies.Square:
				currentSurfaceName = "Square";
				break;
			case Frequencies.Triangle:
				currentSurfaceName = "Triangle";
				break;
			case Frequencies.Wave:
				currentSurfaceName = "Wave";
				break;
			default:
				break;
		}

		foreach (NavMeshAgent agent in agents)
		{
			if(agent != null)
			{
				agent.areaMask = 1 << NavMesh.GetAreaFromName(currentSurfaceName);
			}
		}
	}
}
