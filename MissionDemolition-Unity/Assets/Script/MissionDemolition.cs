using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum GameMode
{
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; // a private Singleton

    [Header("Set in Inspector")]
    public Text uitLevel; // The UIText_Level Text
    public Text uitShots; // The UIText_Shots Text
    public Text uitButton; // The Text on UIButton_View
    public Vector3 castlePos; // The place to put castles
    public GameObject[] castles; // An array of the castles

    [Header("Set in Dynamically")]
    public int level; // The current level
    public int levelMax; // The number of levels
    public int shotsTaken;
    public GameObject castle; // The current Castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; // FollowCam mode

    // Start is called before the first frame update
    void Start()
    {
        S = this; // Define the Singleton

        levelMax = castles.Length;
        StartLevel();
    } // end start

    void StartLevel()
    {
        if (castle != null) // get rid of the old castle if one exists
        {
            Destroy(castle);
        } // end if

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject pTemp in gos)
        {
            Destroy(pTemp);
        }

        castle = Instantiate<GameObject>(castles[level]);
        castle.transform.position = castlePos;
        shotsTaken = 0;
        SwitchView("wShow Both"); // Reset the camera
        ProjectileLine.S.Clear();

        Goal.goalMet = false; // reset the goal

        UpdateGUI();

        mode = GameMode.playing;
    } // end StartLevel

    void UpdateGUI()
    {
        uitLevel.text = "Level: " + (level + 1) + "of" + levelMax;
        uitShots.text = "Shots Taken: " + shotsTaken;
    } // end UpdateGUI()

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        if ((mode == GameMode.playing) && Goal.goalMet)
        {
            mode = GameMode.levelEnd; // change mode to stop checking for level end
            SwitchView("Show Both"); // zoom out
            Invoke("NextLevel", 2f);
        } // end if
    } // end Update

    void NextLevel()
    {
        level++;
        if (level == levelMax)
        {
            level = 0;
        }
        StartLevel();
    } // end NextLevel()
    public void SwitchView(string eView = "")
    {
        if (eView == "")
        {
            eView = uitButton.text;
        } // end if 
        showing = eView;
        switch (showing)
        {
            case "Show Slingshot":
                FollowCam.POI = null;
                uitButton.text = "Show Castle";
                break;

            case "Show Castle":
                FollowCam.POI = S.castle;
                uitButton.text = "Show Both";
                break;

            case "Show Both":
                FollowCam.POI = GameObject.Find("ViewBoth");
                uitButton.text = "Show Slingshot";
                break;
        }
    }

    public static void ShotFired()
    {
        S.shotsTaken++;
    }
}
