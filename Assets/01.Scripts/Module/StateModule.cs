using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public enum State
    {
        MOVING,
        JUMP,
        CHARGE,
        ATTACK,
        SKILL,
        HIT,
        DEAD,
        UI,
        NONE
    }

    public class StateModule : AbBaseModule
    {
        public List<State> CurrentState
        {
            get
            {
                return currentStates;
            }
            set
            {
                currentStates = value;
            }
        }



        private List<State> currentStates = new List<State>();


        public StateModule(AbMainModule _mainModule) : base(_mainModule) { }

        public StateModule() : base() { }



        public bool CheckState(State _state)
        {
            return CurrentState.Contains(_state);
        }
        public bool CheckState(State _state1, State _state2)
        {
            return CurrentState.Contains(_state1) || CurrentState.Contains(_state2);
        }
        public bool CheckState(State _state1, State _state2, State _state3)
        {
            return CurrentState.Contains(_state1) || CurrentState.Contains(_state2) || CurrentState.Contains(_state3);
        }
        public void AddState(State _state)
        {
            if (CurrentState.Contains(_state))
                return;

            CurrentState.Add(_state);
        }
        public void RemoveState(State _state)
        {
            if (!CurrentState.Contains(_state))
                return;
            CurrentState.Remove(_state);
        }

        public void RemoveTypeState(State _state)
        {
            if (!CurrentState.Contains(_state))
                return;

            foreach (State state in CurrentState)
            {
                if (state == _state)
                {
                    CurrentState.Remove(state);
                    if (!CurrentState.Contains(_state))
                        return;
                }
            }
        }
    }
}