using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.InputSystem.InputAction;

namespace Components
{
    public class DeveloperConsoleInput : MonoBehaviour
    {
        [Header("Console")]
        public GameCmdFactory cmdFactory;
        [SerializeField] private string prefix = string.Empty;
        [SerializeField] private ConsoleCommand[] commands = new ConsoleCommand[0];

        [Header("UI")]
        public GameObject uiCanvas;
        public TMP_InputField inputField;
        private float pauseTimeScale;

        private static DeveloperConsoleInput instance;

        private void Awake()
        {
            if(instance != null && instance != this)
            {
                Destroy(gameObject);
                return;
            }  

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        void Toggle()
        {
            // Abre la UI accediendo al evento context developer
            //if(!context.action.triggered){return;}

            if(uiCanvas.activeSelf)
            {   
                Time.timeScale = pauseTimeScale;
                uiCanvas.SetActive(false);
            } 
            else 
            {
                pauseTimeScale = Time.timeScale;
                Time.timeScale = 0;
                uiCanvas.SetActive(true);
                inputField.ActivateInputField();
            }
        }

        public void EnterProcessCommand(string inputValue)
        {
            cmdFactory.PerfomConsole(inputValue, prefix, commands).Execute();
            inputField.text = string.Empty;
        }

        void Update()
        {
            if(Input.touchCount > 2)
            {
                var touch = Input.GetTouch(2);
                if(touch.phase == TouchPhase.Ended)
                {
                    Toggle();
                }
            }
        }
    }

}
