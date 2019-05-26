using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoginRegister : MonoBehaviour
{

    public Text inputText, registerUsername;
    private string path_usernames;
    
     /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
       path_usernames =  Application.persistentDataPath + "/usernames.list";
    }
    

   public void clickLogin() {
        string[] usernames; string username;
        //Get input text
        username = inputText.text;
        //Get name lists        
        if(File.Exists(path_usernames)) {
            BinaryFormatter formatter = new BinaryFormatter(); 
            FileStream stream = new FileStream(path_usernames, FileMode.OpenOrCreate);

            UserNamesSerializable data = formatter.Deserialize(stream) as UserNamesSerializable;
            usernames = data.usernames;
            stream.Close();     

            bool check = false;
            //check username in list
            if(usernames==null) {
                check = true;
            } else {
                foreach (string item in usernames)
                {
                    if(item==username) {
                        UserNames.currentUsername = username;                    
                        SceneManager.LoadScene(0);
                    }
                }
                check = true;
            }
            //Display error message if not exists || go to main menu if exists
            if(check) {
                Debug.Log("Eh tu, registrate!!");
            }       
        } else {
            Debug.LogError("Save file not found in path: ");            
        }

       
   }

    public void clickRegister() {
        BinaryFormatter formatter = new BinaryFormatter(); 
        FileStream stream = new FileStream(path_usernames, FileMode.OpenOrCreate);

        UserNamesSerializable data = formatter.Deserialize(stream) as UserNamesSerializable;
        string[] usernames = data.usernames;
        List<string> aux = new List<string>();
        foreach (string item in usernames)
        {
            aux.Add(item);
        }
        aux.Add(registerUsername.text);

        formatter.Serialize(stream, new UserNamesSerializable(aux.ToArray()));
        stream.Close();        
   }
}
