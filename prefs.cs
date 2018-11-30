//Separate file for prefs just for organization alongside commands for managing said prefs (just for organization)
//When adding new stuff that utilizes prefs, just add the pref in this file and its associated command

$PrefManagerEnabled = if(isFile("Add-Ons/System_ReturnToBlockland/server.cs") || isFile("Add-Ons/System_BlocklandGlass/server.cs"))

if($PrefManagerEnabled = 1)
{
	if(!$RTB::RTBR_ServerControl_Hook)
	{
		RTB_registerPref("Announce AFK","Status Mod v2","StatusModv2::AnnounceAFK","bool","Server_StatusModv2",1,0,0);
		RTB_registerPref("Announce Time (Seconds)","Status Mod v2","StatusModv2::LoopTime","int 60 1200","StatusModv2",300,0,0);
		RTB_registerPref("Change Shapenames When AFK?","Status Mod v2","StatusModv2::ChangeShapename","bool","Server_StatusModv2",1,0,0);
		echo("[StatusModv2] Preferences registered successfully.");
	}
}
else
{
	$StatusModv2::AnnounceAFK = 1;
	$StatusModv2::LoopTime = 300;
	$StatusModv2::ChangeShapename = 1;
	echo("[StatusModv2] Blockland Glass / Return to Blockland not found, values for commands set");
}

function serverCmdAnnounceAFK(%client, %value)
{
	if(%client.isSuperAdmin)
	{
		switch ($StatusModv2::AnnounceAFK)
		{
			case 1:
				$StatusModv2::AnnounceAFK = 0;
				if(isObject(BrickRotateSound))
					serverPlay2D(BrickRotateSound);
				announce("\c3" @ %client.name @ " \c6has \c4disabled \c3AFK Announcements\c6.");
			case 0:
				$StatusModv2::AnnounceAFK = 1;
				if(isObject(BrickRotateSound))
					serverPlay2D(BrickRotateSound);
				announce("\c3" @ %client.name @ " \c6 has \c4enabled \c3AFK Announcements\c6.");
			default:
				messageClient(%client,'',"\c6That isn't a valid value! Use\c3 1 \c6for true or\c3 0 \c6for false.");
		}
	}
	else
	{
		messageClient(%client,'',"\c6You must be a \c3Super Admin\c6 to use this command.");
	}
}

function serverCmdAnnounceTime(%client, %value)
{
	if(%client.isSuperAdmin)
	{
		if(%value == $StatusModv2::LoopTime)
		{
			messageClient(%client,'',"\c6Sorry, but the time you put in is the same value as the current preference's variable.");
		}
		else
		{
			if(%value >= 60 || %value <= 1200)
			{
				$StatusModv2::LoopTime = %value;
				%minuteval = %value * 1000;
				announce("\c3" @ %client.name @ " \c6has set the AFK announce time to \c4" @ %value @ " \c6 seconds, or \c4" @ %minuteval @ "\c6 per announcement.");
			}
		}
		else
		{
			if(%value <= 61)
			{
				messageClient(%client,'',"\c6That value is too low! Determine the amount of time (in seconds) by multiplying your desired time (in minutes) by 60. The minimum time per announcement is 60 seconds.");
				messageClient(%client,'',"\c7For example, if you wanted each announcement to be 5 minutes apart, multiply\c6 5 (minutes) \c7by\c6 60 (seconds) \c7and use the result \c7(300) \c7as the desired time.");
			}
		}
		else
		{
			if(%value >= 1201)
			{
				messageClient(%client,'',"\c6That value is too high! Determine the amount of time (in seconds) by multiplying your desired time (in minutes) by 60. The maximum time per announcement is 1200 seconds");
				messageClient(%client,'',"\c7For example, if you wanted each announcement to be 5 minutes apart, multiply\c6 5 (minutes) \c7by\c6 60 (seconds) \c7and use the result \c7(300) \c7as the desired time.");
			}
	}
	else
	{
		messageClient(%client,'',"\c6You must be a \c3Super Admin\c6 to use this command.");
	}
}

function serverCmdAFKNameChanges(%client, %value)
{
	if(%client.isSuperAdmin)
	{
		switch ($StatusModv2::ChangeShapename)
		{
			case 1:
				$StatusModv2::ChangeShapename = 0;
				if(isObject(BrickRotateSound))
					serverPlay2D(BrickRotateSound);
				announce("\c3" @ %client.name @ " \c6has \c4disabled \c3shapename changes\c6 when someone goes AFK.");
			case 0:
				$StatusModv2::ChangeShapename = 1;
				if(isObject(BrickRotateSound))
					serverPlay2D(BrickRotateSound);
				announce("\c3" @ %client.name @ " \c6has \c4enabled \c3shapename changes\c6 when someone goes AFK.");
			default:
				messageClient(%client,'',"\c6That isn't a valid value! Use\c3 1 \c6for true or\c3 0 \c6for false.");
		}
	}
	else
	{
		messageClient(%client,'',"\c6You must be a \c3Super Admin\c6 to use this command.");
	}
}
