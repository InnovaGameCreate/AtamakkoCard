using UnityEngine;
using UnityEngine.UI;
using Arena;

public class EnemyCharacterImage : MonoBehaviour
{
    [SerializeField] private Image CharacterImage;
    void Start()
    {
        SetImage();
    }
    private void SetImage()
    {
        EnemySprite CharacterSprite = Resources.Load<EnemySprite>("EnemySprite");
        CharacterImage.sprite = CharacterSprite.CharacterImageList[enemyDeckData._enemyID];
    }
}
