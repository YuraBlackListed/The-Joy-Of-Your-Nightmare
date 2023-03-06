using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AkshayDhotre.GraphicSettingsMenu
{
    /// <summary>
    /// Abstract option class ,this class can be used to create different options like resolution option , quality level option, vsync option etc.
    /// </summary>
    public abstract class Option : MonoBehaviour
    {
        
        [Tooltip("UI Text field for where the suboption name will be displayed")]
        public Text subOptionText;

        [Tooltip("Suboption list, for storing the suboptions")]
        public List<SubOption> subOptionList = new List<SubOption>();

        public SubOption currentSubOption = null;
        [HideInInspector] public int currentSubOptionIndex;

        [Tooltip("If there is any error in loading data, this option will be loaded as the default")]
        public SubOption fallBackOption = new SubOption();

        private void Awake()
        {
            if(fallBackOption.name == "")
            {
                Debug.LogWarning("Fallback option is invalid or not created");
            }
        }

        /// <summary>
        /// Abstract function, because each option will have different methods to apply the settings
        /// </summary>
        public abstract void Apply();

        /// <summary>
        /// Changes the suboptiontext value to the current suboption name
        /// </summary>
        public void UpdateSuboptionText()
        {
            if(currentSubOption != null)
            {
                subOptionText.text = currentSubOption.name;
            }
            else
            {
                Debug.LogError("Current suboption is null in : " + gameObject.name);
            }
        }

        /// <summary>
        /// Selects the next sub option in the list. If the current suboption is the last element in the list, the first element will be selected
        /// </summary>
        public void SelectNextSubOption()
        {
            currentSubOptionIndex = GetNextSuboptionIndex();
            currentSubOption = subOptionList[currentSubOptionIndex];
            UpdateSuboptionText();
        }

        /// <summary>
        /// Selects the next sub option in the list. If the current suboption is the first element in the list, the last element will be selected
        /// </summary>
        public void SelectPreviousSubOption()
        {
            currentSubOptionIndex = GetPreviousSubOptionIndex();
            currentSubOption = subOptionList[currentSubOptionIndex];
            UpdateSuboptionText();
        }

        /// <summary>
        /// Gets the next suboption index for cycling through the options
        /// </summary>
        /// <returns></returns>
        private int GetNextSuboptionIndex()
        {
            return GetNextValue(currentSubOptionIndex, subOptionList.Count);
        }

        /// <summary>
        /// Gets the previous suboption index for cycling through the options 
        /// </summary>
        /// <returns></returns>
        private int GetPreviousSubOptionIndex()
        {
            return GetPreviousValue(currentSubOptionIndex, subOptionList.Count);
        }

        /// <summary>
        /// Returns the next value under (maxValue - 1), returns 0 if the currentVal is equal to max val
        /// </summary>
        /// <param name="currentVal"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        private int GetNextValue(int currentVal, int maxVal)
        {
            if (currentVal >= maxVal - 1)
            {
                return 0;
            }
            else
            {
                return currentVal + 1;
            }
        }

        /// <summary>
        /// Returns the previous value under (maxValue - 1), returns maxVal if the currentVal is 0
        /// </summary>
        /// <param name="currentVal"></param>
        /// <param name="maxVal"></param>
        /// <returns></returns>
        private int GetPreviousValue(int currentVal, int maxVal)
        {
            if (currentVal == 0)
                return maxVal - 1;

            return currentVal - 1;
        }
    }
}

