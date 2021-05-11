using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack01 : IState{

    private readonly Animator anim;
    private readonly int attack01Trg = Animator.StringToHash("Melee Right Attack 01");

    public PlayerStateAttack01(Animator anim) {
        this.anim = anim;
    }

    public void Tick() {

    }

    public void OnEnter() {
        anim.SetTrigger(attack01Trg);
    }
    public void OnExit() {
    }
}