//Back command .cs file
//Marks user as "not AFK"
//Loaded from server.cs
//Make changes to the messages or script here to change only the /back command

function serverCmdBack(%client)
{
	if(%client.status $= "here")
	{
		messageClient(%client,'',"<color:F660AB>You aren't AFK.");
	}
	else
	{
		messageAll('',"\c3" @ %client.name @ "<color:C8B560> is back!");
		%client.reason = "";
		%client.status = "here";
		if($StatusModv2::ChangeShapename == 1)
		{
			%client.player.setShapeName(%client.name, 8564862);
		}
	}
}
