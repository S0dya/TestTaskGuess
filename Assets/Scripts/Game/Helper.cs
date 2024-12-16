using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static void Shuffle<T>(this IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public static int GetRandomNextInt(int max) => Random.Range(0, max);
    public static T GetRandomElement<T>(T[] elements) => elements[GetRandomNextInt(elements.Length)];

}
