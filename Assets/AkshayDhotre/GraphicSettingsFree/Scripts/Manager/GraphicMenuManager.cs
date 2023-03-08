using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AkshayDhotre.GraphicSettingsMenu
{
    [RequireComponent(typeof(GraphicSettingSaveManager))]
    /// <summary>
    /// Handles the toggling of menu and the saving/loading and applying of the graphic settings.
    /// </summary>
    public class GraphicMenuManager : MonoBehaviour
    {
        //Reference to the options in the scene
        public ResolutionOption resolutionOption;
        public ScreenmodeOption screenmodeOption;
        public QualityLevelOption qualityLevelOption;
        public SensitivityOption sensitivityOption;

        [Tooltip("The button on keyboard which when pressed will apply the graphic settings")]
        public KeyCode keyboardApplySettingsKey = KeyCode.Return;

        private GraphicSettingDataContainer dataToSave = new GraphicSettingDataContainer();//Data to be saved will be stored in this 
        private GraphicSettingDataContainer dataToLoad = new GraphicSettingDataContainer();//Data will be loaded into this 

        private GraphicSettingSaveManager graphicSettingSaveManager;

        
        private void Start()
        {

            graphicSettingSaveManager = GetComponent<GraphicSettingSaveManager>();

            //It is necessary to load the data in Start() rather than in Awake() because we generate the resolution suboption list
            //and the quality level suboption list in the awake function. So if we call this function in awake and apply the settings
            //the fallback suboption settings will be applied.

            if(graphicSettingSaveManager.FileExists())
            {
                Load();
                UpdateUIFromLoadedData();
                ApplySettings();
            }
            else
            {
                Debug.Log("New Save file Created!");
                Save();
            }
            
        }

        private void Update()
        {
            if(Input.GetKeyDown(keyboardApplySettingsKey))
            {
                ApplySettings();
            }
        }

        /// <summary>
        /// Called when the UI apply button is pressed
        /// </summary>
        public void OnApplyButtonPress()
        {
            ApplySettings();
        }

        /// <summary>
        /// Applies the settings and saves the new settings
        /// </summary>
        private void ApplySettings()
        {
            
            resolutionOption.Apply();
            screenmodeOption.Apply();
            qualityLevelOption.Apply();
            sensitivityOption.Apply();


            Save();
        }

        /// <summary>
        /// Get the values from the option, assign them in the GraphicSettingDataContainer and saves the data into a XML file
        /// </summary>
        public void Save()
        {
            //Assign values to dataToSave
            dataToSave.screenHeight = (int)resolutionOption.currentSubOption.vector2Value.y;
            dataToSave.screenWidth = (int)resolutionOption.currentSubOption.vector2Value.x;
            dataToSave.screenMode = screenmodeOption.currentSubOption.integerValue;
            dataToSave.qualityLevel = qualityLevelOption.currentSubOption.integerValue;
            dataToSave.sensitivityLevel = (int)sensitivityOption.currentSubOption.integerValue;

            graphicSettingSaveManager.SaveSettings(dataToSave);
        }

        /// <summary>
        /// Load the settings in dataToLoad(graphicSettingsDataContainer)
        /// </summary>
        public void Load()
        {
            graphicSettingSaveManager.LoadSettings(out dataToLoad);
        }

        /// <summary>
        /// Updates the UI suboption text and also sets the currentSubOption equal to the value from the loaded data
        /// </summary>
        private void UpdateUIFromLoadedData()
        {
            //so that the player will see the current settings on the menu
            resolutionOption.SetCurrentsuboptionByValue(new Vector2(dataToLoad.screenWidth, dataToLoad.screenHeight));
            screenmodeOption.SetCurrentsuboptionByValue(dataToLoad.screenMode);
            qualityLevelOption.SetCurrentsuboptionByValue(dataToLoad.qualityLevel);
            sensitivityOption.SetCurrentsuboptionByValue(dataToLoad.sensitivityLevel);
            
        }
    }
}

