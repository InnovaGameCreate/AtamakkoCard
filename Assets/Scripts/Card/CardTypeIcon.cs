using UnityEngine;
using UnityEngine.UI;

public class CardTypeIcon : MonoBehaviour
{
    [SerializeField]
    private Sprite MoveIcon;
    [SerializeField]
    private Sprite AttackIcon;
    [SerializeField]
    private Image thisImage;


    public void setImage(string type)//送られてくるカードのタイプのアイコンを設定する。
    {
        switch (type)
        {
            case "攻撃":
                thisImage.sprite = AttackIcon;
                break;
            case "移動":
                thisImage.sprite = MoveIcon;
                break;
            default:
                Debug.LogError("設定されていないタイプのカードです");
                break;
        }
    }
}
