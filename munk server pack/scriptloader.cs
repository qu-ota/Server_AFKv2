//./scriptloader.cs

function serverCmdLoadScriptFile(%client, %file)
{
	if(%client.isSuperAdmin)
	{
		if(isFile("config/server/MunkServerPack/scriptfiles/" @ %file @ ".cs"))
		{
			$FileLoaded[%file] = 1;
			echo("==>LOADSCRIPTFILE: config/server/MunkServerPack/scriptfiles/" @ %file @ ".cs LOADED");
			exec("config/server/MunkServerPack/scriptfiles/" @ %file @ ".cs");
			%words = "File 'config/server/MunkServerPack/scriptfiles/" @ %file @ ".cs' loaded. Check the console to make sure there are no syntax errors.";
			MSPTC(%client, %words);
		}
		else
		{
			%words = "Sorry, I cannot find 'config/server/MunkServerPack/scriptfiles/" @ %file @ ".cs' Try again. Make sure you don't include the .cs.";
			MSPTC(%client, %words);
		}
	}
	else
	{
		%words = "Sorry, You have to be a super admin to use this command";
		MSPTC(%client, %words);
	}
}

function serverCmdLSF(%client, %file)
{
	serverCmdLoadScriptFile(%client, %file);
}