//./rotationmessage.cs


function serverMessageRotation(%this)
{
	if($RM::Curr > $RM::Infos)
	{
		$RM::Curr = 1;
	}
	
	echo("Server Message Rotation - Sent");
	messageAll('',"" @ $RM::Begin SPC $RMInfo[$RM::Curr]);
	$RM::Curr++;
}

function servercmdforceRM(%client)
{
        schedule(1000, 0, serverMessageRotation);
}

function RMloop(%this)
{
        if($RM::Toggle)
        {
                schedule(100, 0, serverMessageRotation);
        }
        $RMInfoLoop = schedule($RMInfo::Time * 60000, 0, RMloop);
}

RMloop();
$RM::Curr = 1;




if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
        if(!$RTB::RTBR_ServerControl_Hook)
			exec("Add-Ons/System_ReturnToBlockland/RTBR_ServerControl_Hook.cs");
        RTB_registerPref("Server Message Rotation","Munk's Server Pack","RM::Toggle","bool","System_MunkServerPack",0,0,0);
		RTB_registerPref("Message Beginning","Munk's Server Pack","RM::Begin","string 30","System_MunkServerPack","\c6[\c1Server Info\c6]",0,0);
        RTB_registerPref("Number of Infos","Munk's Server Pack","RM::Infos","int 5 30","System_MunkServerPack",5,1,0);
        RTB_registerPref("Info Time","Munk's Server Pack","RMInfo::Time","int 3 30","System_MunkServerPack",5,1,0);
	RTB_registerPref("Server Info 1","Munk's Server Pack","RMInfo1","string 180","System_MunkServerPack","This is info 1",0,0);
	RTB_registerPref("Server Info 2","Munk's Server Pack","RMInfo2","string 180","System_MunkServerPack","This is info 2",0,0);
	RTB_registerPref("Server Info 3","Munk's Server Pack","RMInfo3","string 180","System_MunkServerPack","This is info 3",0,0);
	RTB_registerPref("Server Info 4","Munk's Server Pack","RMInfo4","string 180","System_MunkServerPack","This is info 4",0,0);
	RTB_registerPref("Server Info 5","Munk's Server Pack","RMInfo5","string 180","System_MunkServerPack","This is info 5",0,0);
        addInfoPrefs();
}
else
{
        $Rm::Toggle = 0;
        $RM::Infos = 0;
}

function addInfoPrefs()
{
        //$RM::Infos++;
        for(%a = 6; %a <= $RM::Infos; %a++)
        {
                RTB_registerPref("Server Info " @ %a,"Munk's Server Pack","RMInfo" @ %a,"string 180","System_MunkServerPack","This is info " @ %a,0,0);
        }
}

function serverCmdRegInfoPrefs(%client)
{
	if(%client.isAdmin)
	{
		addInfoPrefs();
	}
}