using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// import text mesh pro namespace
using TMPro;
// import UI namespace so can use buttons
using UnityEngine.UI;
// import namespace so can open levels
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
    // TODO: Encapsulation - protect public variables, use SerializeField?
     // Variable to store ref to TMP input game object
    public TMP_InputField inputField;
    // Variable to store ref to Start button
    public Button startButton;

        // Start is called before the first frame update
    void Start()
    {
        // Attach the OnStartButtonPressed method to button's onClick event
        startButton.onClick.AddListener(OnStartButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // function called when start button is pressed
    public void OnStartButtonPressed()
    {
        // make sure user has written username before starting game
        if(inputField.text != "")
        {
            // TODO: Create DataHandler singleton
            // Get text from the input field and store in userInput variable
            DataHandler.Instance.userName = inputField.text;
            Debug.Log("User input stored: " + DataHandler.Instance.userName);
            // Launch game
            SceneManager.LoadScene(1);
        }
    }

}
