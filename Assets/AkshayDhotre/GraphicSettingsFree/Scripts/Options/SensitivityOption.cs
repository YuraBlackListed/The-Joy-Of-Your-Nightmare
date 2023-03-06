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
            GraphicSettingHelperMethods.ChangeQualitySettings(currentSubOption.integerValue);
        }

        /// <summary>
        /// Goes through the list of the suboptions and then finds the suboption which has value equal to the input value
        /// and assigns that sub option as the current sub option
        /// </summary>
        /// <param name="v"></param>
        public void SetCurrentsuboptionBySlider()
        {
            foreach (var t in subOptionList)
            {
                if (t.integerValue == slider.value)
                {
                    currentSubOption = t;
                    currentSubOptionIndex = t.indexInList;
                    UpdateSuboptionText();
                    return;
                }
            }
            currentSubOption.integerValue = (int)slider.value;
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

