using System;
using UnityEngine;
using System.IO;
using System.Xml;
using System.Collections;

/*!
 * Read a XML file with a set of mathematical phrases.
 * ![phrases](image/phrases.png)
 */
public class ReadConf {
	string error = null;
	bool isOpen;
	XmlDocument doc;
	private int nObjects;

	/*! 
	 * Number of objects of the XML file.
	 */
	public int NObjects  		
	{ get{ return nObjects;}}  

	/*! 
	 * Error message.
	 */
	public string Error 		
	{ get{ return error;}}     

	/*!
	 * The XML file is open.
	 */
	public bool IsOpen 			
	{ get{ return isOpen;}}


	/*!
	 * Read a XML file.
	 */
	public ReadConf (string path) 
	{
		TextAsset genericAsset = (TextAsset)Resources.Load (path, typeof(TextAsset));

		if(genericAsset == null) {
			isOpen = false;
			nObjects = -1;
			Debug.Log("File not found: "+path);
		}
		else {
			isOpen = true;
			doc = new XmlDocument ();
			doc.LoadXml(genericAsset.text);
			XmlNodeList list = doc.DocumentElement.ChildNodes;
			nObjects = list.Count;	
		}
	}

	/*!
	 * Get the list of mathematical phrases. 
	 */
	public string [] GetPhrases() {
		if (isOpen) {
			XmlNodeList List = doc.SelectNodes("/Collection/phrase");
			string [] LP = new string[List.Count];
			for(int i = 0; i < List.Count; i++) {
				XmlNode n = List[i];
				string text = Convert.ToString(n.Attributes["desc"].Value);
				LP[i] = text;
			}
			return LP;	
		} 
		else
			return null;
	}

}
