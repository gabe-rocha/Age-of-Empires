using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TopDown3DCharacterOn2DWorldControllerVector))]
public class Player : MonoBehaviour {
    [Header("NOTE: Model must be facing RIGHT")]
    [Tooltip("If checked, player will walk before running")]
    [SerializeField] bool handleWalk = false;

    [SerializeField] float runSpeedStartsAt = 2f;
    [SerializeField] float maxSpeed = 5f;
    [SerializeField] float rotationSpeed = 360f;
    [SerializeField] private float movementDeadzone = 0.1f;

    [SerializeField]private Animator anim;

    private StateMachine stateMachine;
    private float horizontal, vertical;
    private bool isRunning = false, isWalking = false;
    private TopDown3DCharacterOn2DWorldControllerVector inputController;
    
    private void Awake() {
        inputController = GetComponent<TopDown3DCharacterOn2DWorldControllerVector>();

        stateMachine = new StateMachine();

        //these are our states
        var idle = new PlayerStateIdle();
        var run = new PlayerStateRun(this, anim, maxSpeed);
        var walk = new PlayerStateWalk(this, anim, maxSpeed);
        var attack01 = new PlayerStateAttack01(anim);
        var attack02 = new PlayerStateAttack02(anim);
        var attack03 = new PlayerStateAttack03(anim);

        AddTransition(idle, walk, IsWalking());
        AddTransition(idle, run, IsRunning());

        AddTransition(walk, run, IsRunning());
        AddTransition(walk, idle, IsIdle());

        AddTransition(run, walk, IsWalking());
        AddTransition(run, idle, IsIdle());

        //helper funcs
        void AddTransition(IState from, IState to, Func<bool> condition) => stateMachine.AddTransitionToDic(from, to, condition);
        Func<bool> IsRunning() => () => CheckIsRunning();
        Func<bool> IsWalking() => () => CheckIsWalking();
        Func<bool> IsIdle() => () => !isRunning && !isWalking;

        stateMachine.SetState(idle);
    }

    private void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        stateMachine.Tick();
    }

    private bool CheckIsWalking() {
        if(!handleWalk) {
            return false;
        }

        if (Mathf.Abs(horizontal) > movementDeadzone || Mathf.Abs(vertical) > movementDeadzone) {            
            var highestSpeedHV = Mathf.Abs(horizontal) >= Mathf.Abs(vertical) ? Mathf.Abs(horizontal) : Mathf.Abs(vertical);
            return maxSpeed * highestSpeedHV < runSpeedStartsAt;
        }
        else {
            return false;
        }
    }

    private bool CheckIsRunning() {
        if (Mathf.Abs(horizontal) > movementDeadzone || Mathf.Abs(vertical) > movementDeadzone) {
            var highestSpeedHV = Mathf.Abs(horizontal) >= Mathf.Abs(vertical) ? Mathf.Abs(horizontal) : Mathf.Abs(vertical);
            return maxSpeed * highestSpeedHV >= runSpeedStartsAt;
        }
        else {
            return false;
        }
    }
}
