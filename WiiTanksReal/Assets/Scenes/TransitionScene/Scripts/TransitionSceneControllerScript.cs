using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TransitionSceneControllerScript : MonoBehaviour
{

    public TMP_Text missionText;
    public TMP_Text enemyTanksText;

    public float screenTime;

    private GameMode gameMode;

    public Animator transition;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = GameObject.FindGameObjectWithTag("GameMode").GetComponent<GameMode>();

        int count = 0;
        for (int i = 0; i < gameMode.getNextLevelInfo().tankInfo.Count; i++)
        {
            count += gameMode.getNextLevelInfo().tankInfo[i].number;
        }

        missionText.text = "Mission: " + (gameMode.currentLevel + 1);
        enemyTanksText.text = "Enemy Tanks: " + count;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= screenTime)
        {
            StartCoroutine(SceneFadeToTransition());
        }

        timer += Time.deltaTime;
    }

    IEnumerator SceneFadeToTransition()
    {
        transition.SetTrigger("FadeOut");
        yield return new WaitForSeconds(.79f);
        gameMode.nextLevel();
    }
}
