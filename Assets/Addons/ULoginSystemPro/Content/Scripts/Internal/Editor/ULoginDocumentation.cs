using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MFPSEditor;

public class ULoginDocumentation : TutorialWizard
{

    //required//////////////////////////////////////////////////////
    private const string ImagesFolder = "login-pro/editor/";
    private NetworkImages[] ServerImages = new NetworkImages[]
    {
        new NetworkImages{Name = "https://www.awardspace.com/images/web_hosting_v2_04.jpg",Type = NetworkImages.ImageType.Custom},
        new NetworkImages{Name = "img-1.jpg", Image = null},
        new NetworkImages{Name = "img-2.jpg", Image = null},
        new NetworkImages{Name = "img-3.jpg", Image = null},
        new NetworkImages{Name = "img-5.jpg", Image = null},
        new NetworkImages{Name = "img-6.jpg", Image = null},
        new NetworkImages{Name = "img-7.jpg", Image = null},
        new NetworkImages{Name = "img-8.jpg", Image = null},
        new NetworkImages{Name = "img-9.jpg", Image = null},
        new NetworkImages{Name = "img-10.jpg", Image = null},
        new NetworkImages{Name = "img-11.jpg", Image = null},
        new NetworkImages{Name = "img-12.jpg", Image = null},
    };
    private Steps[] AllSteps = new Steps[] {
     new Steps { Name = "Require", StepsLenght = 0 },
    new Steps { Name = "Hosting", StepsLenght = 4 },
    new Steps { Name = "ULogin", StepsLenght = 5 },
    new Steps { Name = "Admin Panel", StepsLenght = 0 },
    new Steps { Name = "Version Checking", StepsLenght = 0 },
    };
    //final required////////////////////////////////////////////////

    EditorWWW www = new EditorWWW();

    public override void OnEnable()
    {
        base.OnEnable();
        base.Initizalized(ServerImages, AllSteps, ImagesFolder);
        GUISkin gs = Resources.Load<GUISkin>("content/MFPSEditorSkin") as GUISkin;
        if (gs != null)
        {
            base.SetTextStyle(gs.customStyles[2]);
        }
    }

    public override void WindowArea(int window)
    {
       if(window == 0)
        {
            DrawRequire();
        }else if(window == 1) { DrawHosting(); }
       else if(window == 2) { DrawULogin(); }
       else if (window == 3) { DrawAdminPanel(); }
        else if (window == 4) { DrawVersionChecking(); }
    }

    void DrawRequire()
    {
        DrawText("ULogin System allow to player can authenticated so they can save and load their game progress and other data trough sessions," +
            " to reach this all these data are store in a external database using PHP and Mysqli, this database need to be set up before can be accessed.");
        DownArrow();
        DrawText("In order to create a Database you need:");
        DrawHorizontalColumn("Domain", "domain or web name is the address where Internet users can access your website or hosting files, for example: <i>lovattostudio.com</i>");
        DrawHorizontalColumn("Hosting Space", "an online server for store your web files on Internet so they can be viewed and accessible");
        DrawHorizontalColumn("FTP Client", "stands for File Transfer Protocol. Using an FTP client is a method to upload, download, and manage files on our server, there are some free third party program" +
            " or optionals some hosting services provide a admin panel for this (like we will see later in this tutorial)");
        DownArrow();
        DrawText("if you have or had a web site, you may be familiar with these requirements, if not you can learn how to obtain these for free in the next step.");
        GUILayout.Space(15);
    }

