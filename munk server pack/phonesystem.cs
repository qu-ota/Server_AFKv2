//./phonesystem.cs
package phonesystem
{
	function serverCmdMessageSent(%client, %message)
	{
		if($MSP::PhoneSystem)
		{
			if(!%client.isOnThePhone)
			{
				parent::serverCmdMessageSent(%client, %message);
			}
			else
			{
				if(%client.isOnThePhone) //Just to double check
				{
					%callee = %client.callee;
					%beginning = "\c4[On the Phone\c4] \c1" @ %client.name @ "\c6: ";
					%rmessage = stripmlcontrolchars(%message);
					messageClient(%callee,'',"" @ %beginning @ "\c6" @ %rmessage);
					messageClient(%client,'',"" @ %beginning @ "\c6" @ %rmessage);
					echo("[" @ %client.name @ " to " @ %callee.name @ "]: " @ %rmessage);
				}
				else
				{
				parent::serverCmdMessageSent(%client, %message);
				}
			}
		}
		else
		{
			parent::serverCmdMessageSent(%client, %message);
		}
	}
	
	function serverCmdTeamMessageSent(%client, %message)
	{
		if(%client.isOnThePhone)
		{
			parent::serverCmdMessageSent(%client, %message);
		}
		else
		{
			parent::serverCmdTeamMessageSent(%client, %message);
		}
	}

	function serverCmdCall(%client, %callee)
	{
		if($MSP::PhoneSystem)
		{
			if(isObject(fcbn(%callee)))
			{
				%client.callee = fcbn(%callee);
				%callee2 = fcbn(%callee);
				if(%callee2.pendingCall || %callee2.isOnThePhone)
				{
					%words = "Sorry, but \c3" @ %callee2.name @ "\c6 is already on the phone.";
					MSPTC(%client, %words);
				}
				else
				{
					%client.pendingCall = 1;
					%callee2.pendingCall = 1;
					%callee2.callee = %client;
					startRinging(%client);
				}
			}
			else
			{
				%words = "Sorry, I cannot find \c3" @ %callee @ "\c6, Try using their name.";
				MSPTC(%client, %words);
			}
		}
		else
		{
			%words = "Sorry, The Phone system is disabled. Contact a Super Admin for help.";
			MSPTC(%client, %words);
		}
	}
	
	
	function startRinging(%client)
	{
		if(%client.Rings >= 7)
		{
			%word = "Stopped ringing due to 7 rings.";
			MSPTC(%client, %word);
			%client.Rings = 0;
		}
		else
		{
			%callee = %client.callee;
			%words = "\c6Ring, Ring! \c7(Calling \c3" @ %callee.name @ "\c7)";
			%client.PCS = schedule(8000, 0, startRinging, %client);
			MSPTC(%client, %words);
			%calleeWords = "\c2Ring, Ring! \c7(Call from \c3" @ %client.name @ "\c7. Say /answer to answer, or /ignore to ignore.)";
			MSPTC(%callee, %calleewords);
			%client.Rings++;
		}
	}
	
	function serverCmdAnswer(%client)
	{
		if(%client.pendingCall)
		{
			%callee = %client.callee;
			%client.pendingCall = 0;
			%callee.pendingCall = 0;
			%client.isOnThePhone = 1;
			%callee.isOnThePhone = 1;
			%callee.Rings = 0;
			cancel(%callee.PCS);
			%words = "\c3" @ %client.name @ "\c6 has answered. You are on the phone!";
			MSPTC(%callee, %words);
			%words2 = "\c6You answered the phone. You are on the phone!";
			MSPTC(%client, %words2);
		}
		else
		{
			%word = "Someone is not calling you!";
			MSPTC(%client, %word);
		}
	}
	
	function serverCmdIgnore(%client)
	{
		if(%client.pendingCall)
		{
			%callee = %client.callee;
			%client.pendingCall = 0;
			%callee.pendingCall = 0;
			%callee.Rings = 0;
			cancel(%callee.PCS);
			%words2 = "\c6You ignore the phone.";
			MSPTC(%client, %words2);
		}
		else
		{
			%word = "Someone is not calling you!";
			MSPTC(%client, %word);
		}
	}
	
	function serverCmdHangUp(%client)
	{
		%callee = %client.callee;
		if(%client.isOnThePhone)
		{
			%callee.isOnThePhone = 0;
			%client.isOnThePhone = 0;
			%client.callee = "";
			%callee.callee = "";
			%word = "\c3" @ %client.name @ "\c6 hung up on you. You are no longer on the phone.";
			MSPTC(%callee, %word);
			%words = "You hang up the phone. You are no longer on the phone.";
			MSPTC(%client, %words);
		}
		else
		{
			%wordss = "You cannot hang up if you are not on the phone!";
			MSPTC(%client, %wordss);
		}
	}
};
activatepackage(phonesystem);

if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
	if(!$RTB::RTBR_ServerControl_Hook)

exec("Add-Ons/System_ReturnToBlockland/RTBR_ServerControl_Hook.cs");
	RTB_registerPref("Phone Use Toggle","Munk's Server Pack","MSP::PhoneSystem","bool", "MunkServerPack", 1, 0, 1);
}
else
{
	$MSP::PhoneSystem = 1;
}