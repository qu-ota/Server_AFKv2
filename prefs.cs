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
		echo("[AFKv2] Preferences registered successfully.");
	}
}
else
{
	$StatusModv2::AnnounceAFK = 1;
	$StatusModv2::LoopTime = 300;
	$StatusModv2::ChangeShapename = 1;
	echo("[AFKv2] Blockland Glass / Return to Blockland not found, values for commands set");
}

function serverCmdAnnounceAFK(%client, %value)
{
	if(%client.isSuperAdmin)
	{
		switch ($AnnounceAFK)
		{
			case 1:
				$AnnounceAFK = 0;
				if(isObject(BrickRotateSound))
					serverPlay2D(BrickRotateSound);
				announce("\c3" @ %client.name @ " \c6has \c4disabled \c3AFK Announcements\c6.");
			case 0:
				$AnnounceAFK = 1;
				if(isObject(BrickRotateSound))
					serverPlay2D(BrickRotateSound);
				announce("\c3" @ %client.name @ " \c6 has \c4enabled \c3AFK Announcements\c6.");
			default:
				messageClient(%client,'',"\c6That isn't a valid value! Use \c31 \c6for true or \c3\0 \c6for false.");
		}
	}
}
//more commands coming soon
