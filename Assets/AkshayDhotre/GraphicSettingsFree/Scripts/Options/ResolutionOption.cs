using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AkshayDhotre.GraphicSettingsMenu
{
    /// <summary>
    /// Inherits from the Option class
    /// This class handles the resolution options
    /// </summary>
    public class ResolutionOption : Option
    {
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            GenerateResolutionSubOptions();

            //Assign the first element from the suboption list as the current option
            //We check if the current suboption is null because if we load the data from the xml file, the currentsuboption will be assigned 
            //and we don't want to reassign it.
            if (currentSubOption.name == "" && subOptionList.Count > 0)
            {
                currentSubOptionIndex = 0;
                currentSubOption = subOptionList[currentSubOptionIndex];
                
            }

            UpdateSuboptionText();
        }

        /// <summary>
        /// Applies the resolution settings
        /// from the xml file
        /// </summary>
        public override void Apply()
        {
            GraphicSettingHelperMethods.ChangeResolution((int)currentSubOption.vector2Value.x, (int)currentSubOption.vector2Value.y);
        }

        /// <summary>
        /// Gets the available resolution from Screen.resolutions and creates the suboptions list containing the availables resolutions
        /// </summary>
        private void GenerateResolutionSubOptions()
        {
            //Clear/Empty the list
            subOptionList.Clear();


            //Cycle through each resolution in Screen.resolutions and create a new suboption with the corresponding names, values and the index
            //Then add that suboption to the suboption list
            int i = 0;
            foreach(Resolution r in Screen.resolutions)
            {
                SubOption t = new SubOption();
                t.name = r.width.ToString() + "x" + r.height.ToString();
                t.vector2Value = new Vector2(r.width, r.height);
                t.indexInList = i;
                subOptionList.Add(t);
                i++;
            }
        }

        /// <summary>
        /// Goes through the list of the resolution and then finds the suboption which has value equal to the input value
        /// and assigns that sub option as the current sub option
        /// </summary>
        /// <param name="v"></param>
        public void SetCurrentsuboptionByValue(Vector2 v)
        {
            if(subOptionList.Count > 0)
            {
                foreach(var t in subOptionList)
                {
                    if(t.vector2Value == v)
                    {
                        currentSubOption = t;
                        currentSubOptionIndex = t.indexInList;
                        UpdateSuboptionText();
                        return;
                    }
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