    void DrawHosting()
    {
        if (subStep == 0)
        {
            DrawText("Like I said before, you need a web hosting in order to use ULogin Pro, if you already have one and you know how to upload files to your server you can skip this step.");
            DownArrow();
            DrawText("there are a lot of web hosting services around Internet where you can register a domain and use hosting space, the majority of this offer only paid plans, but there are someones that" +
                " offers free accounts with limitations of course, but that is ok, you can use these free features in your Game Development phase and upgrade to a better plan when you release your game.");
            DrawText("so in this tutorial I will use a Hosting Service that offer a good free plan as example, it's called <b>Awardspace</b>, you can check it here: ");
            if (DrawImageAsButton(GetServerImage(0)))
            {
                Application.OpenURL("https://www.awardspace.com/?id=AW-16015-0");
            }
            DownArrow();
            DrawText("So in the web site of the hosting, create a account like in another site and then select the free plan or the plan that you prefer.");
            DrawText("Once you have register your account, you should see this dashboard:");
            DrawImage(GetServerImage(1));
        }else if(subStep == 1)
        {
            DrawText("Now you need to register a domain, for it go to (in the site dashboard) Hosting Tools -> Domain Manager:");
            DrawImage(GetServerImage(2));
            DownArrow();
            DrawText("Now if you have the a free plan you need to select <b>Register a Free Domain</b> (dx.am) or if you have a free plan but wanna have a custom domain like .com,.net,.uk, etc... (Recommended) you can" +
                "use <b>Register a Domain</b> and buy it, but in this tutorial I will use only free alternatives, is all up to you, so write your domain in the field like for example: <i>mygamedomain</i> and click Register");
            DrawImage(GetServerImage(3));
            DrawText("if the domain if available to register, you will see other steps to finish with the registration, so just follow it, just be sure to not select any pay feature that they will offer (if you don't want it of course)");
            DownArrow();
        }else if(subStep == 2)
        {
            DrawText("Now we have the hosting and the domain name ready, let's create a directory to store our ulogin files, so go to <b>Hosting Tools -> File Manager</b>");
            DownArrow();
            DrawText("in the page that will load, double click to open the folder with your domain name, then you will see that the folder is empty, here you need create folders that work as" +
                " directory of your server, so create a folder to host your game files, simply click on the button <b>Create</b> and in the popup window that will appear write your folder name:");
            DrawImage(GetServerImage(4));
            DownArrow();
            DrawText("Open the new folder, and if you want can create other folder to store only the php script, example called it \"php\" so the directory will be like: \"game-name\\php\\\".");          
        }else if(subStep == 3)
        {
            DrawText("Right, so now we have the hosting space, domain name and our the directory to upload the ulogin files, now you need create a Database");
            DownArrow();
            DrawText("Go to <b>Hosting Tools -> MySQL DataBases</b>, in the loaded page you will see a simply form for create the database, just set the database name and a password, \nfor the name you" +
                " can simple set something like \"game\" and a secure password, and then click on <b>Create DataBase</b> button");
            DrawImage(GetServerImage(5));
            DownArrow();
            DrawText("Good, now you have your web hosting and database ready!, continue with the next step to set up ULogin");
        }
    }

