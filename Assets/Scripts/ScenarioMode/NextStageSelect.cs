using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextStageSelect : MonoBehaviour
{

    /// <summary>
    /// �V�i���I�ōŌ�̃}�X�ɍs�����㎟�̕����X�e�[�W�֐i�ނ��ǂ���
    /// </summary>

    [SerializeField]
    private SceneObject[] nextScenes;

    public void goNestStage(int i)
    {

        SceneManager.LoadScene(nextScenes[i]);
    }

    public void BackToTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
