
////
//		TS13
////
function listAllFiles( %pattern ) {
   %filename = findFirstFile( %pattern );

   while("" !$= %filename ) {
      echo(%filename);
      %filename =  findNextFile(%pattern );
   }
}


function ts13() {

   listAllFiles("*gui*");

}

ts13();