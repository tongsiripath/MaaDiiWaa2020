using UnityEngine;
using UnityEngine.UI;

public class bl_SignIn : MonoBehaviour
{
    [Header("References")]
    [SerializeField]private InputField UserNameInput;
    [SerializeField]private InputField PasswordInput;
    [SerializeField]private Text KillStatic;
    [SerializeField]private Text Deathstatic;
    [SerializeField]private Text ScoreStatic;
    [SerializeField]private Text PlayTimeStatic;
    [SerializeField]private Text CoinsText;
    public Text ClanNameText;
    [SerializeField]private Toggle RememberToggle;
    private bl_LoginPro Login;

    private const string REMEMBER_KEY = "login.remember.bool";

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        Login = FindObjectOfType<bl_LoginPro>();
        bool b = PlayerPrefs.GetString(REMEMBER_KEY, string.Empty) == string.Empty;
        RememberToggle.isOn = !b;
        if (b == false) { UserNameInput.text = PlayerPrefs.GetString(REMEMBER_KEY, string.Empty); }
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDisable()
    {
       // UserNameInput.text = string.Empty;
        PasswordInput.text = string.Empty;
    }

    public void OnLogin(LoginUserInfo info)
    {
        KillStatic.text = info.Kills.ToString();
        Deathstatic.text = info.Deaths.ToString();
        ScoreStatic.text = info.Score.ToString();
        CoinsText.text = info.Coins.ToString();
        PlayTimeStatic.text = bl_DataBaseUtils.TimeFormat(info.PlayTime);
#if CLANS
        if (info.Clan.haveClan)
        {
            ClanNameText.text = info.Clan.Name;
        }
        else { ClanNameText.transform.parent.gameObject.SetActive(false); }
#else
        ClanNameText.transform.parent.gameObject.SetActive(false);
#endif
    }

    /// <summary>
    /// 
    /// </summary>
    public void SignIn()
    {
        if (Login == null)
            return;

        string user = UserNameInput.text;
        string pass = PasswordInput.text;

        if (string.IsNullOrEmpty(user))
        {
            Debug.Log("User name can't be empty, please write your user name");
            Login.SetLogText("User name can't be empty, please write your user name");
            return;
        }
        if (string.IsNullOrEmpty(pass))
        {
            Debug.Log("Password can't be empty, please write your password");
            Login.SetLogText("Password can't be empty, please write your password");
            return;
        }
        Login.Login(user, pass);
        PlayerPrefs.SetString(REMEMBER_KEY, (RememberToggle.isOn ? user : string.Empty));
    }
}