using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AkshayDhotre.GraphicSettingsMenu
{
    public class SensitivityOption : Option
    {
        [SerializeField] private ControllsValue scriptableObj;
        [SerializeField] private Slider slider;
        public override void Apply()
        {
            
        }

        public void SetCurrentsuboptionBySlider()
        {
            foreach (var t in subOptionList)
            {
                scriptableObj.sensitivity = (int)slider.value;
                print(slider.value);
                //print(scriptableObj.sensitivity);
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
                    scriptableObj.sensitivity = v;
                    currentSubOption = t;
                    currentSubOptionIndex = t.indexInList;
                    t.integerValue = v;
                    currentSubOption.integerValue = v;
                    print(currentSubOption.integerValue);
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

