using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Utill.Pattern;

namespace UpdateManager
{
    public interface IUpdateObj
	{
        public void UpdateManager_Update();
        public void UpdateManager_FixedUpdate();
        public void UpdateManager_LateUpdate();

    }

	/// <summary>
	/// Updates the list of <see cref="ManagedMover"/> types manually
	/// </summary>
	public class UpdateManager : MonoSingleton<UpdateManager>
	{
        public static Stopwatch SW { get; private set; } = new Stopwatch();
        public static Action StopWatchStoppedCallback;

        static HashSet<IUpdateObj> _updateables = new HashSet<IUpdateObj>();

        /// <summary>
        /// Adds a <see cref="Mover"/> to the list of updateables
        /// </summary>
        /// <param name="obj">The object that will be added</param>
        public static void Add(IUpdateObj mover)
        {
            if(!_updateables.Add(mover))
            {
                Debug.LogError("중복");
            }                
        }

        /// <summary>
        /// Removes a <see cref="Mover"/> from the list of updateables
        /// </summary>
        /// <param name="obj">The object that will be removed</param>
        public static void Remove(IUpdateObj mover)
        {
            if (!_updateables.Contains(mover))
            {
                Debug.LogError("없음");
            }
            _updateables.Remove(mover);
        }

        public static void Clear()
		{
            _updateables.Clear();

        }

        void Update()
        {
            SW.Restart();
            try
            {
                foreach (var mover in _updateables)
                {
                    try
                    {
                        mover.UpdateManager_Update();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch
            {

            }
            SW.Stop();
            StopWatchStoppedCallback?.Invoke();
        }
        void FixedUpdate()
        {
            SW.Restart();
            try
            {
                foreach (var mover in _updateables)
                {
                    try
                    {
                        mover.UpdateManager_FixedUpdate();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch
            {

            }
            SW.Stop();
            StopWatchStoppedCallback?.Invoke();
        }
        void LateUpdate()
        {
            SW.Restart();
            try
            {
                foreach (var mover in _updateables)
                {
                    try
                    {
                        mover.UpdateManager_LateUpdate();
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            catch
            {

            }
            SW.Stop();
            StopWatchStoppedCallback?.Invoke();
        }
        //#region Static constructor and field for inner MonoBehaviour
        //
        //static UpdateManager()
        //{
        //    var gameObject = new GameObject();
        //    _innerMonoBehaviour = gameObject.AddComponent<UpdateManagerInnerMonoBehaviour>();
        //#if UNITY_EDITOR
        //gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
        //_innerMonoBehaviour.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
        //#endif
        //}
        //static UpdateManagerInnerMonoBehaviour _innerMonoBehaviour;
        //
        //#endregion
    }
}
