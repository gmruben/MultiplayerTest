using UnityEngine;
using System.Json;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class MatchManager
{
	private static List<MatchData> matchDataList = new List<MatchData>();
	
	public static List<MatchData> retrieveMatchDataList()
	{
		if (File.Exists(Application.persistentDataPath + "/matches.json"))
		{
			using(FileStream fs = new FileStream(Application.persistentDataPath + "/matches.json", FileMode.Open))
			{
				BinaryReader fileReader = new BinaryReader(fs);
				
				matchDataList = retrieveMatchDataListFromJson(fileReader.ReadString());
				fs.Close();
			}
		}

		return matchDataList;
	}
	
	public static void storeMatchDataList()
	{
		using(FileStream fs = new FileStream(Application.persistentDataPath + "/matches.json", FileMode.Create))
		{
			BinaryWriter fileWriter = new BinaryWriter(fs);
			
			fileWriter.Write(storeMatchDataListFromJson(matchDataList));
			fs.Close();
		}
	}

	public static void addMatchData(MatchData matchData)
	{
		matchDataList.Add(matchData);
	}

	private static List<MatchData> retrieveMatchDataListFromJson(string data)
	{
		List<MatchData> dataList = new List<MatchData>();

		JsonArray jsonArray = JsonArray.Parse(data) as JsonArray;
		for (int i = 0; i < jsonArray.Count; i++)
		{
			MatchData matchData = MatchData.parseJSonToMatchData(jsonArray[i]);
			dataList.Add(matchData);
		}
		return dataList;
	}

	private static string storeMatchDataListFromJson(List<MatchData> dataList)
	{
		JsonArray jsonArray = new JsonArray();
		for (int i = 0; i < dataList.Count; i++)
		{
			JsonObject jsonData = MatchData.parseMatchDataToJSon(dataList[i]);
			jsonArray.Add(jsonData);
		}
		return jsonArray.ToString();
	}
}