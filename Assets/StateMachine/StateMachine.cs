using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum StateType
{
    Idle,
    Run,
    Jump,
}

public abstract class State
{
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate(float deltaTime);
}
public class IdleState : State
{
    public override void OnEnter()
    {
        Debug.Log("Enter : Idle");
    }
    public override void OnExit()
    {
        Debug.Log("Exit : Idle");
    }
    public override void OnUpdate(float deltaTime)
    {
        Debug.Log("Update : Idle");
    }
}
public class RunState : State
{
    public override void OnEnter()
    {
        Debug.Log("Enter : Run");
    }
    public override void OnExit()
    {
        Debug.Log("Exit : Run");
    }
    public override void OnUpdate(float deltaTime)
    {
        Debug.Log("Update : Run");
    }
}
public class JumpState : State
{
    public override void OnEnter()
    {
        Debug.Log("Enter : Jump");
    }
    public override void OnExit()
    {
        Debug.Log("Exit : Jump");
    }
    public override void OnUpdate(float deltaTime)
    {
        Debug.Log("Update : Jump");
    }
}

public class Transition
{
    public StateType To { get; set; }
    public TriggerType Trigger { get; set; }
}

public enum TriggerType
{
    KeyDownI,
    KeyDownJ,
    KeyDownR,
}

public class StateMachine
{
    private StateType _stateType;
    private State _state;

    private Dictionary<StateType, State> _stateTypes = new Dictionary<StateType, State>();
    private Dictionary<StateType, List<Transition>> _transitionLists = new Dictionary<StateType, List<Transition>>();

    public StateMachine(StateType initialState)
    {
        _stateTypes.Add(StateType.Idle, new IdleState());
        _stateTypes.Add(StateType.Run, new RunState());
        _stateTypes.Add(StateType.Jump, new JumpState());

        // Å‰‚ÌState‚É‘JˆÚ
        ChangeState(initialState);
    }

    /// <summary>
    /// ƒgƒŠƒK[‚ğÀs‚·‚é
    /// </summary>
    public void ExecuteTrigger(TriggerType trigger)
    {
        var transitions = _transitionLists[_stateType];
        foreach (var transition in transitions)
        {
            if (transition.Trigger == trigger)
            {
                ChangeState(transition.To);
                break;
            }
        }
    }

    /// <summary>
    /// ‘JˆÚî•ñ‚ğ“o˜^‚·‚é
    /// </summary>
    public void AddTransition(StateType from, StateType to, TriggerType trigger)
    {
        if (!_transitionLists.ContainsKey(from))
        {
            _transitionLists.Add(from, new List<Transition>());
        }
        var transitions = _transitionLists[from];
        var transition = transitions.FirstOrDefault(x => x.To == to);
        if (transition == null)
        {
            // V‹K“o˜^
            transitions.Add(new Transition { To = to, Trigger = trigger });
        }
        else
        {
            // XV
            transition.To = to;
            transition.Trigger = trigger;
        }
    }

    /// <summary>
    /// XV‚·‚é
    /// </summary>
    public void Update(float deltaTime)
    {
        _state.OnUpdate(deltaTime);
    }

    /// <summary>
    /// State‚ğ’¼Ú•ÏX‚·‚é
    /// </summary>
    private void ChangeState(StateType stateType)
    {
        if (_state != null)
        {
            _state.OnExit();
        }

        _stateType = stateType;
        _state = _stateTypes[_stateType];
        _state.OnEnter();
    }
}