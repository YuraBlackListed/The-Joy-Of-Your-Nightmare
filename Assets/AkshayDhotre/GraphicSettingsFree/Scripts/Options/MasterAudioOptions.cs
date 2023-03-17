using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AkshayDhotre.GraphicSettingsMenu
{
    public class MasterAudioOptions : Option
    {
        [SerializeField] private SettingsScrObj scriptableObj;
        [SerializeField] private Slider slider;
        public override void Apply()
        {
            
        }


        public void SetCurrentsuboptionBySlider()
        {
            foreach (var t in subOptionList)
            {
                scriptableObj.masterVolume = (int)slider.value;
                currentSubOption = t;
                currentSubOptionIndex = t.indexInList;
                currentSubOption.integerValue = (int)slider.value;
                UpdateSuboptionText();
                return;
            }
            
        }
        public void SetCurrentsuboptionByValue(int v)
        {
            
            if (subOptionList.Count > 0)
            {
                foreach (var t in subOptionList)
                {
                    slider.value = v;
                    scriptableObj.masterVolume = v;
                    currentSubOption = t;
                    currentSubOptionIndex = t.indexInList;
                    t.integerValue = v;
                    currentSubOption.integerValue = v;
                    UpdateSuboptionText();
                    return;
                }

                //If no item is found then we use the fall back option
                Debug.LogWarning("Suboption with value : " + v + " ,not found in :" + gameObject.name + ",using fallback option instead");
                currentSubOption = fallBackOption;
                currentSubOptionIndex = fallBackOption.indexInList;
            }
            else
            {
                Debug.LogError("No items in suboption list : " + gameObject.name);
            }
        }
    }

}

