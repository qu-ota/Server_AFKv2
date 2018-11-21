if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
	if(!$RTB::RTBR_ServerControl_Hook)
		exec("Add-Ons/System_ReturnToBlockland/RTBR_ServerControl_Hook.cs");
	RTB_registerPref("Toggle","AFK Killer","AFKKiller::Toggle","bool","Server_AFKKiller",1,0,0);
	RTB_registerPref("Minigame Only","AFK Killer","AFKKiller::MGO","bool","Server_AFKKiller",1,0,0);
	RTB_registerPref("Time (Minutes)","AFK Killer","AFKKiller::Time","int 1 1440","Server_AFKKiller",10,0,0);
}
else
{
	$AFKKiller::Toggle = 1;
	$AFKKiller::MGO = 1;
	$AFKKiller::Time = 10;
}

package AFK_Killer
{
	function serverCmdMessageSent(%client,%message)
	{
		if(%client.isAFK)
		{
			%client.isAFK = 0;
			%client.hasBeenAfk = 0;
		}
		Parent::serverCmdMessageSent(%client,%message);
	}

	function serverCmdTeamMessageSent(%client,%message)
	{
		if(%client.isAFK)
		{
			%client.isAFK = 0;
			%client.hasBeenAfk = 0;
		}
		Parent::serverCmdTeamMessageSent(%client,%message);
	}

	function serverCmdStartTalking(%client)
	{
		if(%client.isAFK)
		{
			%client.isAFK = 0;
			%client.hasBeenAfk = 0;
		}
		Parent::serverCmdStartTalking(%client);
	}

	function serverCmdstoptalking(%client)
	{
		if(%client.isAFK)
		{
			%client.isAFK = 0;
			%client.hasBeenAfk = 0;
		}
		Parent::serverCmdStopTalking(%client);
	}

	function Armor::onTrigger(%this,%obj,%slot,%val)
	{
		Parent::onTrigger(%this,%obj,%slot,%val);
		%client = %obj.client;
		if(%val)
		{
			if(%client.isAFK)
			{
				%client.isAFK = 0;
				%client.hasBeenAfk = 0;
			}
		}
	}
	
};
activatepackage(AFK_Killer);

function AFKKillerTick()
{
	cancel($AFKKillerTick);
	for(%i=0;%i<clientGroup.getCount();%i++)
	{
		%client = clientGroup.getObject(%i);
		if($AFKKiller::Toggle)
		{
			if(isObject(%client.player))
			{
				%face = %client.player.getEyeVector();
				%position = %client.player.gettransform();
				if(%client.lastface $= %face && %client.lastposition $= %position)
				{
					%client.isAFK = 1;
					%client.hasBeenAfk++;
				}
				else
				{
					%client.hasBeenAfk = 0;
					%client.isAfk = 0;
				}
				%client.lastface = %face;
				%client.lastposition = %position;
				if(%client.hasBeenAfk)
				{
					if(%client.hasBeenAfk == $AFKKiller::Time - 1)
						MessageClient(%client,'',"\c0WARNING: \c6You appear to be AFK. If you do not return within the next minute you will be killed.");
					if(%client.hasBeenAfk >= $AFKKiller::Time)
					{
						if($AFKKiller::MGO)
						{
							if(isObject(%client.minigame))
							{
								MessageAll('',"\c3"@%client.name@" \c6was killed for being AFK.");
								%client.player.kill();
								%client.hasBeenAfk = 0;
							}
							else
								%client.hasBeenAfk = 0;
						}
						else
						{
							MessageAll('',"\c3"@%client.name@" \c6was killed for being AFK.");
							%client.player.kill();
							%client.hasBeenAfk = 0;
						}
					}
				}
			}
			else
				%client.hasBeenAfk = 0;
		}
		else
		{
			if(%client.isAfk)
				%client.isAfk = 0;
			if(%client.hasBeenAfk)
				%client.hasBeenAfk = 0;
		}
	}
	$AFKKillerTick = schedule(60000,0,AFKKillerTick);
}
AFKKillerTick();