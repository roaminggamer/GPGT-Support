
////
//		TS38
////
function ts38() {

   %toClean = "<tab:60>  I'm,<spush><font: arial: 8> all, clean,____    <spop>";

   echo("\c3Cleaning up an ugly string...");

   echo(%toClean);

   echo("\n", "\c3Remove Mark-up language...");

   %toClean = stripMLControlChars( %toClean );

   echo(%toClean);

   echo("\n", "\c3Remove leading and trailing spaces...");

   %toClean = trim( %toClean );

   echo(%toClean);

   echo("\n", "\c3Remove commas...");

   %toClean = stripChars( %toClean , ",");

   echo(%toClean);

   echo("\n", "\c3Get rid of underscores...");

   %toClean = stripTrailingSpaces( %toClean );

   echo(%toClean);

}

ts38();
