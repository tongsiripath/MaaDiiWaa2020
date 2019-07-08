using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class LoginUserInfo
{
    public string LoginName = "";
    public string NickName = "";
    public int Kills = 0;
    public int Deaths = 0;
    public int Score = 0;
    public int Coins = 0;
    public int PlayTime = 0;
    public string IP = "";
    public string SavedIP = "";
    public int ID = 0;
    public List<string> FriendList = new List<string>();
    public Status UserStatus = Status.NormalUser;
#if CLANS
    public bl_ClanInfo Clan = null;
#endif
#if SHOP
    public bl_ShopUserData ShopData = null;
#endif

    public enum Status
    {
        NormalUser = 0,
        Admin = 1,
        Moderator = 2,
        Banned = 3,
    }   

    public WWWForm AddData(WWWForm form)
    {
        form.AddField("kills", Kills);
        form.AddField("deaths", Deaths);
        form.AddField("score", Score);
        return form;
    }

    public void SetNewData(int score, int kills, int deaths)
    {
        Score += score;
        Kills += kills;
        Deaths += deaths;
    }

    public void SetFriends(string line)
    {
        if (!string.IsNullOrEmpty(line))
        {
            FriendList.Clear();
            string[] splitFriends = line.Split('/');
            FriendList.AddRange(splitFriends);
        }
    }
}