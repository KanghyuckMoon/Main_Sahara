using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utill.Random
{
    public static class StaticRandom
    {
        public static int Choose(float[] probs)
        {

            float total = 0;

            foreach (float elem in probs)
            {
                total += elem;
            }

            float randomPoint = UnityEngine.Random.Range(0f, 1f) * total;

            for (int i = 0; i < probs.Length; i++)
            {
                if (randomPoint < probs[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= probs[i];
                }
            }
            return probs.Length - 1;
        }
    }
}
