//./status.cs

package statusmod
{
        function servercmdafk(%client, %c1, %c2, %c3, %c4, %c5, %c6, %c7, %c8, %c9, %c10, %c11, %c12, %c13, %c14, %c15, %c16, %c17, %c18, %c19, %c20)
        {
                if(%client.status $= "afk")
                {
                        messageclient(%client,'',"<color:F660AB>You are already afk!");
                }
                else
                {
                        if(%client.canStatus)
                        {
                                if(%c1 !$= "")
                                {
                                    for(%a = 1; %a < 21; %a++)
                                    {
                                            if(%c[%a] !$= "")
                                            {
                                                    %client.reason = %client.reason SPC %c[%a];
                                            }
                                    }
                                        %client.reason = stripMLControlChars(trim(%client.reason));
                                        messageall('',"\c3" @ %client.name @ "<color:C8B560> is now AFK: \c4" @ %client.reason);
                                        %client.status = "afk";
                                        %client.canStatus = 0;
                                        schedule(7000, 0, canstatusnow, %client);
                                }
                                else
                                {
                                        %client.reason = "Away from Keyboard";
                                        messageall('',"\c3" @ %client.name @ "<color:C8B560> is now AFK: \c4" @ %client.reason);
                                        %client.status = "afk";
                                        %client.canStatus = 0;
                                        schedule(7000, 0, canstatusnow, %client);
                                }
                        }
                        else
                        {
                                messageClient(%client,'',"\c4---\c6Please do not spam.");
                        }
				}
        }
        
        function servercmdbrb(%client, %c1, %c2, %c3, %c4, %c5, %c6, %c7, %c8, %c9, %c10, %c11, %c12, %c13, %c14, %c15, %c16, %c17, %c18, %c19, %c20)
        {
                if(%client.status $= "afk")
                {
                        messageclient(%client,'',"<color:F660AB>You are already afk!");
                }
                else
                {
                        if(%client.canStatus)
                        {
                                if(%c1 !$= "")
                                {
                                    for(%a = 1; %a < 21; %a++)
                                    {
                                        if(%c[%a] !$= "")
                                        {
                                                %client.reason = %client.reason SPC %c[%a];
                                        }
                                    }
                                        %client.reason = stripMLControlChars(trim(%client.reason));
                                        messageall('',"\c3" @ %client.name @ "<color:C8B560> will Be Right Back: \c4" @ %client.reason);
                                        %client.status = "afk";
                                        %client.canStatus = 0;
                                        schedule(7000, 0, canstatusnow, %client);
                                }
                                else
                                {
                                        %client.reason = "Be Right Back!";
                                        messageall('',"\c3" @ %client.name @ "<color:C8B560> is now AFK: \c4" @ %client.reason);
                                        %client.status = "afk";
                                        %client.canStatus = 0;
                                        schedule(7000, 0, canstatusnow, %client);
                                }
                        }
                        else
                        {
                                        messageClient(%client,'',"\c4---\c6Please do not spam!");
                        }
                }
        }
        
        function serverCmdBack(%client)
        {
                if(%client.status $= "here")
                {
                        messageclient(%client,'',"<color:F660AB>You are already there!");
                } 
                else
                {
                        messageall('',"\c3" @ %client.name @ "<color:C8B560> is back!"); 
						%client.reason = "";
                        %client.status = "here"; 
                }
        }
		
		function serverCmdFoodBreak(%client)
		{
			%random = getRandom(1, 5);
			
			switch(%random)
			{
				case 1:
					%reason = "Food Break!!!";
					
				case 2:
					%reason = "FOOD BREAK!!!";
					
				case 3:
					%reason = "I AM HUNGRY!";
					
				case 4:
					%reason = "I need some food....";
					
				case 5:
					%reason = "I'm hungry. Taking a food break.";
			}
			
			serverCmdAfk(%client, %reason);
		}
        
		function serverCmdBathroomBreak(%client)
		{
			%random = getRandom(1, 5);
			
			switch(%random)
			{
				case 1:
					%reason = "I have to go PEE!!!";
					
				case 2:
					%reason = "Bathroom BREAK!";
					
				case 3:
					%reason = "Going to the Bathroom.";
					
				case 4:
					%reason = "#2";
					
				case 5:
					%reason = "Taking a bathroom break.";
			}
			
			serverCmdAfk(%client, %reason);
		}

        function serverCmdStatus(%client, %target)
        {
                %targetcl = findClientByName(%target);        
                
                if(!IsObject(findClientByName(%target)))
                {
                        messageclient(%client,'',"<color:F660AB>Sorry, but I cannot find\c3" @ %target @ "\c6.");
                }
                else
                {
                        if(%targetcl.status $= "afk")
                        {
                                messageclient(%client,'',"\c3" @ %targetcl.name @ "\c6 is Afk: \c5" @ %targetcl.reason);
                        }
                        else
                        {
                                messageclient(%client,'',"\c3" @ %targetcl.name @ "\c6 is there!");
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
                                messageclient(%client,'',"\c3Current AFK List:");
                                messageclient(%client,'',"\c3" @ %cl.name @ "\c6 Is AFK: \c4" @ %cl.reason);
                        }
                }
        }
        
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
        
};
activatePackage(statusmod);
schedule(1000, 0, announceafkloop);

if(isFile("Add-Ons/System_ReturnToBlockland/server.cs"))
{
   if(!$RTB::RTBR_ServerControl_Hook)
      exec("Add-Ons/System_ReturnToBlockland/RTBR_ServerControl_Hook.cs");
   RTB_registerPref("Announce AFK","Munk's Server Pack","StatusMod::AnnounceAFK","bool","System_MunksServerPack",1,0,0);
   RTB_registerPref("Announce Time(Seconds)","Munk's Server Pack","StatusMod::LoopTime","int 60 1200","System_MunksServerPack",300,0,0);
}
else
{
        //Change these prefs if you don't have Return to Blockland.
   $StatusMod::AnnounceAFK = 1;
   $StatusMod::LoopTime = 300;
}