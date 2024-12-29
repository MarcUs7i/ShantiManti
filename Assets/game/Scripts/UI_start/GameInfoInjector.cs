using UnityEngine;
using TMPro;

public class GameInfoInjector : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    private void Start()
    {
        if (!infoText)
        {
            Debug.LogError($"{nameof(GameInfoInjector)}: {nameof(infoText)} is not set!");
            return;
        }
        var buildInfo = Resources.Load<TextAsset>("BuildInfo");
        var buildDate = buildInfo ? buildInfo.ToString().Trim() : "undefined";

        var text = infoText.text.Replace("{gamever}", Application.version)
            .Replace("{unityver}", Application.unityVersion)
            .Replace("{builddate}", buildDate);
        
        infoText.text = text;
    }
}
