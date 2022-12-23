using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace System.Audio
{
    public class BGMPlayer : MonoBehaviour
    {
        public enum CurrentScene
        {
            Title,
            Battle,
            Story,
        }

        public static CurrentScene BGMScene = CurrentScene.Title;
        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            AnalysisCurrentScene();
            PlayBGM(BGMScene);
        }
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log(scene.name + " scene loaded");
            AnalysisCurrentScene();
        }

        public void PlayBGM(CurrentScene targetBGMScene)
        {
            BGMManager.Instance.StopSound();
            switch (targetBGMScene)
            {
                case CurrentScene.Title:
                    BGMManager.Instance.ShotSe(BGMType.Title01);
                    break;
                case CurrentScene.Battle:
                    BGMManager.Instance.ShotSe(BGMType.Battle01);
                    break;
                case CurrentScene.Story:
                    break;
                default:
                    break;
            }
        }

        public void AnalysisCurrentScene()
        {
            var scene = SceneManager.GetActiveScene().name;
            Debug.Log(SceneManager.GetActiveScene().name);
            switch (scene)
            {
                case "Title":
                case "Matching":
                    PlayBGM(CurrentScene.Title);
                    break;
                case "Battle":
                case "BattleCPU":
                    PlayBGM(CurrentScene.Battle);
                    break;
                case "StoryBoard1":
                case "StoryBoard2":
                case "StoryBoardBlue1":
                case "StoryBoardBlue2":
                case "StoryBoardBlue3":
                case "StoryBoardEnd1":
                case "StoryBoardEnd2":
                case "StoryBoardEnd3":
                case "StoryBoardRed1":
                case "StoryBoardRed2":
                case "StoryBoardRed3":
                case "StoryBoardWhite1":
                case "StoryBoardWhite2":
                case "StoryBoardWhite3":
                    PlayBGM(CurrentScene.Story);
                    break;

                default:
                    break;
            }
        }
    }
}