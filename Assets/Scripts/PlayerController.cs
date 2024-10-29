using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

//外部集合总结所有状态

public class BreakStateConfig
{
    public PlayerState playerState;

    public List<PlayerState> allowBreakStates;

    //public List<PlayerState> notAllowBreakStates;
}


public class PlayerController : MonoBehaviour, IStateMachineOwner
{
    StateMachine stateMachine;
    [HideInInspector] public PlayerState currentState;
    [HideInInspector] public PlayerState nextState;
    [HideInInspector] public Animator animator;

    private int bulletCount;

    List<BreakStateConfig> breakStateConfigs;

    private void Start()
    {
        LoadData();
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine(this);
        currentState = PlayerState.Run;
        SwitchState(PlayerState.Idle);
    }

    private void LoadData()
    {
        string data = Resources.Load<TextAsset>("Config/PlayerConfig").text;
        breakStateConfigs = JsonConvert.DeserializeObject<List<BreakStateConfig>>(data);
    }

    public void SwitchState(PlayerState playerState)
    {
        Debug.Log("Change State:" + playerState);
        if (currentState == playerState) return;

        if (!IsAllowChangeState(playerState))
        {
            Debug.Log(playerState + " cannot Breaked " + currentState);
            return;
        }
        Debug.Log(playerState + "allow Breaked" + currentState);

        switch (playerState)
        {
            case PlayerState.Idle:
                stateMachine.EnterState<PlayerIdleState>();
                break;
            case PlayerState.Run:
                stateMachine.EnterState<PlayerRunState>();
                break;
            case PlayerState.Magazine:
                stateMachine.EnterState<PlayerMagazineState>();
                break;
            case PlayerState.Sprint:
                stateMachine.EnterState<PlayerSprintState>();
                break;
            case PlayerState.Fire:
                stateMachine.EnterState<PlayerFireState>();
                break;
            case PlayerState.Collimation:
                stateMachine.EnterState<PlayerCollimationState>();
                break;
            default:
                break;
        }
        currentState = playerState;
    }

    public bool IsAllowChangeState(PlayerState nextState)
    {
        List<PlayerState> stateBreaks = GetStateBreakList(currentState);

        if (stateBreaks == null) return true;

        return stateBreaks.Contains(nextState);
    }
 
    private List<PlayerState> GetStateBreakList(PlayerState state)
    {
        foreach (var breakState in breakStateConfigs)
        {
            if (state == breakState.playerState)
            {
                return breakState.allowBreakStates;
            }
        }
        return null;
    }

    private void Update()
    {
        stateMachine.CurrentState.UpdateState();
    }

    public void FinishBullet()
    {
        Debug.Log("Finish Bullet");
        bulletCount = bulletCount + 1;
    }
}
