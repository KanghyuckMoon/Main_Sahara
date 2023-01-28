using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForTheTest
{
    [CreateAssetMenu(menuName = "SO/PlayerData_TestSO")]
    public class PlayerData_TesSO : ScriptableObject
    {
        public float speed;
        public float jumpPower;
        public int hp;
    }
}