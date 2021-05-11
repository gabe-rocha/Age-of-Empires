using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAttack : MonoBehaviour
{
    //Animation Hashes
    private int meleeRightAttack01Trg = Animator.StringToHash("Melee Right Attack 01");
    private int meleeRightAttack02Trg = Animator.StringToHash("Melee Right Attack 02");
    private int meleeRightAttack03Trg = Animator.StringToHash("Melee Right Attack 03");
    
    private int meleeLeftAttack01Trg = Animator.StringToHash("Melee Left Attack 01");
    
    private int jumpRightAttack01Trg = Animator.StringToHash("Jump Right Attack 01");
    
    private int rightPunchAttackTrg = Animator.StringToHash("Right Punch Attack");
    private int leftPunchAttackTrg = Animator.StringToHash("Left Punch Attack");
    
    private int spinAttackTrg = Animator.StringToHash("Spin Attack");

    //
    private Animator anim;
    

    private void OnEnable() {
        EventManager.PlayerEvents.OnPlayerPressedAttack01 += OnPlayerAttack01;
    }

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    private void OnPlayerAttack01() {
        anim.SetTrigger(meleeRightAttack01Trg);
    }
}
