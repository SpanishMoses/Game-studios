using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class LeaderBoardV2 : MonoBehaviour
{
    //from https://www.armandoesstuff.com/posts/unity-to-steam
    private SteamLeaderboard_t s_currentLeaderboard;
    private bool s_initialized = false;
    private CallResult<LeaderboardFindResult_t> m_findResult = new CallResult<LeaderboardFindResult_t>();
    private CallResult<LeaderboardScoreUploaded_t> m_uploadResult = new CallResult<LeaderboardScoreUploaded_t>();
    private CallResult<LeaderboardScoresDownloaded_t> m_downloadResult = new CallResult<LeaderboardScoresDownloaded_t>();
    public struct LeaderboardDataV2
    {
        public string username;
        public int rank;
        public int score;
    }
    List<LeaderboardDataV2> LeaderboardDatasetV2;


    private void Start()
    {
        if (!SteamManager.Initialized) { return; }
    }

    public void UpdateScore(int score)
    {
        if (!s_initialized)
        {
            Debug.LogError("Leaderboard not initialized");
        }
        else
        {
            score = PlayerPrefs.GetInt("high_waveV2", 0);
            //Change upload method to 
            SteamAPICall_t hSteamAPICall = SteamUserStats.UploadLeaderboardScore(s_currentLeaderboard, ELeaderboardUploadScoreMethod.k_ELeaderboardUploadScoreMethodKeepBest, score, null, 0);
            m_uploadResult.Set(hSteamAPICall, OnLeaderboardUploadResult);
        }
    }

    private void Awake()
    {
        SteamAPICall_t hSteamAPICall = SteamUserStats.FindLeaderboard("Rounds Survived Arena 2");
        m_findResult.Set(hSteamAPICall, OnLeaderboardFindResult);

    }
    private void OnLeaderboardFindResult(LeaderboardFindResult_t pCallback, bool failure)
    {
        Debug.Log($"Steam Leaderboard Find: Did it fail? {failure}, Found: {pCallback.m_bLeaderboardFound}, leaderboardID: {pCallback.m_hSteamLeaderboard.m_SteamLeaderboard}");
        s_currentLeaderboard = pCallback.m_hSteamLeaderboard;
        s_initialized = true;
    }
    private void OnLeaderboardUploadResult(LeaderboardScoreUploaded_t pCallback, bool failure)
    {
        Debug.Log($"Steam Leaderboard Upload: Did it fail? {failure}, Score: {pCallback.m_nScore}, HasChanged: {pCallback.m_bScoreChanged}");
    }
    //change ELeaderboardDataRequest to get a different set (focused around player or global)
    public void GetLeaderBoardData(ELeaderboardDataRequest _type = ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal, int entries = 10)
    {
        SteamAPICall_t hSteamAPICall;
        switch (_type)
        {
            case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobal:
                hSteamAPICall = SteamUserStats.DownloadLeaderboardEntries(s_currentLeaderboard, _type, 1, entries);
                m_downloadResult.Set(hSteamAPICall, OnLeaderboardDownloadResult);
                break;
            case ELeaderboardDataRequest.k_ELeaderboardDataRequestGlobalAroundUser:
                hSteamAPICall = SteamUserStats.DownloadLeaderboardEntries(s_currentLeaderboard, _type, -(entries / 2), (entries / 2));
                m_downloadResult.Set(hSteamAPICall, OnLeaderboardDownloadResult);
                break;
            case ELeaderboardDataRequest.k_ELeaderboardDataRequestFriends:
                hSteamAPICall = SteamUserStats.DownloadLeaderboardEntries(s_currentLeaderboard, _type, 1, entries);
                m_downloadResult.Set(hSteamAPICall, OnLeaderboardDownloadResult);
                break;
        }
        //Note that the LeaderboardDataset will not be updated immediatly (see callback below)
    }
    private void OnLeaderboardDownloadResult(LeaderboardScoresDownloaded_t pCallback, bool failure)
    {
        Debug.Log($"Steam Leaderboard Download: Did it fail? {failure}, Result - {pCallback.m_hSteamLeaderboardEntries}");
        LeaderboardDatasetV2 = new List<LeaderboardDataV2>();
        //Iterates through each entry gathered in leaderboard
        for (int i = 0; i < pCallback.m_cEntryCount; i++)
        {
            LeaderboardEntry_t leaderboardEntry;
            SteamUserStats.GetDownloadedLeaderboardEntry(pCallback.m_hSteamLeaderboardEntries, i, out leaderboardEntry, null, 0);
            //Example of how leaderboardEntry might be held/used
            LeaderboardDataV2 lD;
            lD.username = SteamFriends.GetFriendPersonaName(leaderboardEntry.m_steamIDUser);
            lD.rank = leaderboardEntry.m_nGlobalRank;
            lD.score = leaderboardEntry.m_nScore;
            LeaderboardDatasetV2.Add(lD);
            Debug.Log($"User: {lD.username} - Score: {lD.score} - Rank: {lD.rank}");
        }
        //This is the callback for my own project - function is asynchronous so it must return from here rather than from GetLeaderBoardData
        FindObjectOfType<HighscoreUIManV2>().FillLeaderboard(LeaderboardDatasetV2);
    }
}
