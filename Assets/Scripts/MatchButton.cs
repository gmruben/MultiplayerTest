using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchButton : MonoBehaviour
{
	public Text matchText;

	public UIButton sendTurnButton;
	public UIButton leaveMatchButton;

	private MatchData matchData;

	public void init(MatchData matchData)
	{
		this.matchData = matchData;
		matchText.text = matchData.id + " - " + matchData.state;

		sendTurnButton.onClick += onSendTurnButtonClick;
		leaveMatchButton.onClick += onLeaveMatchButtonClick;
	}

	private void onSendTurnButtonClick()
	{
		Debug.Log ("SEND TURN: " + matchData.id);

		TurnBasedGameData gameData = new TurnBasedGameData();
		GooglePlayManager.takeTurn (matchData.gameData);
	}

	private void onLeaveMatchButtonClick()
	{
		Debug.Log ("LEAVE MATCH: " + matchData.id);

		TurnBasedGameData gameData = new TurnBasedGameData();
		GooglePlayManager.finishMatch (gameData);
	}
}