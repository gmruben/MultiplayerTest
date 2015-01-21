using UnityEngine;
using System.Collections;

public class Main : MonoBehaviour
{
	private bool inMatch = false;
	private TurnBasedGameData gameData;

	void Start()
	{
#if !UNITY_EDITOR
		GooglePlayManager.init();
		GooglePlayManager.authenticate();
#endif

		//ooglePlayManager.onTurnBasedMatchStarted += onTurnBasedMatchStarted;
		//GooglePlayManager.onGameDataReceived += onGameDataReceived;
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(0, 0, Screen.width, Screen.height * 0.20f), "START MATCH"))
		{
			GooglePlayManager.createWithInvitationScreen();
		}
		if (GooglePlayManager.isMyTurn)
		{
			if (GUI.Button(new Rect(0, Screen.height * 0.20f, Screen.width, Screen.height * 0.20f), "END TURN"))
			{
				gameData.numTurn++;
				GooglePlayManager.takeTurn(gameData);
			}
		}

		GUI.Label(new Rect(0, Screen.height * 0.40f, Screen.width, Screen.height * 0.10f), GooglePlayManager.playerName + " VS " + GooglePlayManager.opponentName);
		GUI.Label(new Rect(0, Screen.height * 0.50f, Screen.width, Screen.height * 0.10f), "AUTHENTICATED: " + GooglePlayManager.isAuthenticated);
		GUI.Label(new Rect(0, Screen.height * 0.60f, Screen.width, Screen.height * 0.10f), "IN MATCH: " + inMatch);

		if (gameData != null)
		{
			GUI.Label(new Rect(0, Screen.height * 0.70f, Screen.width, Screen.height * 0.10f), "NUM TURN: " + gameData.numTurn);
		}
	}

	private void onTurnBasedMatchStarted()
	{
		//GooglePlayManager.onTurnBasedMatchStarted -= onTurnBasedMatchStarted;

		gameData = new TurnBasedGameData();
		gameData.numTurn = 0;
	}

	private void onGameDataReceived(TurnBasedGameData gameData)
	{
		this.gameData = gameData;
	}
}