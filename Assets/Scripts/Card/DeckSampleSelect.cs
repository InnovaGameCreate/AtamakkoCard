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
    }
}
