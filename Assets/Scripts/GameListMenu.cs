using UnityEngine;
using System.Collections;

public class GameListMenu : MonoBehaviour
{
	public UIButton initButton;
	public UIButton createMatchButton;
	public UIButton leaveMatchButton;
	public UIButton inviteFriendsButton;
	public UIButton achievementsButton;

	void Start()
	{
		init ();
	}

	public void init()
	{
		initButton.onClick += onInitButtonClick;
		createMatchButton.onClick += onCreateMatchButtonClick;
		leaveMatchButton.onClick += onLeaveMatchButtonClick;
		inviteFriendsButton.onClick += onInviteFriendsButtonClick;
		achievementsButton.onClick += onAchievementsButtonClick;

		SocialManager.onAuthenticationComplete += onAuthenticationComplete;
		SocialManager.onAuthenticationFailed += onAuthenticationFailed;
	}

	private void onInitButtonClick()
	{
		Debug.Log ("INIT GOOGLE PLAY");

		//GooglePlayManager.init();
		//GooglePlayManager.authenticate ();

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
		Debug.Log ("CREATE QUICK MATCH");
		GooglePlayManager.createQuickMatch ();
	}

	private void onLeaveMatchButtonClick()
	{
		Debug.Log ("LEAVE MATCH");
		GooglePlayManager.leave ();
	}

	private void onInviteFriendsButtonClick()
	{
		Debug.Log ("INVITE FRIENDS");
		GooglePlayManager.createWithInvitationScreen ();
	}

	private void onAchievementsButtonClick()
	{
		Debug.Log ("SHOW ACHIEVEMENTS");
		SocialManager.showAchievementsUI ();
	}
}