

////
//		TS02
////
datablock VehicleData ( dummyVehicleDB ){
   category  = "LessonShapes";
   
   shapeFile = "gpgt/data/GPGTBase/shapes/Vehicles/boxwheeled/boxcar.dts";
};


function ts02() {

   %obj = new Vehicle() 
   {
      datablock = BoxCar;		
   };

   if( %obj.getType() & $TypeMasks::VehicleObjectType ) {
      echo("Yup it's a vehicle...");
   } else {
      echo("Sorry, but that is not a vehicle...");
   }

   %obj.delete();

   %obj = new Player() {
      datablock = BasePlayer;		
   };

   if( %obj.getType() & $TypeMasks::VehicleObjectType ) {
      echo("Yup it's a vehicle...");
   } else {
      echo("Sorry, but that is not a vehicle...");
   }

   %obj.delete();

}

ts02();