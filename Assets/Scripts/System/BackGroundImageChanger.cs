using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BackGroundImageChanger : MonoBehaviour
{
    [SerializeField]
    private Sprite[] BackGroundImages;
    [SerializeField]
    private bool RandomSelect;//選ばれている画像からランダムで変更するかどうか
    [SerializeField]
    private float viewTime;//変更されるまでの時間
    [SerializeField]
    private float changeTime;//画像が遷移する時間
    [SerializeField]
    private Image frontImage;
    [SerializeField]
    private Image behindImage;
    private int count = 0;
    void Start()
    {
        behindImage.sprite = BackGroundImages[0];
        if (!RandomSelect) count = 1;
        else count = randomCount();
        StartCoroutine(System());
    }
    IEnumerator System()
    {
        behindImage.sprite = BackGroundImages[count];
        while (true)
        {
            if (!RandomSelect)
            {
                if (count < BackGroundImages.Length - 1) count++;//カウントの数を増やす
                else count = 1;//カウントの数が最大の場合は最初に戻す
            }
            else
            {
                count = randomCount();
            }

            yield return new WaitForSeconds(viewTime);
            frontImage.sprite = behindImage.sprite;
            frontImage.DOFade(1, 0);
            frontImage.DOFade(0, changeTime);
            Debug.Log(count);
            behindImage.sprite = BackGroundImages[count];
            yield return new WaitForSeconds(changeTime);

            
        }
    }
    private int randomCount()
    {
        int temp;
        while (true)
        {
            temp = Random.Range(1, BackGroundImages.Length);
            if (temp != count) break;
        }
        return temp;
    }
}
