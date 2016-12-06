using UnityEngine;
using System.Collections;
using System;
public enum TypeMetrique{
	score,tempsSurvie,tempsImo,freqColisionMalus,nbBonusRate
}
public class Metrique {

	public Metrique(){
		
	}
	public void write_file(string toAdd)
	{
		System.IO.File.WriteAllText(@"Assets\Scripts\File\lol.txt", toAdd);
	}
}
