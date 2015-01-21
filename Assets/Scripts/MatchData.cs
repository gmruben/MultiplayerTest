using System;
using System.Json;

[Serializable]
public class MatchData
{
	public string id;
	public string state;
	
	public string p1TeamName;
	public string p2TeamName;
	
	public TurnBasedGameData gameData;
	
	public static MatchData parseJSonToMatchData(string json)
	{
		MatchData matchData = new MatchData();
		JsonObject jsonObject = JsonObject.Parse(json) as JsonObject;
		
		matchData.id = jsonObject["id"].ToString();
		matchData.state = jsonObject["state"].ToString();

		matchData.gameData = TurnBasedGameData.parseJSonToMatchData(jsonObject ["gameData"].ToString ());
		
		return matchData;
	}
	
	public static JsonObject parseMatchDataToJSon(MatchData matchData)
	{
		JsonObject jsonObject = new JsonObject();
		
		jsonObject.Add("id", matchData.id);
		jsonObject.Add("state", matchData.state);

		jsonObject.Add("gameData", TurnBasedGameData.parseMatchDataToJSon(matchData.gameData));
		
		return jsonObject;
	}
}

public class MatchStateIds
{
	public const string Started = "Started";
	public const string Finished = "Finished";
	public const string Cancelled = "Cancelled";
}