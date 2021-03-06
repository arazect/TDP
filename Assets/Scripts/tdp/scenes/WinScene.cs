using Assets.Scripts.tdp.configuration;
using Assets.Scripts.tdp.constants;
using UnityEngine;

namespace Assets.Scripts.tdp.scenes {
    public class WinScene : MonoBehaviour {
        private Rect buttonStartRectangleSize;
        private Rect labelRectangleSize;

        public void Start() {
            buttonStartRectangleSize = new Rect(
                Configuration.ScreenWidth / 2 - 50,
                Configuration.ScreenHeight / 2 - 50, 
                100, 100);

            labelRectangleSize = new Rect(
                Configuration.ScreenWidth / 2 - 35, 
                Configuration.ScreenHeight / 2 - 90, 
                70, 30);
        }

        public void OnGUI() {
            GUI.Label(labelRectangleSize, "You win!");

            if (GUI.Button(buttonStartRectangleSize, "Play again")) {
                Application.LoadLevel(SceneNames.Main);
            }
        }
    }
}