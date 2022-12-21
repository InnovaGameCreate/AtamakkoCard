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
        ButtonImage[0].color = Color.gray;//選択中のデッキのボタンの色を灰色にする。
        PlayerConfig.Deck = decks[0].cardIDList;//自分のデッキに選択したデッキを登録
    }
    public void SelectDeck(int i)
    {
        PlayerConfig.Deck = decks[i].cardIDList;//自分のデッキに選択したデッキを登録
        foreach (var item in ButtonImage)
        {
            item.color = Color.white;
        }
        ButtonImage[i].color = Color.gray;//選択中のデッキのボタンの色を灰色にする。
    }
}
