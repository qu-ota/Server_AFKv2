//all modules will be loaded here. if adding new modules, make sure you put in a line here.
//example: exec("./<MODULE>.cs");
//capitalization not required, do not include <> when adding line

$afkNameChanges = 1;
exec("*/back.cs");
exec("*/afk.cs");
exec("*/brb.cs");

functon serverCmdToggleAfkNameChanges(%client)
{
	if(%client.isSuperAdmin)
	{
		if($afkNameChanges == 1)
		{
			$afkNameChanges = 0;
			//to be continued soon
