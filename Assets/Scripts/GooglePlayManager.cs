using UnityEngine;
using System;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.Multiplayer;
using UnityEngine.SocialPlatforms;

public class GooglePlayManager : MonoBehaviour
{
	private const int numOpponents = 1;

	public static Action<TurnBasedMatch> onTurnBasedMatchStarted;
	public static Action<TurnBasedGameData> onGameDataReceived;

	private static TurnBasedMatch turnBasedMatch;
	private static Invitation incomingInvitation;

	private static TurnBasedGameData gameData;

	public static void init()
	{
		//Register an invitation delegate
		PlayGamesPlatform.Instance.RegisterInvitationDelegate(onInvitationReceived);
		//Register a match delegate
		PlayGamesPlatform.Instance.TurnBased.RegisterMatchDelegate(onMatchReceived);
	}

	public static void authenticate()
	{
		//Authenticate user
		//Social.localUser.Authenticate(onAuthenticationComplete);
	}

	//Called when an invitation is received
	private static void onInvitationReceived(Invitation invitation, bool shouldAutoAccept)
	{
		if (shouldAutoAccept)
		{
			// Invitation should be accepted immediately. This happens if the user already
			// indicated (through the notification UI) that they wish to accept the invitation,
			// so we should not prompt again.
			//ShowWaitScreen();
			PlayGamesPlatform.Instance.TurnBased.AcceptInvitation(invitation.InvitationId, onMatchStarted);
		}
		else
		{
			// The user has not yet indicated that they want to accept this invitation.
			// We should *not* automatically accept it. Rather we store it and 
			// display an in-game popup:
			incomingInvitation = invitation;
		}
	}

	//Called when a match is received
	private static void onMatchReceived(TurnBasedMatch match, bool shouldAutoLaunch)
	{
		if (shouldAutoLaunch)
		{
			// if shouldAutoLaunch is true, we know the user has indicated (via an external UI)
			// that they wish to play this match right now, so we take the user to the
			// game screen without further delay:
			onMatchStarted(true, match);
		}
		else
		{
			// if shouldAutoLaunch is false, this means it's not clear that the user
			// wants to jump into the game right away (for example, we might have received
			// this match from a background push notification). So, instead, we will
			// calmly hold on to the match and show a prompt so they can decide
			turnBasedMatch = match;
		}
	}

	private static void onAuthenticationComplete(bool success)
	{
		//Register an invitation delegate
		PlayGamesPlatform.Instance.RegisterInvitationDelegate(onInvitationReceived);
		//Register a match delegate
		PlayGamesPlatform.Instance.TurnBased.RegisterMatchDelegate(onMatchReceived);
	}

	public static void createQuickMatch()
	{
		PlayGamesPlatform.Instance.TurnBased.CreateQuickMatch(numOpponents, numOpponents, 0, onMatchStarted);
	}

	public static void createWithInvitationScreen(Action<bool, TurnBasedMatch> onMatchStartAction)
	{
		PlayGamesPlatform.Instance.TurnBased.CreateWithInvitationScreen (numOpponents, numOpponents, 0, onMatchStartAction); // onMatchStarted);
	}

	public static void acceptFromInbox()
	{
		PlayGamesPlatform.Instance.TurnBased.AcceptFromInbox(onMatchStarted);
	}

	public static void takeTurn(TurnBasedMatch turnBasedMatch, TurnBasedGameData gameData)
	{
		//Convert the game data to byte array
		byte[] matchData = Util.ObjectToByteArray((object) gameData);
		//This indicates whose turn is next (a participant ID)
		string whoIsNext = turnBasedMatch.Participants[1].ParticipantId;

		PlayGamesPlatform.Instance.TurnBased.TakeTurn(turnBasedMatch, matchData, whoIsNext, onTakeTurnComplete);
	}

	private static void onTakeTurnComplete(bool success)
	{
		if (success)
		{
			Debug.Log("TAKE TURN SUCCESS");
		}
		else
		{
			Debug.Log("TAKE TURN FAIL");
		}
	}

	public static void leave(TurnBasedMatch turnBasedMatch)
	{
		PlayGamesPlatform.Instance.TurnBased.Leave(turnBasedMatch, onLeaveComplete);
	}

	private static void onLeaveComplete(bool success)
	{
		if (success)
		{
			// turn successfully submitted!
		}
		else
		{
			// show error
		}
	}

	public static void finishMatch(TurnBasedGameData gameData)
	{
		//Convert the game data to byte array
		byte[] matchData = Util.ObjectToByteArray((object) gameData);
		//define the match's outcome
		MatchOutcome matchOutcome = new MatchOutcome();

		//matchOutcome.SetParticipantResult(turnBasedMatch.Participants[0].ParticipantId, 0);
		//matchOutcome.SetParticipantResult(turnBasedMatch.Participants[1].ParticipantId, 1);

		PlayGamesPlatform.Instance.TurnBased.Finish(turnBasedMatch, matchData, matchOutcome, onMatchFinished);
	}

	private static void onMatchFinished(bool success)
	{
		if (success)
		{
			Debug.Log("MATCH FINISHED SUCCESS");
		}
		else
		{
			Debug.Log("MATCH FINISHED FAIL");
		}
	}

	private static void onMatchStarted(bool success, TurnBasedMatch match)
	{
		if (success)
		{
			Debug.Log("MATCH STARTED SUCCESS");

			turnBasedMatch = match;

			if (match.Data != null)
			{
				gameData = (TurnBasedGameData) Util.ByteArrayToObject(match.Data);
				if (onGameDataReceived != null) onGameDataReceived(gameData);
			}
			else
			{
				if (onTurnBasedMatchStarted != null) onTurnBasedMatchStarted(match);
			}

			// I can only make a move if the match is active and it's my turn!
			bool canPlay = (turnBasedMatch.Status == TurnBasedMatch.MatchStatus.Active && turnBasedMatch.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn);
		}
		else
		{
			Debug.Log("MATCH STARTED FAIL");
		}
	}

	public static bool isAuthenticated
	{
		get { return Social.localUser.authenticated; }
	}

	public static bool isMyTurn
	{
		get
		{ 
			if (turnBasedMatch == null) return false;
			else return turnBasedMatch.TurnStatus == TurnBasedMatch.MatchTurnStatus.MyTurn;
		}
	}

	public static string playerName
	{
		get
		{
			if (turnBasedMatch == null) return "";
			else return turnBasedMatch.Participants[0].DisplayName;
		}
	}
	public static string opponentName
	{
		get
		{
			if (turnBasedMatch == null) return "";
			else return turnBasedMatch.Participants[1].DisplayName;
		}
	}
}