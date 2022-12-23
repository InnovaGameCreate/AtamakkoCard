using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class TitleBackGroundImageAnime : MonoBehaviour
{
    private Vector3 offsetPosition = new Vector3(960, 540, 0);
    void Start()
    {
        StartCoroutine(anime());
    }


    IEnumerator anime()
    {
        while (true)
        {
            Debug.Log("アニメーションを再生");
            var i = Random.Range(0, 4);
            switch (i)
            {
                case 0:
                    transform.DOMove(new Vector3(95, 0, 0) + offsetPosition, 12f);
                    break;
                case 1:
                    transform.DOMove(new Vector3(-95, 0, 0) + offsetPosition, 12f);
                    break;
                case 2:
                    transform.DOMove(new Vector3(0, 50, 0) + offsetPosition, 12f);
                    break;
                case 3:
                    transform.DOMove(new Vector3(0, -50, 0) + offsetPosition, 12f);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(17f);
        }
    }
}
