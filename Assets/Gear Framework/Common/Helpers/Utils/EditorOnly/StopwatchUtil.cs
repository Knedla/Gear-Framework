#if UNITY_EDITOR

using System;
using System.Diagnostics;

namespace GearFramework.Common.EditorOnly
{
    public class StopwatchUtil
    {
        public static void MeasureExecutionTime(Action[] actions, string[] debugTexts = null)
        {
            if (debugTexts != null && debugTexts.Length != actions.Length)
                Debug.LogWarning("Number of debug texts must be the same as number of actions or will not be shown", MessageStyle.Warning("Stopwatch Util"));

            Stopwatch stopwatch = new Stopwatch();

            for (int i = 0; i < actions.Length; i++)
            {
                stopwatch.Start();
                actions[i]();
                stopwatch.Stop();

                if (debugTexts == null || debugTexts.Length != actions.Length)
                    Debug.Log($"{stopwatch.ElapsedTicks}", MessageStyle.Done("Stopwatch Util"));
                else
                    Debug.Log($"{stopwatch.ElapsedTicks} - {debugTexts[i]}", MessageStyle.Done("Stopwatch Util"));

                stopwatch.Reset();
            }
        }
    }
}
#endif
