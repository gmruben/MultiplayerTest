using System;
using System.Json;

[Serializable]
public class TurnBasedGameData
{
	public int numTurn;
	//public TurnActionData[] actionDataList;		//List with all the actions in this turn

	public static TurnBasedGameData parseJSonToMatchData(string json)
	{
		TurnBasedGameData gameData = new TurnBasedGameData();
		JsonObject jsonObject = JsonObject.Parse(json) as JsonObject;
		
		gameData.numTurn = int.Parse(jsonObject["numTurn"].ToString());
		
		return gameData;
	}

	public static JsonObject parseMatchDataToJSon(TurnBasedGameData gameData)
	{
		JsonObject jsonObject = new JsonObject();
		
		jsonObject.Add("numTurn", gameData.numTurn);
		
		return jsonObject;
	}
}