
////
//		TS07
////
function Square::printArea( %this ) {
   echo("The area of this square is: ", %this.width * %this.height);
}

function Circle::printArea( %this ) {
   echo("The area of this circle is: ", 
      %this.radius * %this.radius * 3.1415962 );
}

function ts07() {

   %obj0 = new ScriptObject( Square ) {
      width  = 10.0;
      height = 5.0;
   };

   %obj1 = new ScriptObject( Circle ) {
      radius  = 10.0;
   };


   Square.printArea();

   %obj1.printArea();

   %obj0.delete();

   %obj1.delete();

}

ts07();