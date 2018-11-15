//./admincommands.cs

if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
	if(!$RTB::RTBR_ServerControl_Hook)

exec("Add-Ons/System_ReturnToBlockland/RTBR_ServerControl_Hook.cs");
	RTB_registerPref("Fling Command","Munk's Server Pack","MSP::Fling","list SuperAdmin 1 Admins 2 Host 3", "System_MunkServerPack",1, 0, 1);
	RTB_registerPref("Launch Command","Munk's Server Pack","MSP::Launch","list SuperAdmin 1 Admins 2 Host 3", "System_MunkServerPack",1, 0, 0);
	RTB_registerPref("Mute Command","Munk's Server Pack","MSP::Mute","list SuperAdmin 1 Admins 2 Host 3", "System_MunkServerPack",1, 0, 1);
	RTB_registerPref("Tumble Command","Munk's Server Pack","MSP::Tumble","list SuperAdmin 1 Admins 2 Host 3", "System_MunkServerPack",1, 0, 1);
	RTB_registerPref("Flash Command","Munk's Server Pack","MSP::Flash","list SuperAdmin 1 Admins 2 Host 3", "System_MunkServerPack",1, 0, 0);
	RTB_registerPref("Roast Command","Munk's Server Pack","MSP::Roast","list SuperAdmin 1 Admins 2 Host 3", "System_MunkServerPack",1, 0, 0);
	RTB_registerPref("LaunchNTumble Command","Munk's Server Pack","MSP::LNT","list SuperAdmin 1 Host 3", "System_MunkServerPack",1, 0, 1);
	RTB_registerPref("Sparta Command","Munk's Server Pack","MSP::Sparta","list SuperAdmin 1 Admins 2 Host 3", "System_MunkServerPack",1, 0, 1);
	RTB_registerPref("Flying","Munk's Server Pack","MSP::Flying","list SuperAdmin 1 Admins 2 Host 3 Everyone 4", "System_MunkServerPack",1, 0, 1);
}
else
{
	$MSP::Fling = 1;
	$MSP::Launch = 1;
	$MSP::Mute = 0;
	$MSP::Tumble = 1;
	$MSP::Flash = 1;
	$MSP::Roast = 1;
	$MSP::LNT = 1;
	$MSP::Sparta = 1;
	$MSP::Flying = 1;
}

