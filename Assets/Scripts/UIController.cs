using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController Instance;

    [Header("REGISTER AND LOGIN UI SCREENS")]
    public Canvas FullDashboardUI;
    public RectTransform FullDashboard;
    public RectTransform ContractsUI;
    public RectTransform DronesUI;
    public RectTransform FieldUI;
    public RectTransform WeatherUI;
    public Canvas LogInAndRegisterScreen;
    public RectTransform LoginUI, RegisterUI, LogAndRegInitialButtonsScreen;

    [Header("LOG INFO")]
    [SerializeField]
    Text LogText;
    [SerializeField]
    Canvas LogPanel;

    private void Awake()
    {
        Instance = this;
    }


    #region RegisterAndLogin

    public void OpenRegister()
    {
        LogAndRegInitialButtonsScreen.DOAnchorPos(new Vector2(-800, 0), 0.4f);
        RegisterUI.DOAnchorPos(new Vector2(0, 7.8f), 0.4f);
    }

    public void OpenLogin()
    {
        LogAndRegInitialButtonsScreen.DOAnchorPos(new Vector2(800, 0), 0.4f);
        LoginUI.DOAnchorPos(new Vector2(0, 7.8f), 0.4f);
    }

    public void CloseRegister()
    {
        LogAndRegInitialButtonsScreen.DOAnchorPos(new Vector2(0, 0), 0.4f);
        RegisterUI.DOAnchorPos(new Vector2(-800, 7.8f), 0.4f);
    }

    public void CloseLogin()
    {
        LogAndRegInitialButtonsScreen.DOAnchorPos(new Vector2(0, 0), 0.4f);
        LoginUI.DOAnchorPos(new Vector2(800, 7.8f), 0.4f);
    }

    #endregion

    #region FullDashboard

    public void OpenFullDasobhard()
    {
        LogInAndRegisterScreen.enabled = false;
        // here the wait time needs to respond to the real wait time later when the data will be loaded from the db
        DisplayLogText(2f, "Loading, please wait");
        StartCoroutine(OpenDashboardAfterLogIn(2f,FullDashboard,true));
    }


    /// <summary>
    /// Activates UI overtime
    /// </summary>
    /// <returns></returns>
    IEnumerator OpenDashboardAfterLogIn(float waitTime, RectTransform Screen, bool ScreenVisibility)
    {
        yield return new WaitForSeconds(waitTime);
        FullDashboardUI.enabled = true;
        Screen.DOAnchorPos(new Vector2(0f, -159f), 0.5f);
    }

    #endregion

    #region ContractUI

    public void OpenContractsUI()
    {
        ContractsUI.DOAnchorPos(new Vector2(0, 0), 0.4f);
    }

    public void CloseContractUI()
    {
        ContractsUI.DOAnchorPos(new Vector2(0, -1400), 0.4f);
    }

    #endregion

    #region DroneUI

    public void OpenDroneUI()
    {

        DronesUI.DOAnchorPos(new Vector2(0, 0), 0.4f);

    }
    public void CloseDronetUI()
    {
        DronesUI.DOAnchorPos(new Vector2(0, -1400), 0.4f);
    }

    #endregion

    #region FieldUI

    public void OpenFieldUI()
    {
        FieldUI.DOAnchorPos(new Vector2(0, 0), 0.4f);
    }
    public void CloseFieldUI()
    {
        FieldUI.DOAnchorPos(new Vector2(0, -1400), 0.4f);
    }

    #endregion

    #region WeatherUI

    public void OpenWeatherUI()
    {
        WeatherUI.DOAnchorPos(new Vector2(0, 0), 0.4f);
    }
    public void CloseWeatherUI()
    {
        WeatherUI.DOAnchorPos(new Vector2(0, -1400), 0.4f);
    }


    #endregion

    /// <summary>
    /// Log info
    /// </summary>
    /// <param name="waitTime"></param>
    /// <param name="textDescription"></param>
    public void DisplayLogText(float waitTime, string textDescription)
    {
        StartCoroutine(LogTextCoroutine(waitTime, textDescription));
    }

    /// <summary>
    /// Log info coroutine
    /// </summary>
    /// <param name="waitTime"></param>
    /// <param name="textDescription"></param>
    IEnumerator LogTextCoroutine(float waitTime, string textDescription)
    {
        LogPanel.enabled = true;
        LogText.text = textDescription;
        yield return new WaitForSeconds(waitTime);
        LogText.text = "";
        LogPanel.enabled = false;

    }


}
