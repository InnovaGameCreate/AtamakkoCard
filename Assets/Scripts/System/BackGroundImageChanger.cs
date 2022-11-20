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
    private bool RandomSelect;//�I�΂�Ă���摜���烉���_���ŕύX���邩�ǂ���
    [SerializeField]
    private float viewTime;//�ύX�����܂ł̎���
    [SerializeField]
    private float changeTime;//�摜���J�ڂ��鎞��
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
                if (count < BackGroundImages.Length - 1) count++;//�J�E���g�̐��𑝂₷
                else count = 1;//�J�E���g�̐����ő�̏ꍇ�͍ŏ��ɖ߂�
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
