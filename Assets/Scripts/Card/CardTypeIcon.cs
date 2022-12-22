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


    public void setImage(string type)//�����Ă���J�[�h�̃^�C�v�̃A�C�R����ݒ肷��B
    {
        switch (type)
        {
            case "�U��":
                thisImage.sprite = AttackIcon;
                break;
            case "�ړ�":
                thisImage.sprite = MoveIcon;
                break;
            default:
                Debug.LogError("�ݒ肳��Ă��Ȃ��^�C�v�̃J�[�h�ł�");
                break;
        }
    }
}
