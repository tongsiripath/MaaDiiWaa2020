using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class bl_UserProfile : MonoBehaviour
{

    [Header("References")]
    [SerializeField]private Text NameText;
    [SerializeField]private Text ProfileNameText;
    [SerializeField]private Text ScoreText;
    [SerializeField]private Text KillsText;
    [SerializeField]private Text DeathsText;
    [SerializeField]private Text PlayTimeText;
    [SerializeField]private Text CoinsText;
    [SerializeField] private Text BarCoinsText;
    public Text ClanText;
    [SerializeField]private Text LogText;
    [SerializeField]private GameObject ProfileWindow;
    [SerializeField]private GameObject SettingsWindow;
    [SerializeField]private GameObject ChangeNameWindow;
    [SerializeField]private GameObject SuccessWindow;
    [SerializeField]private GameObject ChangeNameButton;  
    [SerializeField]private InputField CurrentPassNick;
    [SerializeField]private InputField NewNickInput;
    [SerializeField]private InputField CurrentPassInput;
    [SerializeField]private InputField NewPassInput;
    [SerializeField]private InputField RePassInput;

    private bl_DataBase DataBase;
    private bool isSettingOpen = false;
    private bool isOpen = false;

    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        DataBase = FindObjectOfType<bl_DataBase>();
        if(DataBase != null && DataBase.isLogged)
        {
            OnLogin();
        }
        ChangeNameButton.SetActive(bl_LoginProDataBase.Instance.PlayerCanChangeNick);
    }

    private void OnEnable()
    {
        bl_DataBase.OnUpdateData += OnUpdateData;
    }

    private void OnDisable()
    {
        bl_DataBase.OnUpdateData -= OnUpdateData;
    }

    void OnUpdateData(LoginUserInfo userInfo)
    {
        OnLogin();
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnLogin()
    {
        if(DataBase == null) { DataBase = FindObjectOfType<bl_DataBase>(); }
        ProfileNameText.text = DataBase.LocalUser.NickName;
        NameText.text = DataBase.LocalUser.NickName;
        ScoreText.text = DataBase.LocalUser.Score.ToString();
        KillsText.text = DataBase.LocalUser.Kills.ToString();
        DeathsText.text = DataBase.LocalUser.Deaths.ToString();
        CoinsText.text = DataBase.LocalUser.Coins.ToString();
        BarCoinsText.text = DataBase.LocalUser.Coins.ToString();
#if CLANS
        if (DataBase.LocalUser.Clan.haveClan)
        {
            ClanText.text = DataBase.LocalUser.Clan.Name;
        }
        else
        {
            ClanText.transform.parent.gameObject.SetActive(false);
        }
#else
         ClanText.transform.parent.gameObject.SetActive(false);
#endif
        PlayTimeText.text = bl_DataBaseUtils.TimeFormat(DataBase.LocalUser.PlayTime);
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnSettings()
    {
        isSettingOpen = !isSettingOpen;
        SettingsWindow.SetActive(isSettingOpen);
    }

    public void OnProfile()
    {
        isOpen = !isOpen;
        ProfileWindow.SetActive(isOpen);
        if (!isOpen)
        {
            SettingsWindow.SetActive(false);
            ChangeNameWindow.SetActive(false);
        }
    }

    public void ChangeName()
    {
        string pass = CurrentPassNick.text;
        string nick = NewNickInput.text;
        if(pass != DataBase.CacheAccessToken)
        {
            Debug.Log("Password doesn't match!");
            SetLog("Password doesn't match!");
            return;
        }
        if (string.IsNullOrEmpty(nick))
        {
            SetLog("Empty nick name");
            return;
        }
        if (nick.Length < 3)
        {
            SetLog("Nick name should have 3 or more characters");
            return;
        }
        StartCoroutine(SetChangeName(nick));
    }

    IEnumerator SetChangeName(string nick)
    {      
        WWWForm wf = new WWWForm();
        string hash = bl_DataBaseUtils.Md5Sum(DataBase.LocalUser.LoginName + bl_LoginProDataBase.Instance.SecretKey).ToLower();
        wf.AddField("name", DataBase.LocalUser.LoginName);
        wf.AddField("nick", nick);
        wf.AddField("typ", 4);
        wf.AddField("hash", hash);

        using (UnityWebRequest www = UnityWebRequest.Post(bl_LoginProDataBase.Instance.GetUrl(bl_LoginProDataBase.URLType.DataBase), wf))
        {
            yield return www.SendWebRequest();

            if (www.error == null && !www.isNetworkError)
            {
                if (www.downloadHandler.text.Contains("successcn"))
                {
                    DataBase.LocalUser.NickName = nick;
                    ProfileNameText.text = DataBase.LocalUser.NickName;
                    NameText.text = DataBase.LocalUser.NickName;
                    Debug.Log("Change nick name!");
                    SuccessWindow.SetActive(true);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                }
            }
            else
            {
                Debug.LogError(www.error);
            }
        }
    }

    public void ChangePass()
    {
        string cp = CurrentPassInput.text;
        string np = NewPassInput.text;
        string rp = RePassInput.text;

        if (cp != DataBase.CacheAccessToken)
        {
            Debug.Log("Password doesn't match!");
            SetLog("Password doesn't match!");
            return;
        }
        if(np != rp)
        {
            Debug.Log("New password doesn't match!");
            SetLog("New password doesn't match!");
            return;
        }
        if(np.Length < bl_LoginProDataBase.Instance.MinPasswordLenght)
        {
            string t = string.Format("Password should have {0} or more character", bl_LoginProDataBase.Instance.MinPasswordLenght);
            Debug.Log(t);
            SetLog(t);
            return;
        }
        StartCoroutine(SetChangePass(cp, np));
    }

    IEnumerator SetChangePass(string pass, string newpass)
    {
        //Used for security check for authorization to modify database
        string hash = bl_DataBaseUtils.Md5Sum(DataBase.LocalUser.ID + bl_LoginProDataBase.Instance.SecretKey).ToLower();
        // Create instance of WWWForm
        WWWForm wf = new WWWForm();
        //sets the mySQL query to the amount of rows to load
        wf.AddField("id", DataBase.LocalUser.ID);
        wf.AddField("password", pass);
        wf.AddField("newpassword", newpass);
        wf.AddField("hash", hash);
        //Creates instance to run the php script to access the mySQL database
        using (UnityWebRequest www = UnityWebRequest.Post(bl_LoginProDataBase.Instance.GetUrl(bl_LoginProDataBase.URLType.ChangePassword), wf))
        {
            //Wait for server response...
            yield return www.SendWebRequest();
            string result = www.downloadHandler.text;

            //check if we have some error
            if (www.error == null && !www.isNetworkError)
            {
                if (result.Contains("success"))
                {
                    Debug.Log("Change password!");
                    DataBase.CacheAccessToken = newpass;
                    SuccessWindow.SetActive(true);
                }
                else//Wait, have a error?, please contact me for help with the result of next debug log.
                {
                    // ErrorType(result);
                }
            }
            else
            {
                Debug.LogError("Error: " + www.error);
                SetLog(www.error);
            }
        }
    }

    void SetLog(string t)
    {
        LogText.text = t;
        Invoke("CleanLog", 5);
    }
    
    void CleanLog() { LogText.text = string.Empty; }

    private static bl_UserProfile _instance;
    public static bl_UserProfile Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<bl_UserProfile>();
            }
            return _instance;
        }
    }
}