
////
//		TS26
////
$testvar::EDO = 10;

function doit(%name) {
   // Build a temporary var named -> 
   // "$testvar::%name", where %name is passed in
   %buildTheVar = "$testvar::" @ %name;
   echo(%buildTheVar);
   // Assign a new value to it
   %assignTheVar = %buildTheVar @ " = 20;";
   eval(%assignTheVar);
}

function ts26( %sampleCase ) {

   %sampleCase = strlwr( %sampleCase );

   switch$( %sampleCase ) {

   case "a":

      $testvar::EDO = 10;
      echo( $testvar::EDO );

   case "b":

      $testvar::EDO = 10;
      doit(EDO);
      echo($testvar::EDO);

   case "c":

      doit(TEST);
      echo($testvar::TEST);

   }

}

ts26(a);