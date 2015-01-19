using UnityEngine;
using System.Collections;

public class GameListMenu : MonoBehaviour
{
	public UIButton initButton;
	public UIButton createMatchButton;
	public UIButton leaveMatchButton;
	public UIButton inviteFriendsButton;

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
	}

	private void onInitButtonClick()
	{
		Debug.Log ("INIT GOOGLE PLAY");

		GooglePlayManager.init();
		GooglePlayManager.authenticate ();
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
}