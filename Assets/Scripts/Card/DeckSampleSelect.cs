using UnityEngine;
using UnityEngine.UI;
using Card;

public class DeckSampleSelect : MonoBehaviour
{
    [SerializeField]
    Deck[] decks;
    [SerializeField]
    private Image[] ButtonImage;

    private void Start()
    {
        ButtonImage[0].color = Color.gray;//�I�𒆂̃f�b�L�̃{�^���̐F���D�F�ɂ���B
        PlayerConfig.Deck = decks[0].cardIDList;//�����̃f�b�L�ɑI�������f�b�L��o�^
    }
    public void SelectDeck(int i)
    {
        PlayerConfig.Deck = decks[i].cardIDList;//�����̃f�b�L�ɑI�������f�b�L��o�^
        foreach (var item in ButtonImage)
        {
            item.color = Color.white;
        }
        ButtonImage[i].color = Color.gray;//�I�𒆂̃f�b�L�̃{�^���̐F���D�F�ɂ���B
        PlayerConfig.Equipmnet.Clear();
        switch (i)
        {
            case 0:
                PlayerConfig.Equipmnet.Add(1);
                PlayerConfig.Equipmnet.Add(0);
                PlayerConfig.Equipmnet.Add(2);
                PlayerConfig.Equipmnet.Add(48);
                PlayerConfig.Equipmnet.Add(49);
                PlayerConfig.Equipmnet.Add(50);
                break;
            case 1:
                PlayerConfig.Equipmnet.Add(19);
                PlayerConfig.Equipmnet.Add(18);
                PlayerConfig.Equipmnet.Add(20);
                PlayerConfig.Equipmnet.Add(57);
                PlayerConfig.Equipmnet.Add(60);
                PlayerConfig.Equipmnet.Add(61);
                break;
            case 2:
                PlayerConfig.Equipmnet.Add(46);
                PlayerConfig.Equipmnet.Add(45);
                PlayerConfig.Equipmnet.Add(47);
                PlayerConfig.Equipmnet.Add(65);
                PlayerConfig.Equipmnet.Add(66);
                PlayerConfig.Equipmnet.Add(67);
                break;
            default:
                break;
        }
    }
}
