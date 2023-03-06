using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AkshayDhotre.GraphicSettingsMenu
{
    /// <summary>
    /// Contains the name of the subOption and its value
    /// For example : The option of Resolution will contain suboptions with
    /// vector2 values like (1920,1080) and name as "1920x1080"
    /// </summary>
    [System.Serializable]
    public class SubOption
    {
        [Tooltip("The name of the suboption that the user will see Ex 1920x1080, Off,On etc")]
        public string name;
       
        [Tooltip("The integer value corresponding to the name Ex Off = 0, On = 1")]
        public int integerValue = 0;

        [Tooltip("The index value of this option in the suboption list, created in option class")]
        public int indexInList;//Will be used when loading the data from the xml file


        [Tooltip("The vector2 value specially for the resolution")]
        public Vector2 vector2Value = Vector2.zero;

        public bool boolVal = false;
    }
}

