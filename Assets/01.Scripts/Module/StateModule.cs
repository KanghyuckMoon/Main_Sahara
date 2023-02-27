using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Module
{
    public enum State
    {
        MOVING,
        JUMP,
        ATTACK,
        HIT,
        DEAD,
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



        private List<State> currentStates;


        public StateModule(AbMainModule _mainModule) : base(_mainModule) { }


        public bool CheckState(State _state)
        {
            return CurrentState.Contains(_state);
        }
        public bool CheckState(State _state1, State _state2)
        {
            return CurrentState.Contains(_state1) || CurrentState.Contains(_state2);
        }
        public void AddState(State _state)
        {
            if (CurrentState.Contains(_state))
                return;

            CurrentState.Add(_state);
        }
    }
}