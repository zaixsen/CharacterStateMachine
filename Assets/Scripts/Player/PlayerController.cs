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
    [HideInInspector] public PlayerState lastState;
    [HideInInspector] public Animator animator;
    [SerializeField, Header("移动速度"), Range(1, 10)] public float moveSpeed;
    [SerializeField, Header("旋转速度"), Range(1, 100)] public float rotationSpeed;

    private int bulletCount;

    List<BreakStateConfig> breakStateConfigs;

    private void Start()
    {
        LoadData();
        animator = GetComponentInChildren<Animator>();
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
            case PlayerState.Walk:
                stateMachine.EnterState<PlayerWalkState>();
                break;
            case PlayerState.TurnBack:
                stateMachine.EnterState<PlayerTurnBackState>();
                break;
            default:
                break;
        }

        lastState = currentState;
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

    public void PlayerRotate(float angle)
    {
        transform.Rotate(Vector3.up * angle);
    }

    public void PlayerMove(float motion)
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * motion);
    }
}
