#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

namespace Spawner
{
    [CustomEditor(typeof(RandomMonsterSpawner))]
    public class RandomMonsterSpawnerEditor : Editor
    {
        static RandomMonsterSpawner _randomMonsterSpawner = null;

        void OnEnable()
        {
			_randomMonsterSpawner = (target as RandomMonsterSpawner);
		}

        private void OnSceneGUI()
		{
			if (_randomMonsterSpawner is null)
			{
				return;
			}

			Handles.color = Color.red;
			Handles.DrawWireDisc(_randomMonsterSpawner.transform.position, Vector3.up, _randomMonsterSpawner.radius, 3);

			float value = Handles.ScaleSlider(
				_randomMonsterSpawner.radius,
				_randomMonsterSpawner.transform.position,
				Vector3.up,
				Quaternion.identity,
				_randomMonsterSpawner.radius,
				0.001f
			);

			if (value < 0 || float.IsNaN(value))
			{
				value = 0;
			}

			_randomMonsterSpawner.radius = value;
		}
    }

}
#endif