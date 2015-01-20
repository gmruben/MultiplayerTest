using UnityEngine;
using System;
using System.Collections;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class SocialManager
{
	public static event Action onAuthenticationComplete;
	public static event Action onAuthenticationFailed;
	
	public static event Action onReportProgressComplete;
	public static event Action onReportProgressFailed;
	
	public static event Action onReportScoreComplete;
	public static event Action onReportScoreFailed;

	public static void init()
	{
		#if UNITY_ANDROID
		// recommended for debugging:
		//PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		//PlayGamesPlatform.Activate();

		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// enables saving game progress.
			.EnableSavedGames()
				// registers a callback to handle game invitations received while the game is not running.
				//.WithInvitationDelegate(<callback method>)
				// registers a callback for turn based match notifications received while the
				// game is not running.
				//.WithMatchDelegate(<callback method>)
				.Build();
		
		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		PlayGamesPlatform.Activate();

		#elif UNITY_IOS
		Social.Active = new UnityEngine.SocialPlatforms.GameCenter.GameCenterPlatform();
		#endif
	}
	
	public static void authenticate()
	{
		//Authenticate user
		Social.localUser.Authenticate(authenticationComplete);
	}
	
	private static void authenticationComplete(bool success)
	{
		if (success)
		{
			if (onAuthenticationComplete != null) onAuthenticationComplete();
		}
		else
		{
			if (onAuthenticationFailed != null) onAuthenticationFailed();
		}
	}
	
	public static void reportProgress(string achievementId, float progress)
	{
		Social.ReportProgress(achievementId, progress, reportProgressComplete);
	}
	
	private static void reportProgressComplete(bool success)
	{
		if (success)
		{
			if (onReportProgressComplete != null) onReportProgressComplete();
		}
		else
		{
			if (onReportProgressFailed != null) onReportProgressFailed();
		}
	}
	
	public static void submit(long score, string boardId)
	{
		Social.ReportScore(score, boardId, reportScoreComplete);
	}
	
	private static void reportScoreComplete(bool success)
	{
		if (success)
		{
			if (onReportScoreComplete != null) onReportScoreComplete();
		}
		else
		{
			if (onReportScoreFailed != null) onReportScoreFailed();
		}
	}
	
	public static void showAchievementsUI()
	{
		Social.ShowAchievementsUI();
	}
	
	public static void showLeaderboardUI()
	{
		Social.ShowLeaderboardUI();
	}
	
	public static bool isAuthenticated
	{
		get { return Social.localUser.authenticated; }
	}
}