using Assemble;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UI;
using System.Threading.Tasks;
using Card;
using DG.Tweening;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private GameObject InstantiatePositon;
    [SerializeField] private GameObject EquipmentPrefab;
    [SerializeField] private GameObject TypeOnlyBottons;
    [SerializeField] private GameObject NormalBottons;
    [SerializeField] private ButtonController Gacha1;
    [SerializeField] private ButtonController Gacha10;
    [SerializeField] private ButtonController UpperType;
    [SerializeField] private ButtonController MidType;
    [SerializeField] private ButtonController BottomType;
    [SerializeField] private ButtonController AccesoryType;
    [SerializeField] private ButtonController normal;
    [SerializeField] private ButtonController TypeOnly;
    private bool isNormal = true;
    private int ListCount;

    private enum Type
    {
        Upper,
        Mid,
        Bottom,
        Accesory,
    }
    private Type GachaType;//部位限定ガチャの指定部位

    private void Start()
    {
        GachaType = Type.Mid;
        BottonColorGray();
        MidType.gameObject.GetComponent<Image>().color = Color.white;
        TypeOnlyBottons.SetActive(false);
        ListCount = equipmentData.CardDataArrayList.Count;

        TypeOnly.Pushed.Subscribe(_ =>//TypeOnlyのbuttonが押された場合
        {
            TypeOnlyBottons.SetActive(true);
            NormalBottons.SetActive(false);
            isNormal = false;
        }).AddTo(this);

        normal.Pushed.Subscribe(_ =>//normalのbuttonが押された場合
        {
            TypeOnlyBottons.SetActive(false);
            NormalBottons.SetActive(true);
            isNormal = true;
        }).AddTo(this);

        Gacha1.Pushed.Subscribe(_ =>//Gacha1のbuttonが押された場合
        {
                if (isNormal) normalGacha(1);
                else typeOnlyGacha(1);
        }).AddTo(this);

        Gacha10.Pushed.Subscribe(_ =>//Gacha10のbuttonが押された場合
        {
            if (isNormal) normalGacha(10);
            else typeOnlyGacha(10);
        }).AddTo(this);

        UpperType.Pushed.Subscribe(_ =>//UpperTypeのbuttonが押された場合
        {
            GachaType = Type.Upper;
            BottonColorGray();
            UpperType.gameObject.GetComponent<Image>().color = Color.white;
        }).AddTo(this);

        MidType.Pushed.Subscribe(_ =>//MidTypeのbuttonが押された場合
        {
            GachaType = Type.Mid;
            BottonColorGray();
            MidType.gameObject.GetComponent<Image>().color = Color.white;
        }).AddTo(this);

        BottomType.Pushed.Subscribe(_ =>//BottomTypeのbuttonが押された場合
        {
            GachaType = Type.Bottom;
            BottonColorGray();
            BottomType.gameObject.GetComponent<Image>().color = Color.white;
        }).AddTo(this);

        AccesoryType.Pushed.Subscribe(_ =>//AccesoryTypeのbuttonが押された場合
        {
            GachaType = Type.Accesory;
            BottonColorGray();
            AccesoryType.gameObject.GetComponent<Image>().color = Color.white;
        }).AddTo(this);
    }



    public async void normalGacha(int count)
    {
        Interactable(false);
        for (int i = 0; i < count; i++)
        {
            var Equpment = Instantiate(EquipmentPrefab, InstantiatePositon.transform);
            var IntantiateInt = Random.Range(0, ListCount);
            var quipmentData = new equipmentModel(equipmentData.CardDataArrayList[IntantiateInt]);
            Equpment.GetComponent<equipmentView>().Show(quipmentData);
            PlayerConfig.unLockEquipment[quipmentData.ID] = true;
            cardAnimation(Equpment);
            Destroy(Equpment, 6);
            await Task.Delay(100);
        }
        await Task.Delay(6500);
        Interactable(true);
    }
    public async void typeOnlyGacha(int count)
    {
        Interactable(false);
        for (int i = 0; i < count; i++)
        {

            var Equpment = Instantiate(EquipmentPrefab, InstantiatePositon.transform);
            var Viewer = Equpment.GetComponent<equipmentView>();
            int IntantiateInt;
            var quipmentData = new equipmentModel(equipmentData.CardDataArrayList[0]);
            while (true)
            {
                IntantiateInt = Random.Range(0, ListCount);
                quipmentData = new equipmentModel(equipmentData.CardDataArrayList[IntantiateInt]);
                if (quipmentData.Position == position()) break;
            }
            PlayerConfig.unLockEquipment[quipmentData.ID] = true;
            Viewer.Show(quipmentData);
            cardAnimation(Equpment);
            Destroy(Equpment, 6);
            await Task.Delay(100);
        }
        await Task.Delay(6500);
        Interactable(true);
    }
    private string position()
    {
        switch (GachaType)
        {
            case Type.Upper:
                return "上部";
            case Type.Mid:
                return "中央";
            case Type.Bottom:
                return "下部";
            case Type.Accesory:
                return "アクセサリ";
            default:
                return "その他";
        }
    }

    private void BottonColorGray()
    {
        UpperType.gameObject.GetComponent<Image>().color = Color.gray;
        MidType.gameObject.GetComponent<Image>().color = Color.gray;
        BottomType.gameObject.GetComponent<Image>().color = Color.gray;
        AccesoryType.gameObject.GetComponent<Image>().color = Color.gray;
    }
    private void Interactable(bool value)
    {
        Gacha10.MyInteractable = value;
        Gacha1.MyInteractable = value;
    }

    private async void cardAnimation(GameObject card)
    {
        var Viewer = card.GetComponent<equipmentView>();
        Viewer.backCard.SetActive(true);
        card.transform.DORotate(new Vector3(0, -180, 0), 0, RotateMode.FastBeyond360);
        await Task.Delay(1100);
        card.transform.DORotate(new Vector3(0, 0, 0), 2, RotateMode.FastBeyond360).SetEase(Ease.InOutCirc);
        await Task.Delay(1000);
        Viewer.backCard.SetActive(false);
    }
}
