using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create EnemySprite", fileName = "EnemySprite")]
public class EnemySprite : ScriptableObject
{
    public List<Sprite> CharacterImageList;
}
