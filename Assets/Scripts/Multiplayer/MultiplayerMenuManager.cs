﻿﻿﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MultiplayerMenuManager : MonoBehaviour
{

	public static MultiplayerMenuManager Instance { get; set; }

	public GameObject MainMenu, ConnectMenu, HostMenu;

	public GameObject HostAddressInputField;

	public GameObject ClientPrefab, ServerPrefab;

	void Start ()
	{
		Instance = this;

		MainMenu.SetActive ( true );

//		for ( int i = 0; i < this.MainMenu.transform.childCount; i++ )
//			this.MainMenu.transform.GetChild ( i ).gameObject.SetActive ( true );

		HostMenu.SetActive ( false );

		ConnectMenu.SetActive ( false );
		
		DontDestroyOnLoad ( gameObject );
	}

	void Update ()
	{
		
	}

	public void ACCESS_CONNECT_MENU_BUTTON ()
	{

		this.MainMenu.SetActive ( false );

		this.ConnectMenu.SetActive ( true );

	}

	public void ACCESS_HOST_MENU_BUTTON ()
	{
		
		this.MainMenu.SetActive ( false );

		this.HostMenu.SetActive ( true );

		try
		{

			GameObject serverGameObject = Instantiate ( this.ServerPrefab ) as GameObject;

			if ( serverGameObject != null )
			{

				Server s = serverGameObject.GetComponent <Server> ();

				s.Init ();

				GameObject clientGameObject = Instantiate ( ClientPrefab );
				
				clientGameObject.name = "Host";

				if ( clientGameObject != null )
				{
					Client c = clientGameObject.GetComponent <Client> ();

					c.IsHost = true;
					
					c.ClientName = "Host";
					
					c.ConnectToServer ( "localhost", 6321 ); // we're the server, connect to ourself
					// don't hardcode the port.

				}

			}

		} catch ( Exception e )
		{
			Debug.Log ( e.Message );
		}
		
	}

	public void CONNECT_TO_SERVER ()
	{
		 
		string hostAddress = this.HostAddressInputField.GetComponent <InputField> ().text;

		if ( hostAddress == null || hostAddress == "" )
			hostAddress = "localhost";

		try
		{

			GameObject clientGameObject = Instantiate ( ClientPrefab );

			clientGameObject.name = "Client";

			if ( clientGameObject != null )
			{
				Client c = clientGameObject.GetComponent <Client> ();
				
				c.IsHost = false;

				c.ClientName = "Client";
				
				c.ConnectToServer ( hostAddress, 6321 ); // don't hardcode the port.
				
				this.ConnectMenu.SetActive ( false );
				
			}
		} catch ( Exception e )
		{
			Debug.Log ( e.Message );
		}
		
	}

	public void BACK_BUTTON ()
	{
		MainMenu.SetActive ( true );

		HostMenu.SetActive ( false );

		ConnectMenu.SetActive ( false );

		Server s = FindObjectOfType <Server> ();
		if ( s != null )
		{
			s.server.Stop ();
			Destroy ( s.gameObject );
		}

		Client c = FindObjectOfType <Client> ();
		if ( c != null )
		{
			c.socket.Close ();
			Destroy ( c.gameObject );
		}
	}
	
	public void StartGame ()
	{
		MainMenu.SetActive ( false );

		HostMenu.SetActive ( false );

		ConnectMenu.SetActive ( false );

		SceneManager.LoadScene ( "__TEST__" );
	}
	
}