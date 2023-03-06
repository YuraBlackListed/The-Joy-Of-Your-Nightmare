using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace AkshayDhotre.GraphicSettingsMenu
{
    
    /// <summary>
    /// Controls the settings with the keyboard, changing the suboptions with the A/D or the Left/Right arrow keys.
    /// Works with the old input system.
    /// </summary>
    public class KeyboardMouseInput : MonoBehaviour
    {
        [Tooltip("Object which shows that the current object is selected, it can be a simple pointer next to the option" +
            "or an image overlay over the option")]
        public GameObject optionMarker;

        private bool selected = false;//is the mouse cursor hovering over this option or is the option selected by using the keyboard
        private Option myOption;//The option which will be controlled

        private EventSystem currentEventSystem;

        private void Awake()
        {
            myOption = GetComponent<Option>();
            currentEventSystem = EventSystem.current;
        }

        private void Update()
        {
            //Check if the menu is active then enable the keyboard controls
            if(IsMenuActive())
            {
                KeyboardControls();
            }
        }

        private void KeyboardControls()
        {
            //If the current option is selected we can cycle through the sub-options!
            if (selected)
            {
                if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    myOption.SelectNextSubOption();
                }
                else if (Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    myOption.SelectPreviousSubOption();
                }
            }
        }

        /// <summary>
        /// Checks if the graphic option menu is active
        /// </summary>
        /// <returns></returns>
        private bool IsMenuActive()
        {
            /// The controls works only if this method returns true
            //Gets the top most parent, and it the canvas component is enabled the menu is active!
            if (transform.root.GetComponent<Canvas>().enabled)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the current option as selected if val is true, and also assings it as currently selected object in the event system.
        /// </summary>
        /// <param name="val"></param>
        public void SetMarkerActive(bool val)
        {
            //This method is called by the event trigger component attached to the option item
            //If the mouse cursor is on the option the we enable the option marker
            optionMarker.SetActive(val);
            //By settings this to true we can use the keyboard to control the suboptions
            selected = val;

            //If the mouse cursor is on this option but the option is not selected in the EventSystem
            //The player the won't be able to use the Up/Down keys to change the option
            //So we select this option
            if (val == true && currentEventSystem.currentSelectedGameObject != this.gameObject)
            {
                currentEventSystem.SetSelectedGameObject(null);
                currentEventSystem.SetSelectedGameObject(this.gameObject);
            }
        }
    }
}

