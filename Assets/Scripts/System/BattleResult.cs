using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using storyMode;

public class BattleResult : MonoBehaviour
{
    PastSceneManager SceneManager;
    private enum PastSceneType
    {
        scenario,
        arena,
        pvp,
    }
    private PastSceneType ResultType;
    void Start()
    {
        SceneManager = FindObjectOfType<PastSceneManager>();
        checkType();
    }

    private void checkType()
    {
        switch (SceneManager.getPastScene())
        {
            case "Matching":
                ResultType = PastSceneType.pvp;
                break;
            case "StoryBoard1":
            case "StoryBoard2":
            case "StoryBoardSelect":
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
                ResultType = PastSceneType.scenario;
                break;
            case "Arena":
                ResultType = PastSceneType.arena;
                break;
            default:
                break;
        }
    }

    public void setResult(bool isWinGame)
    {
        PlayerConfig.SetData();
        switch (ResultType)
        {
            case PastSceneType.scenario:
                if(!isWinGame)
                {
                    ProgressRecorder.BattleDefeated = true;
                }
                break;
            case PastSceneType.arena:
                if(isWinGame)
                {
                    PlayerConfig.ArenaRank--;
                }
                break;
            case PastSceneType.pvp:
                if (isWinGame)
                {
                    PlayerConfig.PlayerRate += Random.Range(8, 11);
                }
                else
                {
                    PlayerConfig.PlayerRate -= Random.Range(8, 11);
                }
                break;
            default:
                break;
        }
    }
}
