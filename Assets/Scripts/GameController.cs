using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameController : MonoBehaviour
{
    public GameObject LevelPrefab_2x2;
    public GameObject LevelPrefab_3x3;
    public GameObject LevelPrefab_4x4;
    public GameObject LevelPrefab_5x5;

    public GameObject level;

    public Coins coinsPanel;

    public static int currentLevel = 0;
    public static bool gameIsFinished;

    public Popup popupWindow;

    public static List<Level> levels;

    public List<VideoClip> videos;
    public List<RenderTexture> textures;

    void Start()
    {
        levels = new List<Level>();
        //"size, fixed, countMove, time"
        levels.Add(new Level(2, 2, 10)); // 1
        levels.Add(new Level(2, 1, 10)); // 2

        levels.Add(new Level(3, 6, 15)); // 3
        levels.Add(new Level(3, 4, 15)); // 4
        levels.Add(new Level(3, 2, 20)); // 5
        levels.Add(new Level(3, 1, 20)); // 6

        levels.Add(new Level(4, 12, 25)); // 7
        levels.Add(new Level(4, 10, 25)); // 8
        levels.Add(new Level(4, 8, 30)); // 9
        levels.Add(new Level(4, 6, 35)); // 10
        levels.Add(new Level(4, 4, 35)); // 11
        levels.Add(new Level(4, 2, 40)); // 12
        levels.Add(new Level(4, 1, 40)); // 13

        levels.Add(new Level(5, 20, 45)); // 14
        levels.Add(new Level(5, 15, 45)); // 15
        levels.Add(new Level(5, 13, 50)); // 16
        levels.Add(new Level(5, 10, 50)); // 17
        levels.Add(new Level(5, 8, 55)); // 18
        levels.Add(new Level(5, 4, 60)); // 19
        levels.Add(new Level(5, 2, 60)); // 20
        levels.Add(new Level(5, 1, 60)); // 21
        SetCurrentLevel();
    }

    void FixedUpdate()
    {
        if (gameIsFinished && !Popup.isOn) // game is over -> popup
        {
            var coins = 100 * levels[currentLevel].levelTemplate;
            popupWindow.SetActivePopup(coins);
       
            gameIsFinished = true;
        }
    }

    public void SetCurrentLevel() // more logic
    {
        Destroy(level);
        currentLevel = PlayerPrefs.GetInt("level");
        switch (levels[currentLevel].levelTemplate)
        {
            case 2:
                level = Instantiate(LevelPrefab_2x2, transform.position, transform.rotation, gameObject.transform); // training
                break;
            case 3:
                level = Instantiate(LevelPrefab_3x3, transform.position, transform.rotation, gameObject.transform); // training
                break;
            case 4:
                level = Instantiate(LevelPrefab_4x4, transform.position, transform.rotation, gameObject.transform); // training
                break;
            case 5:
                level = Instantiate(LevelPrefab_5x5, transform.position, transform.rotation, gameObject.transform); // training
                break;
        }

        var pref = level.GetComponent<LevelPrefab>();
        pref.playfield.Shuffle();
        gameIsFinished = false;
    }
}

public class Level
{
    public int levelTemplate;
    public int fix;
    public int moveCount;

    public Level(int levelTemplate, int fix, int moveCount)
    {
        this.fix = fix;
        this.levelTemplate = levelTemplate;
        this.moveCount = moveCount;
    }
}
