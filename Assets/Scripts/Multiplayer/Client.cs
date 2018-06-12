﻿using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;
using System.Collections.Generic;

public class Client : MonoBehaviour
{

	public string ClientName;
	
	[NonSerialized]
	public bool IsHost;

	public int team;
	
	private bool isSocketReady;
	public TcpClient socket;
	private NetworkStream stream;
	private StreamWriter writer;
	private StreamReader reader;

	void Start ()
	{
		DontDestroyOnLoad ( gameObject );

		//MainController = GameObject.Find ( "MainController" ).GetComponent <Main_Controller> ();
	}
	
	public bool ConnectToServer ( string host, int port )
	{
		if ( this.isSocketReady )
			return false;

		try
		{
			this.socket = new TcpClient ( host, port );
			this.stream = this.socket.GetStream ();
			this.writer = new StreamWriter ( this.stream );
			this.reader = new StreamReader ( this.stream );

			this.isSocketReady = true;
		} catch ( Exception e )
		{
			Debug.Log ( "Socket error: " + e.Message );
		}

		return this.isSocketReady;
	}

	private void OnIncomingData ( string data )
	{
		Debug.Log ( "Client::OnIncomingData: " + data );

		string[] aData = data.Split ( '|' );

		switch ( aData [0] )
		{
			case "SWHO":
				Send ( "CWHO|" + this.ClientName + "|" + ( this.IsHost ? 1 : 0 ) );
				break;
			case "SCON":this.team = int.Parse ( aData [2] );
				break;
			case "SSTART":
				StartTheGame ();
				break;
			case "SACTION":
				ParseSACTION ( aData );
				break;
		}
	}

	void ParseSACTION ( string[] data )
	{
		int x1 = int.Parse ( data [1] );
		int y1 = int.Parse ( data [2] );
		int x2 = int.Parse ( data [3] );
		int y2 = int.Parse ( data [4] );

		string actionType = data [5];

		int x3 = int.Parse ( data [6] );
		int y3 = int.Parse ( data [7] );
		
		int actionId = int.Parse ( data [8] );
		
		Unit unit = BoardGenerator.GetUnitAtCoord ( x1, y1 );
		Tile destinationTile = BoardGenerator.tiles [BoardGenerator.CoordToIndex ( x2, y2 )];

		if ( unit == null )
			unit = GameplayManager.Instance.UnitSelected;
		
		bool didMove = unit.MoveTo ( destinationTile );

		Tile attackedTile = BoardGenerator.tiles [BoardGenerator.CoordToIndex ( x3, y3 )];

		int attackSucceeded = -1;
		
		switch ( actionType )
		{
			case "atk":
				attackSucceeded = unit.Attack ( attackedTile );
				break;
			case "atkspeone":
				attackSucceeded = unit.UseFirstAbility ( attackedTile );
				break;
			case "atkspetwo":
				attackSucceeded = unit.UseSecondAbility ();
				break;
			case "atkspethree":
				attackSucceeded = unit.UseThirdAbility ();
				break;
			case "compspe":
				attackSucceeded = unit.UseSpecialAbility ( attackedTile );
				break;
			case "obj":
				//unit.UseObject(attackedTile);
				break;
			case "NOTHING":
				break;
		}
		
		TurnManager.Instance.NextTurn ();
		
		GameplayManager.Instance.StartTurn ();
	}

	private void StartTheGame ()
	{
		MultiplayerMenuManager.Instance.StartGame ();
	}

	public void Send ( string data )
	{
		if ( !this.isSocketReady )
			return;

		Debug.Log ( "Client::Send: " + data );
		
		this.writer.WriteLine ( data );
		this.writer.Flush ();
	}
	
	void Update ()
	{

		if ( !this.isSocketReady )
			return;

		if ( this.stream.DataAvailable )
		{
			string data = this.reader.ReadLine ();

			if ( data != null )
				OnIncomingData ( data );
		}

	}

	private void OnApplicationQuit ()
	{
		CloseSocket ();
	}

	private void OnDisable ()
	{
		CloseSocket ();
	}
	
	private void CloseSocket ()
	{
		if ( !this.isSocketReady )
			return;

		this.writer.Close ();
		this.reader.Close ();
		this.socket.Close ();
		this.isSocketReady = false;
	}

}