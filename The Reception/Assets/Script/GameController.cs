using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class GameController : MonoBehaviour
{
    public LostAndFoundController LostAndFoundController;
    public Transform[] LostFoundPlacementIndex = new Transform[4];
    //
    // private void Start()
    // {
    //     Debug.Log("Game Controller");
    //     UpdatePlacement();
    // }
    //
    // public void UpdatePlacement()
    // {
    //     LostFoundPlacementIndex = LostAndFoundController.GetSlotTransform();
    //     var rng = new Random();
    //     rng.Shuffle(LostFoundPlacementIndex);
    //     rng.Shuffle(LostFoundPlacementIndex); // different order from first call to Shuffle
    // } 
}

internal static class RandomExtensions
{
    public static void Shuffle<T> (this Random rng, T[] array)
    {
        var n = array.Length;
        while (n > 1) 
        {
            var k = rng.Next(n--);
            (array[n], array[k]) = (array[k], array[n]);
        }
    }
}