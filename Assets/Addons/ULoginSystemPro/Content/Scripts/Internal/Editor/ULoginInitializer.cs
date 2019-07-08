using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using MFPSEditor;

public class ULoginInitializer : MonoBehaviour
{

    private const string DEFINE_KEY = "ULSP";

    static ULoginInitializer()
    {
        int start = PlayerPrefs.GetInt("mfps.addons.define." + DEFINE_KEY, 0);
        if (start == 0)
        {
            bool defines = EditorUtils.CompilerIsDefine(DEFINE_KEY);
            if (!defines)
            {
                EditorUtils.SetEnabled(DEFINE_KEY, true);
            }
            PlayerPrefs.SetInt("mfps.addons.define." + DEFINE_KEY, 1);
        }
    }


    [MenuItem("MFPS/Addons/ULogin/Enable")]
    private static void Enable()
    {
        EditorUtils.SetEnabled(DEFINE_KEY, true);
    }


    [MenuItem("MFPS/Addons/ULogin/Enable", true)]
    private static bool EnableValidate()
    {
        return !EditorUtils.CompilerIsDefine(DEFINE_KEY);
    }


    [MenuItem("MFPS/Addons/ULogin/Disable")]
    private static void Disable()
    {
        EditorUtils.SetEnabled(DEFINE_KEY, false);
    }

    [MenuItem("MFPS/Addons/ULogin/Disable", true)]
    private static bool DisableValidate()
    {
        return EditorUtils.CompilerIsDefine(DEFINE_KEY);
    }

    [MenuItem("MFPS/Addons/ULogin/Integrate")]
    private static void Instegrate()
    {
        if (SceneManager.sceneCountInBuildSettings > 0)
        {
            Scene menuScene = EditorSceneManager.OpenScene("Assets/MFPS/Scenes/MainMenu.unity", OpenSceneMode.Single);
            GameObject prefab = AssetDatabase.LoadAssetAtPath("Assets/Addons/ULoginSystemPro/Content/Prefabs/UI/Profile.prefab", typeof(GameObject)) as GameObject;
            bl_Lobby lb = FindObjectOfType<bl_Lobby>();
            if (lb != null)
            {
                if (FindObjectOfType<bl_UserProfile>() == null)
                {
                    GameObject inst = PrefabUtility.InstantiatePrefab(prefab, menuScene) as GameObject;
                    GameObject ccb = lb.AddonsButtons[4];
                    if (ccb != null)
                    {
                        inst.transform.SetParent(ccb.transform, false);
                        inst.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                        EditorUtility.SetDirty(inst);
                        EditorUtility.SetDirty(lb);
                        EditorSceneManager.MarkSceneDirty(menuScene);
                        Debug.Log("ULogin Pro successfully integrated");
                    }
                }
                else
                {
                    Debug.Log("ULogin Pro is already integrated");
                }
            }
            else
            {
                Debug.LogWarning("Can't found the MainMenu scene, that could be cause a change in the default structure of the MFPS folders.");
            }

        }
        else
        {
            Debug.LogWarning("Scenes has not been added in Build Settings, Can't integrate ULogin Pro");
        }

    }
}