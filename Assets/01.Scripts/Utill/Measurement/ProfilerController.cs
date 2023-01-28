using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;
using Unity.Profiling.LowLevel.Unsafe;

namespace Utill.Measurement
{
    public class ProfilerController : MonoBehaviour
    {
        /************************************************************************************************************
        * Source: https://docs.unity3d.com/2020.2/Documentation/ScriptReference/Unity.Profiling.ProfilerRecorder.html
        *************************************************************************************************************/

        //public static ProfilerMarker UpdatePlayerProfilerMarker = new ProfilerMarker("Player.Update");

        [SerializeField]
        private int fontSize = 10;
        [SerializeField]
        private float width = 250;
        [SerializeField]
        private float height = 65;
        [SerializeField]
        private float xPos = 10f;
        [SerializeField]
        private float yPos = 30f;

        private string _statsText;

        private GUIStyle guiStyle = new GUIStyle();
        ProfilerRecorder systemMemoryRecorder;
        ProfilerRecorder gcMemoryRecorder;
        ProfilerRecorder mainThreadTimeRecorder;
        ProfilerRecorder drawCallsCountRecorder;
        ProfilerRecorder vertiecsCountRecorder;
        ProfilerRecorder triCountRecorder;
        ProfilerRecorder setPassCountRecorder;

        private static double GetRecorderFrameAverage(ProfilerRecorder recorder)
        {
            var samplesCount = recorder.Capacity;
            if (samplesCount == 0)
                return 0;

            double r = 0;
            var samples = new List<ProfilerRecorderSample>(samplesCount);
            recorder.CopyTo(samples);
            for (var i = 0; i < samples.Count; ++i)
                r += samples[i].Value;
            r /= samplesCount;

            return r;
        }

        private void OnEnable()
        {
            var availableStatHandles = new List<ProfilerRecorderHandle>();
            ProfilerRecorderHandle.GetAvailable(availableStatHandles);
            for (int i = 0; i < availableStatHandles.Count; ++i)
            {
            }

            systemMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
            gcMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
            mainThreadTimeRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Internal, "Main Thread", 15);
            drawCallsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Draw Calls Count");
            setPassCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "SetPass Calls Count");
            vertiecsCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Vertices Count");
            triCountRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Render, "Triangles Count");

            GetAvailableProfilerStats.EnumerateProfilerStats();
        }

        private void OnDisable()
        {
            systemMemoryRecorder.Dispose();
            gcMemoryRecorder.Dispose();
            mainThreadTimeRecorder.Dispose();
            drawCallsCountRecorder.Dispose();
        }

        private void Update()
        {
            var sb = new StringBuilder(500);
            sb.AppendLine($"Frame Time: {GetRecorderFrameAverage(mainThreadTimeRecorder) * (1e-6f):F1} ms");
            sb.AppendLine($"GC Memory: {gcMemoryRecorder.LastValue / (1024 * 1024)} MB");
            sb.AppendLine($"System Memory: {systemMemoryRecorder.LastValue / (1024 * 1024)} MB");
            sb.AppendLine($"Draw Calls: {drawCallsCountRecorder.LastValue}");
            sb.AppendLine($"SetPass Calls: {setPassCountRecorder.LastValue}");

            if (vertiecsCountRecorder.LastValue > 1000000)
            {
                sb.AppendLine($"Vertices Count: {vertiecsCountRecorder.LastValue / 1000000}M");
            }
            else
            {
                sb.AppendLine($"Vertices Count: {vertiecsCountRecorder.LastValue}");
            }

            if (triCountRecorder.LastValue > 1000000)
            {
                sb.AppendLine($"Tri Count: {triCountRecorder.LastValue / 1000000}M");
            }
            else
            {
                sb.AppendLine($"Tri Count: {triCountRecorder.LastValue}");
            }


            _statsText = sb.ToString();
        }

        private void OnGUI()
        {
            guiStyle.fontSize = fontSize;
            GUI.TextArea(new Rect(xPos, yPos, width, height), _statsText, guiStyle);
        }
    }

}