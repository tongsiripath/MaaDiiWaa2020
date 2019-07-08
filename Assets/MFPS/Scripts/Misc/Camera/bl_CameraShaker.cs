﻿using System.Collections;
using UnityEngine.PostProcessing;
using UnityEngine;

public class bl_CameraShaker : MonoBehaviour
{

    private Vector3 OrigiPosition;
    private Quaternion DefaultCamRot;
    private PostProcessingBehaviour PostEffect;
    private PostProcessingBehaviour SecondPostEffect;

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        DefaultCamRot = transform.localRotation;
        OrigiPosition = transform.localPosition;
        PostEffect = GetComponent<PostProcessingBehaviour>();
        SecondPostEffect = transform.GetChild(0).GetComponent<PostProcessingBehaviour>();
        LoadEffectsStoreData();
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnEnable()
    {
        bl_EventHandler.OnLocalPlayerShake += OnShake;
        bl_EventHandler.OnEffectChange += OnPostEffect;
    }

    /// <summary>
    /// 
    /// </summary>
    private void OnDisable()
    {
        bl_EventHandler.OnLocalPlayerShake -= OnShake;
        bl_EventHandler.OnEffectChange -= OnPostEffect;
    }

    void OnShake(float amount = 0.2f,  float duration = 0.4f,float intense = 0.25f,bool aim = false)
    {
        StopAllCoroutines();
        StartCoroutine(CamShake(amount,duration,intense,aim));
    }

    void OnPostEffect(bool chrab, bool anti, bool bloom,bool ssao, bool motionBlur)
    {
        if (PostEffect != null)
        {
            PostEffect.profile.ambientOcclusion.enabled = ssao;
        }
        if (SecondPostEffect != null)
        {
            SecondPostEffect.profile.motionBlur.enabled = motionBlur;
            SecondPostEffect.profile.chromaticAberration.enabled = chrab;
            SecondPostEffect.profile.antialiasing.enabled = anti;
            SecondPostEffect.profile.bloom.enabled = bloom;
        }
    }

    void LoadEffectsStoreData()
    {
        if (PostEffect != null)
        {
            PostEffect.profile.ambientOcclusion.enabled = PropertiesKeys.GetUniqueKey("ambOclu").GetBoolPrefs(true);
        }
        if (SecondPostEffect != null)
        {
            SecondPostEffect.profile.motionBlur.enabled = PropertiesKeys.GetUniqueKey("motionB").GetBoolPrefs(true);
            SecondPostEffect.profile.chromaticAberration.enabled = PropertiesKeys.GetUniqueKey("chroAb").GetBoolPrefs(true); 
            SecondPostEffect.profile.antialiasing.enabled = PropertiesKeys.GetUniqueKey("antiAli").GetBoolPrefs(true); 
            SecondPostEffect.profile.bloom.enabled = PropertiesKeys.GetUniqueKey("bloom").GetBoolPrefs(true);
        }
    }

    /// <summary>
    /// move the camera in a small range
    /// with the presets Gun
    /// </summary>
    /// <returns></returns>
    IEnumerator CamShake(float amount = 0.2f, float duration = 0.4f, float intense = 0.25f, bool aim = false)
    {
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float percentComplete = elapsed / duration;
            float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

            // map value to [-1, 1]
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= intense * damper;
            y *= intense * damper;
            float mult = (aim) ? 1 : 3;
            float multr = (aim) ? 25 : 40;

            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(x * mult, y * mult, OrigiPosition.z), Time.deltaTime * 17);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(new Vector3(x * multr, y * multr, DefaultCamRot.z)), Time.deltaTime * 12);
            yield return null;
        }

        transform.localPosition = OrigiPosition;
        transform.localRotation = DefaultCamRot;

    }
}