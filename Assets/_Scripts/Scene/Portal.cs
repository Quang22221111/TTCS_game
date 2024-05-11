using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Portal : Colliderable
{
    public string sceneName;               
    private SceneTranslate SceneTranslate;  
    protected override void Start()
    {
        base.Start();
        if (SceneTranslate == null)
            SceneTranslate = GetComponentInChildren<SceneTranslate>();

        GetComponent<BoxCollider2D>().enabled = true;
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            GameManager.instance.SaveState();

            GetComponent<BoxCollider2D>().enabled = false;
            ChangeSceneTo(sceneName);         
        }
    }

    public void ChangeSceneTo(string sceneName)
    {
        SceneTranslate.ChangeToScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

