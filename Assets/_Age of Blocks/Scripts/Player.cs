using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(CapsuleCollider), typeof(BoxCollider))]
public class Player : MonoBehaviour {
    [Header("NOTE: Model must be facing FORWARD (Z)")]
    
    [Tooltip("If checked, player will walk before running")]
    [SerializeField] private bool handleWalk = false;

    public float maxSpeed = 5f;
    public float fallSpeed = 5f;
    public TextMeshProUGUI textState, textIsGrounded;
    [HideInInspector] public bool isGrounded = true;
    
    [SerializeField] private float runSpeedStartsAt = 2f;
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private float movementDeadzone = 0.1f;
    [SerializeField] private Animator anim;
    [SerializeField] private LayerMask groundLayer;

    private PlayerStateIdle idle;
    private PlayerStateRun run;
    private PlayerStateWalk walk;
    private PlayerStateAttack01 attack01;
    private PlayerStateAttack02 attack02;
    private PlayerStateAttack03 attack03;

    private StateMachine stateMachine;
    private float horizontal, vertical;
    private BoxCollider feetCollider;

    private void Awake() {

        stateMachine = new StateMachine();

        //these are our states
        idle = new PlayerStateIdle(this);
        run = new PlayerStateRun(this, anim, maxSpeed, rotationSpeed);
        walk = new PlayerStateWalk(this, anim, maxSpeed, rotationSpeed);
        attack01 = new PlayerStateAttack01(this, anim);
        attack02 = new PlayerStateAttack02(this, anim);
        attack03 = new PlayerStateAttack03(anim);

        feetCollider = GetComponent<BoxCollider>();

        //AddTransition(idle, walk, CanWalk());
        AddTransition(idle, run, CanRun());
        AddTransition(idle, attack01, CanAttack01());

        AddTransition(attack01, idle, IsIdle());
        //AddTransition(attack01, walk, CanWalk());
        AddTransition(attack01, run, CanRun());

        //AddTransition(walk, run, CanRun());
        //AddTransition(walk, idle, IsIdle());
        //AddTransition(walk, attack01, CanAttack01());

        //AddTransition(run, walk, CanWalk());
        AddTransition(run, idle, IsIdle());
        AddTransition(run, attack01, CanAttack01());

        //helper funcs
        void AddTransition(IState from, IState to, Func<bool> condition) => stateMachine.AddTransitionToDic(from, to, condition);
        Func<bool> CanRun() => () => CheckCanRun();
        Func<bool> CanWalk() => () => CheckIsWalking();
        Func<bool> IsIdle() => () => CheckIsIdle();
        Func<bool> CanAttack01() => () => CheckIsAttacking01();

        stateMachine.SetState(idle);
    }

    private void Update() {
        textState.text = stateMachine.GetCurrentStateForDebugOnly().ToString();
        textState.transform.rotation = Quaternion.identity;
        textIsGrounded.text = isGrounded ? "Grounded" : "Not Grounded";
        textIsGrounded.transform.rotation = Quaternion.identity;

        stateMachine.Tick();
    }

    private bool CheckIsIdle() {
        if (attack01.isActive) {
            return false;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        return (Mathf.Abs(horizontal) < movementDeadzone && Mathf.Abs(vertical) < movementDeadzone);
        }

    private bool CheckIsWalking() {
        if(!handleWalk) {
            return false;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal) > movementDeadzone || Mathf.Abs(vertical) > movementDeadzone) {            
            var highestSpeedHV = Mathf.Abs(horizontal) >= Mathf.Abs(vertical) ? Mathf.Abs(horizontal) : Mathf.Abs(vertical);
            return maxSpeed * highestSpeedHV < runSpeedStartsAt;
        }
        else {
            return false;
        }
    }
    private bool CheckCanRun() {
        if(attack01.isActive) {
            return false;
        }

        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal) > movementDeadzone || Mathf.Abs(vertical) > movementDeadzone) {
            var highestSpeedHV = Mathf.Abs(horizontal) >= Mathf.Abs(vertical) ? Mathf.Abs(horizontal) : Mathf.Abs(vertical);
            return maxSpeed * highestSpeedHV >= runSpeedStartsAt;
        }
        else {
            return false;
        }
    }

    private bool CheckIsAttacking01() {
        return Input.GetButtonDown("Fire1");
    }

    private void OnTriggerEnter(Collider collider) {
        isGrounded = (groundLayer & 1 << collider.gameObject.layer) == 1 << collider.gameObject.layer;
    }
}
