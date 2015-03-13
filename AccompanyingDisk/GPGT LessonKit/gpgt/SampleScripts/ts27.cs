
////
//		TS27
////
function ts27() {

   %anObject = "ScriptObject";

   %evalString = "%obj = new %anObject();";

   eval(%evalString);

   if( isObject( %obj ) ) 
      echo("It is an object.  Congratulations!");
   else
      echo("It is NOT an object.  Try again...");

}


ts27();