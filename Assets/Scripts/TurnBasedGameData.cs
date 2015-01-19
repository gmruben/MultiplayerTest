using System;

[Serializable]
public class TurnBasedGameData
{
	public int numTurn;
	public TurnActionData[] actionDataList;		//List with all the actions in this turn
}