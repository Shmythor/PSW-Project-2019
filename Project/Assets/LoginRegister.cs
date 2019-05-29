using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoginRegister : MonoBehaviour
{

    public Text inputText, registerUsername, notifyText;
   

    private string[] usernames;
    
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Update()
    {
        if (Input.GetKey("escape")) {            
            Application.Quit();
        }        
    }
    

   public void clickLogin() {
        //Get input text
        string username = inputText.text;
        
        //Get name lists      
        usernames = SaveLoad.getUserNames();

        bool check = false;
        //check username in list
        if(usernames==null || usernames.Length == 0) {
            check = true;
        } else {
            foreach (string item in usernames)
            {  
                if(item==username) {
                    UserNames.currentUsername = item;
                    SceneManager.LoadScene(1);
                }
            }
            check = true;
        }
        //Display error message if not exists || go to main menu if exists
        if(check) {
            notifyText.color = Color.red; 
            notifyText.text = "Usuario no registrado!";
        }  
   }

    public void clickRegister() {    
        usernames = SaveLoad.getUserNames();

        List<string> aux = new List<string>();

        if(usernames==null || usernames.Length == 0) {
            aux.Add(registerUsername.text);
        } else {
            aux.Add(registerUsername.text);
            foreach (string item in usernames)
            {
                aux.Add(item);
            }
        }  
        
        SaveLoad.addUserNames(aux.ToArray()); 

        notifyText.color = Color.green; 
        notifyText.text = "Usuario registrado con éxito!";  
   }
}
