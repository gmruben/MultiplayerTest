using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchButton : MonoBehaviour
{
	public Text matchText;

	public UIButton sendTurnButton;
	public UIButton finishMatchButton;

	private MatchData matchData;

	public void init(MatchData matchData)
	{
		this.matchData = matchData;
		matchText.text = matchData.id + " - " + matchData.state;

		sendTurnButton.onClick += onSendTurnButtonClick;
		finishMatchButton.onClick += onFinishMatchButtonClick;
	}

	private void onSendTurnButtonClick()
	{
		Debug.Log ("SEND TURN: " + matchData.id);

		TurnBasedGameData gameData = new TurnBasedGameData();
		GooglePlayManager.takeTurn (matchData.gameData);
	}

	private void onFinishMatchButtonClick()
	{
		TurnBasedGameData gameData = new TurnBasedGameData();
		GooglePlayManager.finishMatch (gameData);
	}
}