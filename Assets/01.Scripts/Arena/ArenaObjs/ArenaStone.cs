using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arena
{
    public class ArenaStone : MonoBehaviour, IObserble
    {
        public List<Observer> Observers { get; }
    }
    
}
