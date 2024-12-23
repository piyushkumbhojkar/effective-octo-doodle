using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Fruits Data", order = 1)]
public class FruitsData : ScriptableObject
{
    public List<Fruit> fruits = new();
}

[System.Serializable]
public class Fruit
{
    public FruitType FruitType;
    public Sprite FruitSprite;
}
