//./main.cs

$MSP::Version = "Three";

package MSPClientEnterGame 
{
   function GameConnection::AutoAdminCheck(%this)
   {
		%this.canStatus = 1;
		messageClient(%this, '', "<color:1589FF>This server is running Munk's Server Pack. Say /msp help so you know how this works.");
		messageclient(%this, '', "<color:1589FF>Current server news:");
		%client.sentPoke = 0;
		tellNews(%this);
		return Parent::AutoAdminCheck(%this);
   }
};
activatepackage(MSPClientEnterGame);


//Help Function
function serverCmdMSP(%client, %that)
{
    switch$(%that)
    {
        case "help":
            %words = "Welcome to Munk's Server Pack! Say /msp [Section] to learn more about that section!";
            %words2 = "Sections: help, commands, admincommands, status, afkers, talkas, fling, launch, mute, unmute, flash, slap, kickreason, roast, launchntumble, fly, and phone!";
            
        case "commands":
            %words = "Commands: /poke, /pm, /afkers, /status, /afk, /brb, /foodbreak, /bathroombreak.";
            
        case "admincommands":
            %words = "Admin Commands: /fling, /launch, /mute, /unmute, /blind, /slap, /roast, /launchntumble, /talkas.";
            
        case "fling":
            %words = "/Fling [NameofVictim] X Y Z. The [NameofVictim] is the player being flung. The X Y Z is the velocity the player will go. It is the same as the addvelocity event.";
            
        case "talkas":
            %words = "/talkas [NameofVictim] [Words]. The [NameofVictim] is the player you talk as. The [Words] is the words you saw as them.";
            
        case "launch":
            %words = "/launch [NameofVictim]. The [NameofVictim] is the player being launched. It launches them up X-0 Y-0 Z-100 Velocity, Causing them to go up.";
            
        case "afkers":
            %words = "/afkers. This will list the people that are afk.";
            
        case "status":
            %words = "Status tells people's status. Commands: /status [Nameofperson], /afk, /brb, /back, /foodbreak, /bathroombreak.";
            
        case "slap":
            %words = "/slap [NameofVictim]. The [NameofVictim] is the player being slapped. Slapping a player causes them to be launched away from you.";
            
        case "flash":
            %words = "/flash [NameofVictim] [Power(0.0-1.0)]. The [NameofVictim] is the player being blinded. Blinding a player causes their screen to be flashed, obsecuring their vision.";
            
        case "tumble":
            %words = "/tumble [NameofVictim]. The [NameofVictim] is the played being tumbled. Tumbling a player causes them to freeze up and be affected by physics.";
            
        case "mute":
            %words = "/mute [NameofVictim] [Duration]. The [NameofVictim] is the played being muted, the [Duration] is how many seconds they will be muted.";
            %words2 = "Muting a player prevents them from speaking.";
            
        case "unmute":
            %words = "/unmute [NameofVictim]. The [NameofVictim] must be muted, and is the person being unmuted. Unmuting cancels a mute.";
			
		case "phone":
			%words = "The Phone system has been taken out due to a better version on RTB, by JJStorm. The file is still there, it just doesn't execute it.";
			
		case "kickReason":
			%words = "You can kick people for a reason now! Just say /kick [Name] [Multi-Word-Reason] to kick them with that reason.";
			
		case "roast":
			%words = "/roast [Name] [Duration]. Duration in number of seconds. You can /extinguish [Name], too.";
			
		case "launchntumble":
			%words = "/launchnTumble [Name] Will launch them tumble the player. You can also do /LNT [NAME].";
			
		case "fly":
			%words = "Fly is a script by Munk. Say /fly to start flying, and /fly to stop.";
			
		case "version":
			%words = "Current MSP Version is: " @ $MSP::Version @ ".";
            
		default:
			%words = "Say /msp help for help.";
    }
    
    MSPTC(%client, %words, %words2);
}

function MSPTC(%client, %words1, %words2, %words3, %words4, %words5)
{
    for(%a = 1; %a < 5; %a++) 
    {
        if(%words[%a] !$= "")
        {
            %actualwords = "\c4---\c6" @  %words[%a];
			messageClient(%client,'',"" @ %actualwords);
        }
    }
}

function fcbn(%target)
{
	return findClientByName(%target);
}

function addNewsPrefs()
{
		$MSP::NewsMax = 5;
        for(%a = 1; %a <= 5; %a++)
        {
                RTB_registerPref("News Info " @ %a,"Munk's Server Pack","MSPCurrNews" @ %a,"string 180","System_MunkServerPack","",0,0);
        }
}

addNewsPrefs();

function tellNews(%this)
{
	for(%i=0; %i <= $MSP::NewsMax; %i++)
	{
		%words = $MSPCurrNews[%i];
		if(%words !$= "")
		{
				MSPTC(%this, %words);
		}
	}
}


function isInteger(%string)
{
	%search = "- 0 1 2 3 4 5 6 7 8 9";
	for(%i=0;%i<getWordCount(%search);%i++)
	{
		%string = strReplace(%string,getWord(%search,%i),"");
	}
	if(%string $= "")
		return true;
	return false;
}