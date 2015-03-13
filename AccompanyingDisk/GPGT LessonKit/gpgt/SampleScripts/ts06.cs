
////
//		TS06
////
function printAreaOfSquare ( %Square ) {
   echo("The area of this square is: ", %Square.width * %Square.height);
}

function ts06() {

   %obj = new ScriptObject( Square ) {
      width  = 10.0;
      height = 5.0;
   };

   printAreaOfSquare( %obj );

   %obj.delete();
}

ts06();