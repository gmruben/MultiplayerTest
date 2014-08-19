using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Util : MonoBehaviour
{
	//Converts an object to a byte array
	public static byte[] ObjectToByteArray(object obj)
	{
		if(obj == null)
		{
			return null;
		}

		BinaryFormatter bf = new BinaryFormatter();
		MemoryStream ms = new MemoryStream();
		bf.Serialize(ms, obj);

		return ms.ToArray();
	}
		
	//Converts a byte array to an object
	public static object ByteArrayToObject(byte[] arrBytes)
	{
		MemoryStream memStream = new MemoryStream();
		BinaryFormatter binForm = new BinaryFormatter();

		memStream.Write(arrBytes, 0, arrBytes.Length);
		memStream.Seek(0, SeekOrigin.Begin);

		object obj = (object) binForm.Deserialize(memStream);

		return obj;
	}
}