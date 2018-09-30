using UnityEngine;

public class MainMenuController : MonoBehaviour {

   
    public GameObject level2Locked, level2, level3Locked, level3;

    private void Start()
    {
        ShowCorrectButtons();
    }

    private void ShowCorrectButtons()
    {
        if (GameController.instance.IsLevel2Unlocked() == 0)
        {
            level2Locked.gameObject.SetActive(true);
            level2.gameObject.SetActive(false);
        }

        if (GameController.instance.IsLevel2Unlocked() == 1)
        {
            level2Locked.gameObject.SetActive(false);
            level2.gameObject.SetActive(true);
        }

        if (GameController.instance.IsLevel3Unlocked() == 0)
        {
            level3Locked.gameObject.SetActive(true);
            level3.gameObject.SetActive(false);
        }

        if (GameController.instance.IsLevel3Unlocked() == 1)
        {
            level3Locked.gameObject.SetActive(false);
            level3.gameObject.SetActive(true);
        }
    }

    public void GoToLevel1()
    {
        SceneFader.instance.FadeIn("Gameplay");
    }

    public void GoToLevel2()
    {
        SceneFader.instance.FadeIn("Level2");
    }

    public void GoToLevel3()
    {
        SceneFader.instance.FadeIn("Level3");
    }


}
