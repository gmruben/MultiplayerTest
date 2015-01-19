using System;

/// <summary>
/// This class stores the data for one action. 
/// </summary>
[Serializable]
public class TurnActionData
{
	public string actionId;			//The Id of the action
	public int[] moveToIndexList;	//The index the player has moved through
}