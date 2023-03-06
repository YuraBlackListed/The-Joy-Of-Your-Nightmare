using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AkshayDhotre.GraphicSettingsMenu
{
    /// <summary>
    /// Handles the saving and loading of the graphic settings
    /// The data is saved when the apply button is pressed and loaded when the game starts
    /// </summary>
    public class GraphicSettingSaveManager : MonoBehaviour
    {
        [Tooltip("If kept empty, file will be stored in Application.persistentDataPath. Include / at the end")]
        public string filePath;

        [Tooltip("if kept empty, file name will be MyGraphicSettings.xml")]
        public string fileName;

        //Use this when there is error in loading the saved data
        private GraphicSettingDataContainer defaultDataContainer = new GraphicSettingDataContainer();

        private void Awake()
        {
            if(filePath == "")
            {
                filePath = Application.persistentDataPath + "/";
            }

            if(!filePath.EndsWith("/"))
            {
                Debug.LogError("File path should end with forward slash - / ");
            }
                

            if(fileName == "")
            {
                fileName = "MyGraphicSettings.xml";
            }

            //Initialize the default data container
            defaultDataContainer.screenHeight = 768;
            defaultDataContainer.screenWidth = 1024;
            defaultDataContainer.screenMode = 0;
            defaultDataContainer.qualityLevel = 0;

        }

        /// <summary>
        /// Save the settings using the XmlManager
        /// </summary>
        /// <param name="dataToSave"></param>
        public void SaveSettings(GraphicSettingDataContainer dataToSave)
        {
            XmlManager<GraphicSettingDataContainer>.Save(dataToSave, filePath, fileName);
        }

        /// <summary>
        /// Check if the file exists, if yes the it is assigned to dataToLoad
        /// else defaultDataContainer is assigned to dataToLoad and then saved to create the file
        /// </summary>
        /// <param name="dataToLoad"></param>
        public void LoadSettings(out GraphicSettingDataContainer dataToLoad)
        {
            GraphicSettingDataContainer tempData = XmlManager<GraphicSettingDataContainer>.Load(filePath + fileName, new GraphicSettingDataContainer());
            dataToLoad = tempData;
        }


        /// <summary>
        /// Check if the file exists
        /// </summary>
        /// <returns></returns>
        public bool FileExists()
        {
            if (XmlManager<GraphicSettingDataContainer>.FileExists(filePath + fileName))
            {
                return true;
            }

            Debug.LogWarning("File does not exist in " + filePath + fileName);
            return false;
        }
    }
}

