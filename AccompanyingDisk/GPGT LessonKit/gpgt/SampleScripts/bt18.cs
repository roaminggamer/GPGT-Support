
////
//		BT18
////

datablock StaticShapeData ( SimpleTarget1 ) {
	category  = "LessonShapes";
};

function GameBase::DoIt( %this ) 
{
    echo ( "Calling StaticShape::DoIt() ==> on object" SPC %this );
}


function bt18( %sampleCase ) {

	%sampleCase = strlwr( %sampleCase );


	%myTarget = new StaticShape( CoolTarget ) 
	{
	    position = "0 0 0";
	    dataBlock = "SimpleTarget1";
	};

	
	switch$ ( %sampleCase ) {
	case "a":
		%myTarget.DoIt();

	case "b":

		%myTarget.DoIt();

		StaticShape::DoIt(%myTarget);

		CoolTarget.DoIt();

		"CoolTarget".DoIt();
		

		// The following three examples don't match the code in the book, because
		// we don't know the ID number of the object at this point.
		// So, instead, we'll get the ID and then use it.


		// IN BOOK => 100.DoIt()
		%tmpEvalStr =  %myTarget.getID() @ ".DoIt();";

		eval( %tmpEvalStr );

		
		// IN BOOK => "100".DoIt()
		%tmpEvalStr =  "\"" @ %myTarget.getID() @ "\"" @ ".DoIt();";

		eval( %tmpEvalStr );

		// IN BOOK => StaticShape::DoIt(100);

		StaticShape::DoIt( %myTarget.getID() );

	}

	// Following not in book.  Cleaning up.
	if ( isObject( %myTarget ) ) 
	    %myTarget.delete();

}

bt18( a ) ;

bt18( b ) ;