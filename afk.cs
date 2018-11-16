//AFK command .cs file
//Changes name, creates preference for said event, and contains the function
//Loaded from server.cs
//Make changes to the messages or script here to change only the /afk command

function serverCmdAfk(%client, %c1, %c2, %c3, %c4, %c5, %c6, %c7, %c8, %c9, %c10, %c11, %c12, %c13, %c14, %c15, %c16, %c17, %c18, %c19, %c20)
{
	if(%client.status $= "afk")
	{
		messageclient(%client,'',"<color:F660AB>Sorry, but you are already afk. Try using /back.");
	}
	else
	{
		if(%client.canStatus)
		{
			if(%c1 !$= "")
			{
				for(%a = 1; %a < 21; %a++)
				{
					if(%c[%a] !$= "")
					{
						%client.reason = %client.reason SPC %c[%a];
					}
				}
				%client.reason = stripMLControlChars(trin(%client.reason));
				messageAll('',"\c3" @ %client.name @ "<color:C8B560> is now AFK: <color:ADD8E6>" @ %client.reason);
				%client.status = "afk";
				%client.canStatus = 0;
				schedule(5000, 0, canstatusnow, %client);
				%client.player.setShapeName(%client.name SPC "(AFK)", 8564862);
			}
			else
			{
				%client.reason = "Away from Keyboard (no reason provided)";
				messageAll('',"\c3" @ %client.name @ "<color:C8B560> is now AFK: <color:ADD8E6>" @ %client.reason);
				%client.status = "afk";
				%client.canStatus = 0;
				schedule(5000, 0, canstatusnow, %client);
				%client.player.setShapeName(%client.name SPC "(AFK)", 8564862);
			}
		}
		else
		{
			messageClient(%client,'',"\c6---\c6Please do not spam this command. There is a 5 second delay between usages.");
		}
	}
}
