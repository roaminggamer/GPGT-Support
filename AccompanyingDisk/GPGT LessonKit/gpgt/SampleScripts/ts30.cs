
////
//		TS30
////
function ts30() {

   %test = 10;

   %printTest = "echo(\"" @ %test @ "\");";

   echo("eval(", %printTest, ") produces -->" );

   eval( %printTest );

}

ts30();