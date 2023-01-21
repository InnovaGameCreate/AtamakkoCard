using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankerImage : MonoBehaviour
{
    [SerializeField] Image CharacterImage;

    public void Init(int Num)
    {
        EnemySprite enemySprite = Resources.Load<EnemySprite>("EnemySprite");
        CharacterImage.sprite = enemySprite.CharacterImageList[Num];

    }
}
