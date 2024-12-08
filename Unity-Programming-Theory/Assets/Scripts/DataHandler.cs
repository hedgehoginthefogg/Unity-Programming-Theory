using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// need to use this namespace to use save / load data functions
using System.IO;
// Need to only include this name space is compiling for editor, as isn't included in external builds so would cause an error
#if UNITY_EDITOR
using UnityEditor;
#endif


public class DataHandler : MonoBehaviour
{
    
    // TODO add get set and way to check score and only overwrite if has more points than current best score username
    public string bestScoreUserName = "Sar";
    
    
    // TODO add something to save best score with username

     // make it a static class so values stored in this class member will be shared by all instances of that class. Using get private set so other scripts and access but only this script can set the instance variable
    public static DataHandler Instance {get; private set;}

    // Make sure it's a singleton when script is loaded, before Start() is called
    private void Awake()
    {
    // If there is an instance, and it's not me, delete myself.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        // set Instance variable (Instance is just name of variable) to this script - so any reference to MainManager.Instance will refer to this script, and don't need to bother finding it and storing references (which is only necessary when there are multiple instances)
        Instance = this;
        // mark is not to be destroyed when the scene changes
        DontDestroyOnLoad(gameObject);
        
        // TODO decide where best to call these from
        // SaveBestScore();
        // LoadBestScore();
    }

        // SCRIPT FOR HANDLING USERNAMES
        // Make a static class with username validator function so can be accessed anywhere by using UserNameValidator.IsValidUsername(string)
    public static class UserNameValidator
    {
        public static bool IsValidUsername(string userName)
        {
            return !string.IsNullOrEmpty(userName) && userName.Length <= 3;
        }
    }
    // private backing field for public userName
    private string m_userName;
    // public property that accesses the private backing field. It's standard to use PascalCase for property names.
    public string UserName
    {
        get 
        {
            return m_userName; // getter returns backing field
        }
        set 
        {
            // check if the input value has more than 3 characters
        if (!UserNameValidator.IsValidUsername(value))
        {
            Debug.LogError("Your username must be between 1-3 characters.");
        }
        else
        {
            m_userName = value; // set backing field if value passed in to public field meets condition
            Debug.Log("Your username has been successfully stored: " + m_userName);
        }
        }
    }


    // Save username between games
    [System.Serializable]
    class SaveData
    {
        // private backing field
        private string m_savedBestScoreUserName;

        // public property that accesses the private backing field. It's standard to use PascalCase for property names.
        public string SavedBestScoreUserName
        {
            get 
            {
                return m_savedBestScoreUserName; // getter returns backing field
            }
            set 
            {
                // check if the input value has more than 3 characters
            if (!UserNameValidator.IsValidUsername(value))
            {
                Debug.LogError("Your username must be between 1-3 characters.");
            }
            else
            {
                m_savedBestScoreUserName = value; // set backing field if value passed in to public field meets condition
                Debug.Log("The best score username has been successfully updated: " + m_savedBestScoreUserName);
            }
            }
        }
    }

    // Method to save username data in json
    public void SaveBestScore()
    {
        // Make new instance of SaveData class and store in data variable
        SaveData data = new SaveData();
        // Use dot notation to access username attributes of SaveData class TODO: change to BestScoreUserName not just UserName
        data.SavedBestScoreUserName = bestScoreUserName;
        // convert data to json
        string json = JsonUtility.ToJson(data);
        // on the new json variable, use built in Unity method 'Application.persistentDataPath' that will give folder to save data that will persist between application reinstall or udpate and append to filename 'savefile.json'
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

     // method to load saved usename
    public void LoadBestScore()
    {
            // path to loaded file
            string path = Application.persistentDataPath + "/savefile.json";
        // avoid errors by only trying to access file if path exists
        if(File.Exists(path))
        {
            // save the data in the file to the json variable
            string json = File.ReadAllText(path);
            // parse the json data and save in data variable
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            // save the loaded TeamColor into gloabl  variable
            bestScoreUserName = data.SavedBestScoreUserName;
            Debug.Log(bestScoreUserName);
        }
    }

}

