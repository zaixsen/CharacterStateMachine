using System;
using System.Collections.Generic;

public interface IStateMachineOwner { }

public class StateMachine
{

    private IStateMachineOwner _owner;

    private StateBase _currentState;
    private bool HasState { get => _currentState != null; }
    public StateBase CurrentState { get => _currentState; }

    private Dictionary<Type, StateBase> dic_states;

    public StateMachine(IStateMachineOwner stateMachineOwner)
    {
        _owner = stateMachineOwner;
        dic_states = new Dictionary<Type, StateBase>();
    }

    public void EnterState<T>() where T : StateBase, new()
    {
        Type stateType = typeof(T);
        //有状态 并且 状态相等
        if (HasState && stateType == _currentState.GetType()) return;

        if (HasState)
        {
            _currentState.ExitState();
        }

        _currentState = LoadState<T>();
        _currentState.EnterState();
    }

    public StateBase LoadState<T>() where T : StateBase, new()
    {
        Type stateType = typeof(T);

        if (!dic_states.TryGetValue(stateType, out StateBase stateBase))
        {
            stateBase = new T();
            stateBase.Init(_owner);
            dic_states.Add(stateType, stateBase);
        }
        return stateBase;
    }

    public void Stop()
    {
        _currentState = null;
        foreach (var state in dic_states.Values)
        {
            state.UnInit();
        }
        dic_states.Clear();
    }
}
