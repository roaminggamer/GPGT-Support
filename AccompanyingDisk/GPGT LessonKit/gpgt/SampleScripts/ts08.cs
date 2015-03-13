
////
//		TS08
////
function ts08() {

   %obj = new ScriptObject( Square0 ) {
      class = "Square";
      width  = 10.0;
      height = 5.0;
   };

   %obj = new ScriptObject( Square1 ) {
      class = "Square";
      width  = 10.0;
      height = 50.0;
   };

   Square0.printArea();

   Square1.printArea();

}

ts08();