using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    private UIDocument _uiDocument;
    // Start is called before the first frame update
    void Start()
    {
        _uiDocument = GetComponent<UIDocument>();
        _uiDocument.rootVisualElement.Q<Button>("ExitButtonBtn").clicked += ExitClick;

    }

    private void ExitClick()
    {
        Application.Quit(0);
    }

}