//+---------+
//+-Fling
//+---------+
package fling
{
	function servercmdfling(%client, %user, %X, %Y, %Z)
	{
		if($MSP::Fling == 1 && !%client.isSuperAdmin)
		{
			messageClient(%client, '', '\c6You must be a super admin to use this command.');
			return;
		}
		
		if($MSP::Fling == 2 && !%client.isAdmin)
		{
			messageClient(%client, '', '\c6You must be an Admin to use this command.');
			return;
		}
		
		if($MSP::Fling == 3 && !((isObject(localclientconnection) && %client.getID() == 	localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
		{
			messageClient(%client, '', '\c6You must be the Host to use this command.');
			return;
		}
		
		if(%user !$= "")
		{
				%person = findClientByName(%user).player;
				%personN = findClientByName(%user);
		}
		
		if(isObject(%person))
		{
			%person.addvelocity(%X@" "@%Y@" "@%Z);
			messageAll("",'\c3%1 \c6was flung by \c3%2\c6.', %personN.name, %client.name);
			messageClient(%personN, '', '\c3%1 \c6has flung you.', %client.name);
		}
		else
		{
			messageClient(%client, '', '\c6That player hasnt spawned yet!', %user);
		}
	}
	
	function servercmdflingall(%client, %x, %y, %z)
	{
		if(%client.isSuperAdmin)
		{
			messageall('',"\c3" @ %client.name @ "\c6 has flung everyone!");
			for (%a=0; %a < ClientGroup.getcount(); %a++)
       		{
        	    %cl=ClientGroup.getObject(%a);
        	    if(%cl == %client)
				{
					//Nothing!
				}
				else
        	    {
        	       	%cl.player.addvelocity(%X@" "@%Y@" "@%Z);
        	    }
        	}
		}
		else
		{
			messageclient(%client,'',"\c6You are not Super Admin! HAHAHA!");
		}
	}
};
activatePackage(fling);


//+---------+
//+-Launch
//+---------+
package launch
{
	function servercmdlaunch(%client, %user)
	{
		if($MSP::Launch == 1 && !%client.isSuperAdmin)
		{
			messageClient(%client, '', '\c6You must be a super admin to use this command.');
			return;
		}
		
		if($MSP::Launch == 2 && !%client.isAdmin)
		{
			messageClient(%client, '', '\c6You must be an Admin to use this command.');
			return;
		}
		
		if($MSP::Launch == 3 && !((isObject(localclientconnection) && %client.getID() == 	localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
		{
			messageClient(%client, '', '\c6You must be the Host to use this command.');
			return;
		}
		
		if(%user !$= "")
		{
			%person = findClientByName(%user).player;
			%personN = findClientByName(%user);
		}
		
		if(isObject(%person))
		{
			%person.addvelocity("0 0 100");
			messageAll("",'\c3%1 \c6was launched by \c3%2\c6.', %personN.name, %client.name);
			messageClient(%personN, '', '\c3%1 \c6has launched you.', %client.name);
		}
		else
		{
			messageClient(%client, '', '\c6That player hasnt spawned yet!', %user);
		}
	}
	
	function servercmdlaunchall(%client)
	{
		if(%client.isSuperAdmin)
		{
			messageall('',"\c3" @ %client.name @ "\c6 has launched everyone!");
			for (%a=0; %a < ClientGroup.getcount(); %a++)
       		{
        	    %cl=ClientGroup.getObject(%a);
        	    if(%cl == %client)
					{
					//Nothing!
					}
				else
        	    	{
        	       	%cl.player.addvelocity("0 0 100");
        	    	}
        	}
		}
		else
		{
			messageclient(%client,'',"\c6You are not Super Admin! HAHAHA!");
		}
	}
};
ActivatePackage(launch);


//+---------+
//+-Mute
//+---------+
package mute
{
	
	function servercmdMute(%client, %user, %time)
	{

		if($MSP::Mute == 1 && !%client.isSuperAdmin)
		{
			messageClient(%client, '', '\c6You must be a super admin to use this command.');
			return;
		}
	
		if($MSP::Mute == 2 && !%client.isAdmin)
		{
			messageClient(%client, '', '\c6You must be an Admin to use this command.');
			return;
		}
	
		if($MSP::Mute == 3 && !((isObject(localclientconnection) && %client.getID() == 	localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
		{
			messageClient(%client, '', '\c6You must be the Host to use this command.');
			return;
		}	

		if(%time $= "" || %time < 20 || !isInteger(%time))
		{
			%time = 30;
		}
		
		%victim = FindClientByName(%user);
		$Mute[%victim.BL_ID] = true;
		messageAll("", "\c3"@ %victim.name @"\c6 Has been muted by \c3"@ %client.name @"\c6 for "@ %time @" Seconds");
		messageClient(%victim, '', "\c6You have been muted by \c3"@ %Client.name @"\c6 for "@ %time @" Seconds");
		Cancel($mute::Schedule[%victim.BL_ID]);
		$mute::Schedule[%victim.BL_ID] = schedule(%time*1000,0,"unmute", %victim);
	}	

	function unmute(%victim)
	{
		$Mute[%victim.BL_ID] = false;
		MessageClient(%victim, '', "\c6You are now unmuted");
	}	

	function servercmdunmute(%client, %user)
	{
		%victim = FindClientByName(%user);
		
		if($MSP::Mute == 1 && !%client.isSuperAdmin)
		{
			messageClient(%client, '', '\c6You must be a super admin to use this command.');
			return;
		}
		
		if($MSP::Mute == 2 && !%client.isAdmin)
		{
			messageClient(%client, '', '\c6You must be an Admin to use this command.');
			return;
		}
		
		if($MSP::Mute == 3 && !((isObject(localclientconnection) && %client.getID() == 	localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
		{
			messageClient(%client, '', '\c6You must be the Host to use this command.');
			return;
		}
		
		if($mute[%victim.BL_ID] == false)
		{
			messageClient(%client, '', "\c6That player is not muted!");
			return;
		}
		else	
		{
			$Mute[%victim.BL_ID] = false;
			messageClient(%victim, '', "\c6You have been unmuted by \c3"@ %client.name);
			messageAll("", "\c3"@ %victim.name @" \c6has been unmuted by \c3"@ %client.name);
			cancel($mute::Schedule[%victim.BL_ID]);
		}
	}

	function servercmdMessageSent(%client, %text)
	{
		if($mute[%Client.BL_ID] == true)
		{
			messageClient(%client, '', "\c6You cannot talk while muted.");
		}
		
		else
		{
	 		Parent::ServercmdmessageSent(%client, %text);
		}
	}
	
	function servercmdTeamMessageSent(%client, %text)
	{
		if($mute[%Client.BL_ID] == true)
		{		
			messageClient(%client, '', "\c6You cannot talk while muted.");
		}
		
		else
		{
	 		Parent::ServercmdTeammessageSent(%client, %text);
		}
	}
	
};
ActivatePackage(mute);


//+---------+
//+-Talkas
//+---------+

package talkas
{
	function serverCmdtalkas(%client,%target,%Chat,%Chat2,%Chat3,%Chat4,%Chat5,%Chat6,%Chat7,%Chat8,%Chat9,%Chat10,%Chat11,%Chat12,%Chat13,%Chat14,%Chat15,%Chat16)
	{
		%targetclient = findclientbyname(%target);
		
		if(!%client.isSuperAdmin || !%client.bl_id == 11977)
		{
			messageclient(%client,'',"This command can only be used by Super Admins!");
			return;
		}
		
		if(!isObject(findclientbyname(%target)))
		{
			messageclient(%client,'',"\c6Invalid player, lol.");
			return;
		}
		
		if(findClientByName(%target).bl_id == 11977)
		{
			%words = "You cannot talk as Munk!";
			MSPTC(%client, %words);
			return;
		}
		else
		{
			servercmdMessageSent(findclientbyname(%targetclient.name),"" @ %chat SPC %chat2 SPC %chat3 SPC %chat4 SPC %chat5 SPC %chat6 SPC %chat7 SPC %chat8 SPC %chat9 SPC %chat10 SPC %chat11 SPC %chat12 SPC %chat13 SPC %chat14 SPC %chat15 SPC %chat16); 
		}
	}
};
activatePackage(talkas);


//+---------+
//+-Tumble
//+---------+
package tumble
{
	function servercmdtumble(%client, %user)
	{
		if($MSP::Tumble == 1 && !%client.isSuperAdmin)
		{
			messageClient(%client, '', '\c6You must be a super admin to use this command.');
			return;
		}
		
		if($MSP::Tumble == 2 && !%client.isAdmin)
		{
			messageClient(%client, '', '\c6You must be an Admin to use this command.');
			return;
			
		}
		
		if($MSP::Tumble == 3 && !((isObject(localclientconnection) && %client.getID() == 	localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
		{
			messageClient(%client, '', '\c6You must be the Host to use this command.');
			return;
		}
		
		if(%user !$= "")
		{
			%person = findClientByName(%user).player;
			%personN = findClientByName(%user);
		}
		
		if(isObject(%person))
		{
			schedule(1000, 0, tumble, %person, 0.1);
			messageAll("",'\c3%1 \c6was tumbled by \c3%2\c6.', %personN.name, %client.name);
			messageClient(%personN, '', '\c3%1 \c6has tumbled you.', %client.name);
		}
		else
		{
			messageClient(%client, '', '\c6That player hasnt spawned yet!', %user);
		}
	}
	
	function servercmdtumbleall(%client)
	{
		if(%client.isSuperAdmin)
		{
			messageall('',"\c3" @ %client.name @ "\c6 has tumbled everyone!");
			for (%a=0; %a < ClientGroup.getcount(); %a++)
       		{
        	    %cl=ClientGroup.getObject(%a);
				%clplayer = %cl.player;
        	    if(%cl == %client)
					{
					//Nothing!
					}
				else
        	    	{
					schedule(1000, 0, tumble, %clplayer, 1);
        	    	}
        	}
		}
		else
		{
			messageclient(%client,'',"\c6You are not Super Admin! HAHAHA!");
		}
	}
};
ActivatePackage(tumble);


//+---------+
//+-Flash
//+---------+
package Flash
{
	function servercmdflash(%client, %user, %level)
	{
		if($MSP::Flash == 1 && !%client.isSuperAdmin)
		{
			messageClient(%client, '', '\c6You must be a super admin to use this command.');
			return;
		}
		
		if($MSP::Flash == 2 && !%client.isAdmin)
		{
			messageClient(%client, '', '\c6You must be an Admin to use this command.');
			return;
		}
		
		if($MSP::Flash == 3 && !((isObject(localclientconnection) && %client.getID() == 	localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
		{
			messageClient(%client, '', '\c6You must be the Host to use this command.');
			return;
		}
		
		if(%user !$= "")
		{
			%person = findClientByName(%user).player;
			%personN = findClientByName(%user);
		}
		
		if(isObject(%person))
		{
			if(!%level)
			{
				%level = 1.0;
			}
		
			%person.setwhiteout(%level);
			messageAll("",'\c3%1 \c6was flashed by \c3%2\c6.', %personN.name, %client.name);
			messageClient(%personN, '', '\c3%1 \c6has flashed you.', %client.name);

		}
		else
		{
			messageClient(%client, '', '\c6That player hasnt spawned yet!', %user);
		}
	}
	
	function servercmdflashall(%client, %level)
	{
		if(%client.isSuperAdmin)
		{
			messageall('',"\c3" @ %client.name @ "\c6 has flashed everyone!");
			for (%a=0; %a < ClientGroup.getcount(); %a++)
       		{
        	    %cl=ClientGroup.getObject(%a);
				%clplayer = %cl.player;
        	    if(%cl == %client)
				{
					//Nothing!
				}
				else
        	    {
					if(!%level)
					{
						%level = 1.0;
					}
		
					%clplayer.setwhiteout(%level);
				}
			}
		}
		else
		{
			messageclient(%client,'',"\c6You are not Super Admin! HAHAHA!");
		}
	}
};
ActivatePackage(Flash);


//+---------+
//+-Fake Admins
//+---------+
function servercmdfakeadmin(%client,%targetclient)
{
	
	if(!%client.issuperAdmin)
	{
		messageclient(%client,'',"This command can only be used by Super Admins!");
		return;
	}

	
	if(%client.issuperadmin)
	{
		messageall('MsgAutoAdmin',"\c2" @ %targetclient @ " has become Admin(Auto)");
	}
}

function servercmdfakesuperadmin(%client,%targetclient)
{
	
	if(!%client.isSuperAdmin)
	{
		messageclient(%client,'',"This command can only be used by Super Admins!");
		return;
	}
	
	if(%client.issuperadmin)
	{
		messageall('MsgAutoAdmin',"\c2" @ %targetclient @ " has become Super Admin(Auto)");
	}
}


//+---------+
//+-Kick Reason
//+---------+
package kickreason
{
	function gameConnection::kickReason(%client,%target,%reason) 
	{ 
		%target = findclientbyname(%target); 
		if(%target.isSuperAdmin)
			return; 
		echo(%reason); 
		if(%client.isAdmin) 
		{ 
			messageAll('msgAdminForce',"\c3" @ %client.name @ " \c2kicked \c3" @ %target.name @ "\c2"SPC "(" @ "ID: " @ %target.bl_id @ ")" SPC " - \"" @ %reason @ "\""); 
			%target.delete(%reason); 
		} 
	} 
	function serverCmdKick(%client,%target,%r1,%r2,%r3,%r4,%r5,%r6,%r8,%r9,%r10,%r11,%r12,%r13,%r14,%r15) 
	{ 
		if(%r1 !$= "")
		{
			for(%i = 0; %i < 15; %i++)
			{
				if(%r[%i] !$= "")
				{
					%reason = %reason SPC %r[%i];
				}
			}	 
			%client.kickReason(%target,%reason); 
		}
		else
		{
			parent::ServerCmdKick(%client,%target);
		}
	}
};
activatePackage(kickreason);

//+---------+
//+-Roast
//+---------+
package roast
{
	function servercmdroast(%client, %user, %level)
	{	
		if($MSP::Roast == 1 && !%client.isSuperAdmin)
		{
			messageClient(%client, '', '\c6You must be a super admin to use this command.');
			return;
		}
		
		if($MSP::Roast == 2 && !%client.isAdmin)
		{
			messageClient(%client, '', '\c6You must be an Admin to use this command.');
			return;
		}
		
		if($MSP::Roast == 3 && !((isObject(localclientconnection) && %client.getID() == 	localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
		{
			messageClient(%client, '', '\c6You must be the Host to use this command.');
			return;
		}
		
		if(%user !$= "")
		{
			%person = findClientByName(%user).player;
			%personN = findClientByName(%user);
		}
		
		if(isObject(%person))
		{
			if(!%level)
			{
				%level = 10;
			}
			
		
			%person.burnPlayer(%level);
			messageAll("",'\c3%1 \c6was roasted by \c3%2\c6 for \c3%3 \c6seconds.', %personN.name, %client.name, %level);
			messageClient(%personN, '', '\c3%1 \c6has roasted you for \c3%2 \c6seconds.', %client.name, %level);
		}
		else
		{
			messageClient(%client, '', '\c6That player hasnt spawned yet!', %user);
		}
	}
	
	function serverCmdExtinguish(%client, %target)
	{
		if(%client.isAdmin)
		{
			if(isObject(fcbn(%target)))
			{
				%t = fcbn(%target);
				%t.player.clearBurn();
				%words = "\c3" @ %t.name @ "\c6 was extinguished.";
				%words2 = "\c3" @ %client.name @ "\c6 has extinguished you.";
				MSPTC(%client, %words);
				MSPTC(%t, %words2);
			}
			else
			{
				%words = "Sorry, But I cannot find the player.";
				MSPTC(%client, %words);
			}
		}
	}
	
	function serverCmdRoastAll(%client, %time)
	{
		if(%client.isSuperAdmin)
		{
			if(%time == 0 || %time $= "")
			{
				%time = 10;
			}
			
			for(%i = 0; %i < clientGroup.getCount(); %i++)
			{
				%cl = clientGroup.getObject(%i);
				%pl = %cl.pl;
				%pl.burnPlayer(%time);
				messageAll('',"\c3" @ %client.name @ "\c6 has roasted everyone for \c3" @ %time @ "\c6 seconds!");
			}
		}
	}
	
	function serverCmdExtinguishAll(%client)
	{
		if(%client.isSuperAdmin)
		{
			for(%i = 0; %i < clientGroup.getCount(); %i++)
			{
				%cl = clientGroup.getObject(%i);
				%pl = %cl.pl;
				%pl.clearBurnPlayer();
				messageAll('',"\c4---\c3" @ %client.name @ "\c6 has extinguished everyone!");
			}
		}
	}
	
};
ActivatePackage(Roast);


//+---------+
//+-Launch n tumble
//+---------+
package LNT
{
	function servercmdLaunchNTumble(%client, %user)
	{
		if($MSP::LNT == 1 && !%client.isSuperAdmin)
		{
			messageClient(%client, '', '\c6You must be a super admin to use this command.');
			return;
		}
		
		if($MSP::LNT == 2 && !%client.isAdmin)
		{
			messageClient(%client, '', '\c6You must be an Admin to use this command.');
			return;
		}
		
		if($MSP::LNT == 3 && !((isObject(localclientconnection) && %client.getID() == 	localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
		{
			messageClient(%client, '', '\c6You must be the Host to use this command.');
			return;
		}
		
		if(%user !$= "")
		{
			%person = findClientByName(%user).player;
			%personN = findClientByName(%user);
		}
		
		if(isObject(%person))
		{
			%person.addvelocity("0 0 100");
			schedule(3000, 0, tumble, %person, 0.1);
			messageAll("",'\c3%1 \c6was Launched then Tumbled by \c3%2\c6.', %personN.name, %client.name);
			messageClient(%personN, '', '\c3%1 \c6has Launched then Tumbled you.', %client.name);
		}
		else
		{
			messageClient(%client, '', '\c6That player hasnt spawned yet!', %user);
		}
	}
	
	function serverCmdLNTAll(%client, %time)
	{
		if(%client.isSuperAdmin)
		{
			for(%i = 0; %i < clientGroup.getCount(); %i++)
			{
				%cl = clientGroup.getObject(%i);
				%pl = %cl.pl;
				%pl.addvelocity("0 0 100");
				schedule(3000, 0, tumble, %pl, 0.1);
				messageAll('',"\c3" @ %client.name @ "\c6 has launched and tumbled everyone!");
			}
		}
	}
	
	function serverCmdLNT(%client, %player)
	{
		serverCmdLaunchNTumble(%client, %player);
	}
	
};
ActivatePackage(LNT);


//+---------+
//+-Sparta
//+---------+
package Sparta
{
	function servercmdSparta(%client, %user)
	{
		
		if($MSP::Sparta == 1 && !%client.isSuperAdmin)
		{
			messageClient(%client, '', '\c6You must be a super admin to use this command.');
			return;
		}
		
		if($MSP::Sparta == 2 && !%client.isAdmin)
		{
			messageClient(%client, '', '\c6You must be an Admin to use this command.');
			return;
		}
		
		if($MSP::Sparta == 3 && !((isObject(localclientconnection) && %client.getID() == 	localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
		{
			messageClient(%client, '', '\c6You must be the Host to use this command.');
			return;
		}
		
		if(%user !$= "")
		{
			%person = findClientByName(%user).player;
			%personN = findClientByName(%user);
		}
		
		if(isObject(%person))
		{
			%person.addvelocity("50 50 50"); //FLY
			schedule(800, 0, spartacmd, %person, 2);//4 BURN
			schedule(1000, 0, spartacmd, %person, 1);//0.1 TUMBLE
			schedule(5000, 0, spartacmd, %Person, 3);// DELETE
			messageAll("",'\c3%1 \c6was Spartan-Kicked by \c3%2\c6.', %personN.name, %client.name);
			messageClient(%personN, '', '\c3%1 \c6has Spartan-Kicked you.', %client.name);
		}
		else
		{
			messageClient(%client, '', '\c6That player hasnt spawned yet!', %user);
		}
	}
	
	function spartacmd(%player, %stage)
	{
		%client = %player.client;
		switch(%stage)
		{
			case 1:
				%player.burnPlayer(6);
			
			case 2:
				%player.tumble(0.1);
				
			case 3:
				%client.delete("You were Spartan-Kicked.");
		}
	}
	
};
ActivatePackage(Sparta);


//+---------+
//+-Flying
//+---------+
package flying
{
	function serverCmdFly(%client)
	{
		%tog = %client.FlyToggle;
		if(%tog)
		{
			%client.FlyToggle = 0;
			%words = "Flymode has been turned off.";
			MSPTC(%client, %words);
			%client.player.ChangeDataBlock(%client.oldFDB);
		}
		else
		{
			
			if($MSP::Flying == 1 && !%client.isSuperAdmin)
			{

				messageClient(%client, '', '\c6You must be a super admin to use this command.');

				return;
			}

			if($MSP::Flying == 2 && !%client.isAdmin)
			{

				messageClient(%client, '', '\c6You must be an Admin to use this command.');

				return;
			}

			if($MSP::Flying == 3 && !((isObject(localclientconnection) && %client.getID() == localclientconnection.getID())) || !%client.bl_id == getNumKeyID())
			{

				messageClient(%client, '', '\c6You must be the Host to use this command.');

				return;
			}
			else
			{
				%telling = "You have turned on flymode. Just press your jet button and look around, you will go that way!";
				MSPTC(%client, %telling);
				%client.FlyToggle = 1;
				%client.oldFDB = %client.player.getDatablock();
				%client.Player.changeDatablock(PlayerNoJet);
				%client.FlyLoop = schedule(1000, 0, flyloop, %client);
			}
		}
	}

	function flyloop(%client)
	{	
		if(%client.FlyToggle)
		{
			//talk("checking if player crouching");
			%jetting = %client.player.jet;
			if(%jetting)
			{
				//talk("player is crouching");
				if(!isObject(%client.player)) return;
    				%vec = %client.player.getEyeVector();
    				%client.player.addvelocity(vectorScale(%vec, 8));
	
			}
			else if(%client.player.jump)
			{
				%client.player.Addvelocity("0 0 8");
			}
			else if(%client.player.crouch)
			{
				%client.player.addvelocity("0 0 -8");
			}
			else
			{
				if(%client.player.isOnGround())
				{
					//Nothing
				}
				else
				{
					//Thanks to Mr. Wallet for helping me do this
					%client.player.addVelocity("0 0 1.28");
					%client.player.setVelocity("0 0 1.28");
				}
			}
			%client.FlyLoop = schedule(33, 0, flyLoop, %client);
		}
	}

	function Armor::onTrigger(%datablock,%player,%slot,%val)
	{
		Parent::onTrigger(%datablock,%player,%slot,%val);
		switch(%slot)
		{
			case 2:
				%player.jump = %val;
				
			case 3:
				%player.crouch = %val;
				
			case 4:
				%player.jet = %val;
		}
	}

	//From AGILE Player
	function Player::IsOnGround(%player)
	{
		%pos = %player.getPosition();
		%scale = %player.getScale();
		%xs = getWord(%scale,0);
		%ys = getWord(%scale,1);
		
		%nw = vectorAdd(%pos,0 - (0.75 * %xs) SPC 0 - (0.75 * %ys) SPC 0);
		%dnw = vectorAdd(%nw,"0 0 -0.5");
		%ne = vectorAdd(%pos,(0.75 * %xs) SPC 0 - (0.75 * %ys) SPC 0);
		%dne = vectorAdd(%ne,"0 0 -0.5");
		%se = vectorAdd(%pos,(0.75 * %xs) SPC (0.75 * %ys) SPC 0);
		%dse = vectorAdd(%se,"0 0 -0.5");
		%sw = vectorAdd(%pos,0 - (0.75 * %xs) SPC (0.75 * %ys) SPC 0);
		%dsw = vectorAdd(%sw,"0 0 -0.5");
		%dpos = vectorAdd(%pos,"0 0 -0.5");

		%raynw = ag_bicheck(getWord(containerRaycast(%nw,%dnw,$TypeMasks::FxBrickAlwaysObjectType|$TypeMasks::InteriorObjectType),0));
		%rayne = ag_bicheck(getWord(containerRaycast(%ne,%dne,$TypeMasks::FxBrickAlwaysObjectType|$TypeMasks::InteriorObjectType),0));
		%rayse = ag_bicheck(getWord(containerRaycast(%se,%dse,$TypeMasks::FxBrickAlwaysObjectType|$TypeMasks::InteriorObjectType),0));
		%raysw = ag_bicheck(getWord(containerRaycast(%sw,%dsw,$TypeMasks::FxBrickAlwaysObjectType|$TypeMasks::InteriorObjectType),0));
		%rayce = ag_bicheck(getWord(containerRaycast(%pos,%dpos,$TypeMasks::FxBrickAlwaysObjectType|$TypeMasks::InteriorObjectType),0));

		if(%raynw || %rayne || %rayse || %raysw || %rayce)
		{
			return 1;
		}
		else
		{
			return 0;
		}
	}

	function ag_bicheck(%obj)
	{
		if(!isObject(%obj))
		{
			return 0;
		}
		if(%obj.getClassName() $= "fxDTSbrick")
		{
			if(%obj.isColliding() && %obj.isRendering())
			{
				return 1;
			}
			return 0;
		}
		return 1;
	}

};
activatePackage(flying);
