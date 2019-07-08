using UnityEngine;
using System.Collections.Generic;
#if UNITY_5_3 || UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif
using System.Text;
using System;
using Random = UnityEngine.Random;

public static class bl_DataBaseUtils
{

    public static void LoadLevel(string scene)
    {
#if UNITY_5_3 || UNITY_5_4 || UNITY_5_3_OR_NEWER
        SceneManager.LoadScene(scene);
#else
        Application.LoadLevel(scene);
#endif
    }

    public static void LoadLevel(int scene)
    {
#if UNITY_5_3 || UNITY_5_4 || UNITY_5_3_OR_NEWER
        SceneManager.LoadScene(scene);
#else
        Application.LoadLevel(scene);
#endif
    }

    public static string GenerateKey(int length = 7)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghlmnopqrustowuvwxyz";
        string key = "";
        for (int i = 0; i < length; i++)
        {
            key += chars[Random.Range(0, chars.Length)];
        }
        return key;
    }

    public static int ToInt(this string str)
    {
        int i = 0;
        if (!int.TryParse(str, out i))
        {
            Debug.LogWarning("Can't parse this string: " + str);
        }
        return i;
    }

    public static string Md5Sum(string input)
    {
        System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++) { sb.Append(hash[i].ToString("X2")); }
        return sb.ToString();
    }

    public static string TimeFormat(float seconds)
    {
        TimeSpan t = TimeSpan.FromSeconds((double)seconds);
        string answer = string.Format("{0:D2}h:{1:D2}m", t.Hours, t.Minutes);
        return answer;
    }

    public static bool ContainsAny(this string haystack, params string[] needles)
    {
        foreach (string needle in needles)
        {
            if (haystack.Contains(needle))
                return true;
        }

        return false;
    }
}