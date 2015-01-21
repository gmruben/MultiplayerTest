using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using GooglePlayGames.BasicApi.Multiplayer;

public class MainMenu : MonoBehaviour
{
	public InputField inputField;

	public UIButton initButton;
	public UIButton createMatchButton;
	public UIButton acceptFromInboxButton;
	public UIButton inviteFriendsButton;
	public UIButton showMatchListButton;

	public GameObject matchListMenuPrefab;
	private MatchListMenu matchListMenu;

	void Start()
	{
		init ();
	}

	public void init()
	{
		initButton.onClick += onInitButtonClick;
		createMatchButton.onClick += onCreateMatchButtonClick;
		acceptFromInboxButton.onClick += onAcceptFromInboxButtonClick;
		inviteFriendsButton.onClick += onInviteFriendsButtonClick;
		showMatchListButton.onClick += onShowMatchListButtonClick;

		SocialManager.onAuthenticationComplete += onAuthenticationComplete;
		SocialManager.onAuthenticationFailed += onAuthenticationFailed;

		//GooglePlayManager.onTurnBasedMatchStarted += onMatchStarted;
		GooglePlayManager.onGameDataReceived += onGameDataReceived;
	}

	private void setEnabled(bool isEnabled)
	{
		initButton.setActive (isEnabled);
		createMatchButton.setActive (isEnabled);
		acceptFromInboxButton.setActive (isEnabled);
		inviteFriendsButton.setActive (isEnabled);
		showMatchListButton.setActive (isEnabled);
	}

	private void openMatchListMenu()
	{
		setEnabled (false);

		matchListMenu = (GameObject.Instantiate(matchListMenuPrefab) as GameObject).GetComponent<MatchListMenu>();
		matchListMenu.init ();

		matchListMenu.backButton.onClick += onMatchListMenuBackButtonClick;
	}

	private void onMatchListMenuBackButtonClick()
	{
		setEnabled (true);
		GameObject.Destroy(matchListMenu.gameObject);
	}

	private void onInitButtonClick()
	{
		Debug.Log ("INIT GOOGLE PLAY");

		SocialManager.init ();
		SocialManager.authenticate();
	}

	private void onAuthenticationComplete()
	{
		Debug.Log ("AUTHENTICATION COMPLETE");
	}

	private void onAuthenticationFailed()
	{
		Debug.Log ("AUTHENTICATION FAILED");
	}

	private void onCreateMatchButtonClick()
	{
		GooglePlayManager.createQuickMatch ();
	}

	private void onAcceptFromInboxButtonClick()
	{
		Debug.Log ("ACCEPT FROM INBOX");
		GooglePlayManager.acceptFromInbox ();
	}

	private void onInviteFriendsButtonClick()
	{
		Debug.Log ("INVITE FRIENDS");
		GooglePlayManager.createWithInvitationScreen (onCreateMatch);
	}

	private void onShowMatchListButtonClick()
	{
		Debug.Log ("SHOW MATCH LIST");
		openMatchListMenu ();
	}

	private void onCreateMatch(bool success, TurnBasedMatch match)
	{
		if (success)
		{
			Debug.Log ("MATCH CREATED");

			//Create Match data to store locally
			MatchData matchData = new MatchData ();

			matchData.id = match.MatchId;
			matchData.state = MatchStateIds.Started;

			matchData.p1TeamName = inputField.text;

			//Create Game Data
			TurnBasedGameData gameData = new TurnBasedGameData ();
			gameData.numTurn = 1;

			matchData.gameData = gameData;

			MatchManager.addMatchData (matchData);
		}
		else
		{
			Debug.Log ("COULDN'T CREATE MATCH");
		}
	}

	private void onGameDataReceived(TurnBasedGameData gameData)
	{
		Debug.Log ("GAME DATA RECEIVED");
	}
}