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
     // make it a static class so values stored in this class member will be shared by all instances of that class. Using get private set so other scripts and access but only this script can set the instance variable
    public static DataHandler Instance {get; private set;}


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
        if (value.Length > 3 || value.Length == 0)
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
    }
}

