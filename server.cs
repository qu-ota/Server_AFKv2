//all modules will be loaded here. if adding new modules, make sure you put in 2 lines here.
//example:
//if(isFile("./<MODULE>.cs"))
//	exec("./<MODULE>.cs");
//capitalization not required, do not include <> when adding line

$afkNameChanges = 1;

if(isFile("./afk.cs"))
	exec("./afk.cs");
if(isFile("./brb.cs"))
	exec("./brb.cs");
//if(isFile("./commands.cs"))
//	exec("./commands.cs");

//now for the extra functions

function canStatusNow(%client)
	{
		%client.canStatus = 1;
	}

	function announceafkloop()
	{
		if($StatusMod::AnnounceAFK == 1)
		{
			for(%i = 0; %i < ClientGroup.getCount(); %i++)
			{
				%cl = ClientGroup.getObject(%i);
				if(%cl.status $= "afk")
				{
					messageall('',"\c3" @ %cl.name @ "\c6 Is AFK: \c4" @ %cl.reason);
				}
			}        
		}
		schedule($StatusMod::LoopTime * 1000, 0, announceafkloop);
	}

if(isFile("Add-Ons/System_ReturnToBlockland/server.cs") || isFile("Add-Ons/System_BlocklandGlass/server.cs"))
{
	if(!$RTB::RTBR_ServerControl_Hook)
	{
		RTB_registerPref("Announce AFK","Status Mod v2","StatusModv2::AnnounceAFK","bool","Server_StatusModv2",1,0,0);
		RTB_registerPref("Announce Time (Seconds)","Status Mod v2","StatusModv2::LoopTime","int 60 1200","StatusModv2",300,0,0);
	}
}
else
{
		//Change these prefs if you don't have Return to Blockland.
	$StatusMod::AnnounceAFK = 1;
	$StatusMod::LoopTime = 300;
}

functon serverCmdToggleAfkNameChanges(%client)
{
	if(%client.isSuperAdmin)
	{
		if($afkNameChanges == 1)
		{
			$afkNameChanges = 0;
			//to be continued soon
