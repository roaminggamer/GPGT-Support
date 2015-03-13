
////
//		TS10
////
function myTestDatablock::onAdd( %theDB, %theObj ) {

   echo("A new object: \cp\c2", %theObj.getName(), 
      "\c0 was created with the datablock: \c2", %theDB.getName() ) ;

}

function myTestDatablock::onRemove( %theDB, %theObj ) {

   echo("Deleting: \cp\c2", %theObj.getName(), 
      "\c0 created with the datablock: \cp\c2", %theDB.getName() ) ;

}

datablock StaticShapeData( myTestDatablock ) {
   category  = "LessonShapes";
};

function ts10() {

   %obj = new StaticShape( testObject ) {
      datablock = "myTestDatablock";    
   };

   %obj.delete();

}

ts10();