using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : IState {
    public PlayerStateIdle() { 
    }

    public void Tick() {

    }
    public void OnEnter() {
        //No need to do anything since Idle is our default state
    }

    public void OnExit() {
    }
}