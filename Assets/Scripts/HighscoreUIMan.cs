﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreUIMan : MonoBehaviour
{
    //from https://www.armandoesstuff.com/posts/unity-to-steam
    [SerializeField] Transform holder;
    [SerializeField] GameObject highscorePrefab;
    List<GameObject> highscorePrefabs = new List<GameObject>();

    private void Start()
    {
        if (!SteamManager.Initialized) { return; }
    }

    public void BtnBeginFillLeaderboardLocal()
    {
        FindObjectOfType<LeaderBoards>().GetLeaderBoardData(Steamworks.ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser, 10);
    }
    public void BtnBeginFillLeaderboardGlobal()
    {
        FindObjectOfType<LeaderBoards>().GetLeaderBoardData(Steamworks.ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, 10);
    }
    public void BtnBeginFillLeaderboardFriends()
    {
        FindObjectOfType<LeaderBoards>().GetLeaderBoardData(Steamworks.ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends, 10);
    }
    public void FillLeaderboard(List<LeaderBoards.LeaderboardData> lDataset)
    {
        Debug.Log("filling leaderboard");
        foreach (GameObject g in highscorePrefabs)
        {
            Destroy(g);
        }
        foreach (LeaderBoards.LeaderboardData lD in lDataset)
        {
            GameObject g = Instantiate(highscorePrefab, holder);
            highscorePrefabs.Add(g);
            FillHighscorePrefab(g, lD);
        }
    }
    void FillHighscorePrefab(GameObject _prefab, LeaderBoards.LeaderboardData _lData)
    {
        _prefab.transform.Find("username").GetComponent<TextMeshProUGUI>().text = _lData.username;
        _prefab.transform.Find("score").GetComponent<TextMeshProUGUI>().text = _lData.score.ToString();
        _prefab.transform.Find("rank").GetComponent<TextMeshProUGUI>().text = _lData.rank.ToString();
    }
}
