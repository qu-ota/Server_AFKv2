//Commands for admins to see who is AFK and who is not

function serverCmdStatus(%client, %target)
{
	%targetcl = findClientByName(%target);        
	
	if(!IsObject(findClientByName(%target)))
	{
		messageclient(%client,'',"<color:F660AB>That user (\c3" @ %target @ "<color:F660AB>) does not exist.");
	}
	else
	{
		if(%targetcl.status $= "afk")
		{
			messageclient(%client,'',"\c3" @ %targetcl.name @ "\c6 is currently AFK for the following reason: \c5" @ %targetcl.reason);
		}
		else
		{
			messageclient(%client,'',"\c3" @ %targetcl.name @ "\c6 is not afk.");
		}
	}
}

function servercmdAfkers(%client)
{
	for(%i = 0; %i < ClientGroup.getCount(); %i++)
	{
		%cl = ClientGroup.getObject(%i);
		if(%cl.status $= "afk")
		{
			messageclient(%client,'',"\c3Here is a list of users that are afk:");
			messageclient(%client,'',"\c3" @ %cl.name @ "\c6 for reason: \c4" @ %cl.reason);
		}
	}
}
