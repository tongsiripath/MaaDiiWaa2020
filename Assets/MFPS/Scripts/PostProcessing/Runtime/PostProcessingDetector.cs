using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class PostProcessingDetector : MonoBehaviour
{

    private void OnEnable()
    {
        if(bl_UtilityHelper.isMobile || QualitySettings.GetQualityLevel() <= 1)
        {
            PostProcessingBehaviour pb = GetComponent<PostProcessingBehaviour>();
            if(pb != null)
            {
                Destroy(pb);
            }
        }
    }
}