    int checkID = 0;
    void DrawULogin()
    {
        if (subStep == 0)
        {
            DrawText("Now that you have all necessary, it's time to set up the ULogin files, first thing that you need to do is upload the php scripts in your web hosting directory," +
                "these php scripts allow the communication from the game client to your server and the server handle with the Database stuff with the info receive from the client, but before that you need " +
                "set some info in one of the php scripts.");
            DrawText("<b>Open</b> the script called <b>bl_Common.php</b> (on the PHP folder of ULogin)");
            DrawImage(GetServerImage(7));
            DownArrow();
            DrawText("In this scripts you need set the information of your database, that is used in order to access to the database for read and write when we required it, for obtain the information" +
                " of the database varies of hosting on hosting, but in Awardspace you can get in: Hosting Tools -> MySQL DataBase -> (wait until load finish) -> click on the button under <b>Options</b>" +
                " -> Information -> database information will display like this: ");
            DrawImage(GetServerImage(8));
            DownArrow();
            DrawText("Now in <b>bl_Common.php</b> you need set the database information like this");
            DrawHorizontalColumn("$hostName", "The name of your host, get it from the database info in the hosting page");
            DrawHorizontalColumn("$dbName", "The name the database, get it from the database info in the hosting page");
            DrawHorizontalColumn("$dbUser", "The user name of the database, in Awardspace this is the same as the database name");
            DrawHorizontalColumn("$dbPassworld", "The password that you set when create the database");
            DrawHorizontalColumn("$secretKey", "Set a custom 'password', that just work as a extra security check for avoid others can execute the php code");
            DrawHorizontalColumn("$base_url", "The URL / Address where the php scripts are located in your server, including the domain name and http, for example: <i>http://www.mygamedomain.com/game/php/</i>");
            DrawHorizontalColumn("$emailFrom", "Your server email from where 'Register confirmation' will be send (Only require if you want use confirmation for register), <b>NOTE:</b> this email need to be configure in your hosting, in Awardspace you can create a " +
                "email account in <b>Hosting Tools -> E-mail Account</b>.");
            DrawText("So after you set all the info, your script should look like this (with your own info of course)");
            DrawImage(GetServerImage(9));
            DrawText("Don't forget to save the changes in the script.");
        }
        else if (subStep == 1)
        {
            DrawText("Now you need upload the scripts, in order to upload the php scripts you need have your FTP client ready with the directory where you wanna store the files, if you are using Awardspace hosting, simple go to:" +
    " <b>Hosting Tools -> File Manager</b> -> open the folder with your domain -> open the directory / folder where you will store the files, then click on the button <b>Upload</b>");
            DrawText("In the popup window that will appear you need to drag all the php scripts of ULogin.");
            DownArrow();
            DrawText("The require php scripts are located inside the ULogin Pro folder in your project: <i>Assets -> Addons -> ULoginSystemPro -> Content -> Scripts -> PHP -> *</i>" +
                " open this folder and select all the php scripts (without .meta files), the <b>crossdomain.xml</b> and <b>sql-tables.sql</b>, then drag all these files in the window in the Upload files panel of " +
                "Awardspace dashboard, then click on <b>Upload</b> button.");
            DrawImage(GetServerImage(6));
            DownArrow();
            DrawText("Once all files are uploaded, copy the address in the top bar");
            DrawImage(GetServerImage(10));
            DownArrow();
            DrawText("Go to Unity and open <b>LoginDataBasePro</b> (click button bellow)");
            if (DrawButton("LoginDataBasePro"))
            {
                Selection.activeObject = bl_LoginProDataBase.Instance;
                EditorGUIUtility.PingObject(bl_LoginProDataBase.Instance);
                NextStep();
            }
        }
        else if (subStep == 2)
        {
            DrawText("Now in the inspector window you will see the LoginDataBasePro properties, in the field <b>Php Host Path</b> paste the address that you copied previously," +
                "and add <b>http</b> plus your domain name, for example, the adress that I copied was: <i><color=#40C0FF>mfps-tutorial.dx.am/game-name/php</color></i>" +
                " so after modified it, look like this: <i><color=#40C0FF>http://www.mfps-tutorial.dx.am/game-name/php/</color></i> replace the domain name with your own domain.");
            DownArrow();
            DrawText("In the <b> SecretKey</b> field set the same 'password' that you set in bl_Common.php");
            DrawImage(GetServerImage(11));
        }
        else if (subStep == 3)
        {
            DrawText("Now you need set up the tables in your database, for this we'll use SQL, you can do that manually or automatically," +
                "for do it manually you can use some database tool like PhpMyAdmin and run the SQL query in their sql panel or you can do it here.");
            DownArrow();
            DrawText("First lets check that the tables has not been created yet, click on the button bellow to check the tables.");
            GUILayout.Space(5);
            bool isLURL = bl_LoginProDataBase.Instance.PhpHostPath.Contains("lovatto");
            if (isLURL)
            {
                GUILayout.Label("<color=red>You are still are using the example lovatto studio url,\n please use your own url</color>");
            }
            GUI.enabled = (checkID == 0 && !isLURL);
            if (DrawButton("Check Tables"))
            {
                WWWForm wf = new WWWForm();
                wf.AddField("type", 3);
                www.SendRequest(bl_LoginProDataBase.Instance.GetUrl(bl_LoginProDataBase.URLType.Creator), wf, CheckResult);
                checkID = 1;
            }
            GUI.enabled = true;
            if (checkID == 1)
            {
                GUILayout.Label("LOADING...");
            }
            else if (checkID == 2)
            {
                GUILayout.Label("RESULT: " + checkLog);
            }
            else if (checkID >= 3)
            {
                GUI.enabled = (checkID == 3);
                DrawText("Ok, we have checked and tables are not created yet, so we can do it now, click on the button bellow to create tables");
                GUILayout.Space(5);
                if (DrawButton("Create Tables"))
                {
                    WWWForm wf = new WWWForm();
                    wf.AddField("type", 4);
                    www.SendRequest(bl_LoginProDataBase.Instance.GetUrl(bl_LoginProDataBase.URLType.Creator), wf, CheckCreation);
                    checkID = 4;
                }
                GUI.enabled = true;
                if (checkID == 4) { GUILayout.Label("LOADING..."); }
                if(checkID == 5)
                {
                    DrawText("Done!, Tables has been created successfully.");
                }else if(checkID == 6)
                {
                    DrawText("Couldn't create tables error: <color=red>" + checkLog + "</color>");
                }
            }
        }
        else if(subStep == 4)
        {
            DrawText("Done!, you have set up your database and are ready to use ULogin System!");
            DownArrow();
            DrawText("So let me explain some the rest of options in <b>LoginDataBasePro</b>");
            DrawHorizontalColumn("Update IP", "ULogin collect the IP of the register user in order to block their IP if they get banned in game, if you enable this ULogin will check the IP each time" +
                " that the user log-in so always keep the store IP update");
            DrawHorizontalColumn("Detect Ban", "Basically is = to say: you wanna use Ban Features.");
            DrawHorizontalColumn("Require Email Verification", "After register a account user is required to verify his email in order to log-in?");
            DrawHorizontalColumn("Check Ban In Mid", "Check if the player has not been banned each certain amount of time, if this is false that check will only be fire when player log-in");
            DrawHorizontalColumn("Can Register Same Email", "Can a email be use to register different accounts?");
        }
    }

