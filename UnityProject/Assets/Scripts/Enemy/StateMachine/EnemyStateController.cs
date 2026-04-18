using UnityEngine;

public class EnemyStateController : MonoBehaviour
{
	[SerializeField]
	private EnemyState initialState;

	[SerializeField]
	private EnemyState[] states;

	private EnemyState currentState;

    void OnEnable()
    {
        foreach(EnemyState state in states)
		{
			state.OnStateTransition += OnStateTransition;
		}
    }

	private void OnDisable()
	{
		foreach (EnemyState state in states)
		{
			state.OnStateTransition -= OnStateTransition;
		}
	}

	private void Start()
	{
		StartStateMachine();
	}

	public void StartStateMachine()
	{
		if (initialState)
		{
			initialState.Enter();
			currentState = initialState;
		}
	}

	void Update()
    {
		if (currentState)
		{
			currentState.UpdateState(Time.deltaTime);
		}
    }

	private void FixedUpdate()
	{
		if (currentState)
		{
			currentState.FixedUpdateState(Time.fixedDeltaTime);
		}
	}

	private void OnStateTransition(EnemyState fromState, EnemyState toState)
	{
		if (fromState != currentState) return;

		if (currentState)
		{
			currentState.Exit();
		}

		if (toState)
		{
			toState.Enter();
		}

		currentState = toState;
	}
}
