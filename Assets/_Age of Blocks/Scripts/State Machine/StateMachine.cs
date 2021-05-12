using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class StateMachine
{
    private IState currentState;
    private Dictionary<Type, List<Transition>> transitionsDic = new Dictionary<Type, List<Transition>>();
    private List<Transition> currentTransitionsList = new List<Transition>();
    private List<Transition> anyTransitionsList = new List<Transition>();
    private static List<Transition> emptyTransitionsList = new List<Transition>(0);

    public void Tick() {
        var transition = GetTransition(); //do we have any state to transit to?
        if(transition != null) {
            SetState(transition.To);
        }

        currentState?.Tick();
    }

    public void SetState(IState newState) {
        if(newState == currentState) {
            return;
        }

        currentState?.OnExit();
        currentState = newState;
        
        transitionsDic.TryGetValue(currentState.GetType(), out currentTransitionsList); //here we are getting the list of transitions for the newState, to use it later
        if (currentTransitionsList == null) {
            currentTransitionsList = emptyTransitionsList; //so we do not reallocate memory on every state transition
        }

        currentState.OnEnter();
    }

    public void AddTransitionToDic(IState from, IState to, Func<bool> predicate) {

        if(transitionsDic.TryGetValue(from.GetType(), out var transitions) == false) {
            //our dic still don't transitions for this type
            transitions = new List<Transition>();
            transitionsDic[from.GetType()] = transitions;
        }

        transitions.Add(new Transition(to, predicate));
    }
    public void AddAnyTransitionToDic(IState state, Func<bool> predicate) {
        anyTransitionsList.Add(new Transition(state, predicate));
    }



    private class Transition {

        public Func<bool> Condition { get; } //if this condition is met, we transit to "To"
        public IState To { get; }

        //Constructor
        public Transition(IState to, Func<bool> condition) {
            To = to;
            Condition = condition;
        }
    }

    private Transition GetTransition() {
        //first we check if we have "anyTransitions", since they will always be ran first
        foreach (var transition in anyTransitionsList) {
            if (transition.Condition()) { //if the condition is met, e.g. speed > 0.1 or whatever
                return transition;
            }
        }

        //if we don't have any urgent/any transitions, return the first transition from our list that meets its condition
        foreach (var transition in currentTransitionsList) {
            if (transition.Condition()) { //if the condition is met, e.g. speed > 0.1 or whatever
                return transition;
            }
        }

        return null;
    }

    public IState GetCurrentStateForDebugOnly() {
        return currentState;
    }
}


