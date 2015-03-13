
////
//		TS09
////

function Animal::printMessage( %this ) {
   echo("... ", %this.superClass, ".");
}

function Doberman::printMessage( %this ) {
   echo("A ", %this.getName(), " is a ...");
   Parent::printMessage( %this );
}

function Canine::printMessage( %this ) {
   echo("... ", %this.class, " which is an ...");
   Parent::printMessage( %this );
}


function ts09() {

   %obj = new ScriptObject( Doberman ) {
      class = "Canine";
      superClass = "Animal";
   };

   %obj.printMessage();
}

ts09();