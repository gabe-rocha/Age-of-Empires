using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateAttack03 : IState {

    private readonly Animator anim;
    private readonly int attack03Trg = Animator.StringToHash("Melee Right Attack 03");

    public PlayerStateAttack03(Animator anim) {
        this.anim = anim;
    }

    public void Tick() {

    }

    public void OnEnter() {
        anim.SetTrigger(attack03Trg);
    }

    public void OnExit() {
    }
}