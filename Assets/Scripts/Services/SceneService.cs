using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    public static SceneService Instance;

    void Awake()
    {
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
    
    public void PlayButton(){
        //TODO: on first boot of game we would start with new game mode play,
        // in subsequent play's we should should something 
        

        //TODO: add some flair here, make the transition look juicier
        //Load "first" scene
        SceneManager.LoadScene("Staging");
    }
}
