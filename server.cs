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
if(isFile("./back.cs"))
	exec("./back.cs");
if(isFile("./prefs.cs"))
	exec("./prefs.cs");
if(isFile("./commands.cs"))
	exec("./commands.cs");

//now for the extra functions

function canStatusNow(%client)
{
	%client.canStatus = 1;
}

function announceafkloop()
{
	if($StatusModv2::AnnounceAFK == 1)
	{
		for(%i = 0; %i < ClientGroup.getCount(); %i++)
		{
			%cl = ClientGroup.getObject(%i);
			if(%cl.status $= "afk")
			{
				messageall('',"\c3" @ %cl.name @ "\c6 Is AFK: \c4" @ %cl.reason);
				%cl.player.setShapeName(%cl.name SPC "(AFK)", 8564862);
			}
		}        
	}
	schedule($StatusModv2::LoopTime * 1000, 0, announceafkloop);
}
