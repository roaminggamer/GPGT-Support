

////
//		TS31
////
function ts31() {

   %makeVarTest = "%newVar = 100;";

   echo("evaluating script --> ", %makeVarTest );

   eval( %makeVarTest );

   echo("%newVar == ", %newVar );

}

ts31();