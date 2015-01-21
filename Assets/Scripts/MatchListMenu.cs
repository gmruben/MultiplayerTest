using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MatchListMenu : MonoBehaviour
{
	public MatchButton matchButton;
	public UIButton backButton;

	public void init()
	{
		List<MatchData> matchList = MatchManager.retrieveMatchDataList ();

		if (matchList.Count == 0)
		{
			GameObject.Destroy(matchButton.gameObject);
		}
		else
		{
			matchButton.init (matchList [0]);

			for (int i = 1; i < matchList.Count; i++)
			{
				MatchButton button = (GameObject.Instantiate(matchButton) as GameObject).GetComponent<MatchButton>();
				matchButton.init (matchList [i]);

				button.transform.parent = matchButton.transform.parent;
				button.transform.localPosition = new Vector3(matchButton.transform.localPosition.x, matchButton.transform.localPosition.y - 150, matchButton.transform.localPosition.z);
			}
		}
	}
}