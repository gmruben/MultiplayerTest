using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchButton : MonoBehaviour
{
	public Text matchText;

	public UIButton sendTurnButton;
	public UIButton finishMatchButton;

	public void init(MatchData matchData)
	{
		matchText.text = matchData.id + " - " + matchData.state;

		sendTurnButton.onClick += onSendTurnButtonClick;
		finishMatchButton.onClick += onFinishMatchButtonClick;
	}

	private void onSendTurnButtonClick()
	{
		TurnBasedGameData gameData = new TurnBasedGameData();
		GooglePlayManager.takeTurn (gameData);
	}

	private void onFinishMatchButtonClick()
	{
		TurnBasedGameData gameData = new TurnBasedGameData();
		GooglePlayManager.finishMatch (gameData);
	}
}