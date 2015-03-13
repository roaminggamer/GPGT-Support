
////
//		TS29
////
function ts29() {

   %anObject = "ScriptObject";

   %obj = new (%anObject)();

   eval(%evalString);

   if( isObject( %obj ) ) 
      echo("It is an object.  Congratulations!");
   else
      echo("It is NOT an object.  Try again...");


}

ts29();