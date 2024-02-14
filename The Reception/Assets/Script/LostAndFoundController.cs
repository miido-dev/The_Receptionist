using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LostAndFoundController : MonoBehaviour
{
    [SerializeField] private Transform[] _slotsTransform;
    [SerializeField] private Transform[] _itemTransform;
    [SerializeField] private GameObject[] _itemStatus;
    [SerializeField] private Transform[] _itemPlaceArea;
    [SerializeField] private GameObject _submitButton;

    private List<int> currentItemIndexList;

    private void OnEnable()
    {
        _submitButton.SetActive(false);
        currentItemIndexList = GenerateRandomNumbers(_itemTransform.Length, 0, _itemTransform.Length);
        var slotPlacement = EventManager.GetLostFoundItemPlacementListTrigger.Invoke();
        for (int i = 0; i < _itemTransform.Length; i++)
        {
            _itemTransform[currentItemIndexList[i]].parent = _slotsTransform[i];
            _itemTransform[currentItemIndexList[i]].localPosition = Vector3.zero;
            _itemTransform[currentItemIndexList[i]].GetComponent<Dragable>().Id = slotPlacement[i];

            _itemTransform[currentItemIndexList[i]].Find("Right").gameObject.SetActive(false);
            _itemTransform[currentItemIndexList[i]].Find("Wrong").gameObject.SetActive(false);
        }

        EventManager.ItemsPlacedCheckTrigger += ItemPlacementChildCheck;
    }

    private void Start()
    {
        _itemStatus = new GameObject[4];
    }

    public static List<int> GenerateRandomNumbers(int count, int minValue, int maxValue)
    {
        List<int> possibleNumbers = new List<int>();
        List<int> chosenNumbers = new List<int>();
 
        for (int index = minValue; index < maxValue; index++)
            possibleNumbers.Add(index);
       
        while (chosenNumbers.Count < count)
        {
            int position = Random.Range(0, possibleNumbers.Count);
            chosenNumbers.Add(possibleNumbers[position]);
            possibleNumbers.RemoveAt(position);
        }
        return chosenNumbers;
    }

    public Transform[] GetSlotTransform()
    {
        return _slotsTransform;
    }

    public void Submit()
    {
        foreach (var status in _itemStatus)
        {
            status.SetActive(true);
        }
    }

    public void ItemPlacementChildCheck()
    {
        for (int i = 0; i < _itemPlaceArea.Length; i++)
        {
            Debug.Log($"{i} {_itemPlaceArea[i].childCount}");
         
            if (_itemPlaceArea[i].childCount == 1)
            {
                _itemStatus[i] = _itemTransform[currentItemIndexList[i]].GetComponent<Dragable>().CheckCorrectPlacement();
                _submitButton.SetActive(false);

                //Show Submit Button
                if (i >= _itemPlaceArea.Length - 1)
                {
                    _submitButton.SetActive(true);
                }
            }
            else
            {
                _submitButton.SetActive(false);
                return;
            }
        }
    }

}

// public static List<int> GenerateRandom(int count, int min, int max)
// {
//     if (max <= min || count < 0 ||
//         (count > max - min && max - min > 0))
//     {
//         throw new ArgumentOutOfRangeException("Range " + min + " to " + max +
//                                               " (" + ((Int64)max - (Int64)min) + " values), or count " + count + " is illegal");
//     }
//  
//     HashSet<int> candidates = new HashSet<int>();
//  
//     for (int top = max - count; top < max; top++)
//     {
//         if (!candidates.Add(random.Next(min, top + 1)))
//         {
//             candidates.Add(top);
//         }
//     }
//  
//     List<int> result = candidates.ToList();
//  
//     for (int i = result.Count - 1; i > 0; i--)
//     {
//         int k = random.Next(i + 1);
//         (result[k], result[i]) = (result[i], result[k]);
//     }
//     return result;
// }


