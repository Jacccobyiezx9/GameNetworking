using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_InputField nameInputField;
    [SerializeField] private TMP_Dropdown colorDropdown;
    [SerializeField] private TMP_Dropdown teamDropdown;
    public string playerName;
    public Color playerColor;
    public int selectedTeam;

    public void ShowPanel()
    {
        panel.SetActive(true);
    } 

    public void HidePanel()
    {
        panel.SetActive(false);
    }
    public void SetPlayerStats()
    {

        playerName = nameInputField.text;

        switch (colorDropdown.value)
        {
            case 0: playerColor = Color.red; break;
            case 1: playerColor = Color.blue; break;
            case 2: playerColor = Color.green; break;
            case 3: playerColor = Color.purple; break;
            case 4: playerColor = Color.orange; break;
            case 5: playerColor = Color.black; break;
            default: playerColor = Color.white; break;
        }

        switch (teamDropdown.value)
        {
            case 0: selectedTeam = 1; break;
            case 1: selectedTeam = 2; break;
            default: selectedTeam = 1; break;
        }

        HidePanel();

        NetworkSessionManager.Instance.playerName = playerName;
        NetworkSessionManager.Instance.playerColor = playerColor;
        NetworkSessionManager.Instance.playerTeam = selectedTeam;
        NetworkSessionManager.Instance.StartButton();
    }
}