    void DrawAdminPanel()
    {
        DrawText("ULogin comes with a handy scene that allow the admin/dev and their game moderators make some basic management like Ban, Ascend,etc... to the users right in the game");
        DrawText("For example if you wanna ban a user because X or Y reason you can do as simple as open and play the AdminPanel scene -> in the left side panel, write the user login or nick name -> " +
            "click on Search -> Click on Ban and done, same for UnBan ascend, descend, etc...");
        DownArrow();
        DrawText("Also allow to reply to Tickets summited by players, check some game statistics and database stat, to access to the AdminPanel in game user need to have a status / role of" +
            " Admin or Moderator, so when he/she log-in a AdminPanel button will appear in the log-in confirmation window.");
    }

    void DrawVersionChecking()
    {
        DrawText("ULogin 1.6 comes with a new featured which is 'Version Checking' what this do is compare the local game version with the server game version, if the local version is different then " +
            "players will be not able to login or play the game until they update the game.");
        DownArrow();
        DrawText("To enable this feature, simple go to <i>Assets -> Addons -> ULoginSystemPro -> Content -> Resources -> LoginDataBasePro -> check 'Check Game Version'");
        DownArrow();
        DrawText("Now to change the Game Version, first the Local Game Version is in the GameData in the Resources folder of MFPS -> GameData -> GameVersion.");
        DrawText("For change the server Game Version you have to edit the bl_Common.php script in your server and edit the variable '$GameVersion'");
    }

    string checkLog = "";
    void CheckResult(string data, bool isError)
    {
        if (data.Contains("yes"))
        {
            checkLog = "Tables are already created";
            checkID = 2;
        }
        else if(data.Contains("no"))
        {
            checkLog = "Tables are not created yet";
            checkID = 3;
        }else
        {
            checkLog = data;
            checkID = 2;
        }
        Repaint();
    }

    void CheckCreation(string data, bool isError)
    {
        if (data.Contains("done"))
        {
            checkLog = "Tables created successfully";
            checkID = 5;
        }
        else
        {
            checkLog = data;
            checkID = 6;
        }
        Repaint();
    }

    [MenuItem("MFPS/Addons/ULogin/Tutorial")]
    private static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(ULoginDocumentation));
    }

    [MenuItem("MFPS/Tutorials/ULogin Pro")]
    private static void ShowWindowMFPS()
    {
        EditorWindow.GetWindow(typeof(ULoginDocumentation));
    }
}