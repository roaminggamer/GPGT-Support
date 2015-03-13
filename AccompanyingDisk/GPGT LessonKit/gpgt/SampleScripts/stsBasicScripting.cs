//
// Remaining basic training scripts from book.
//

////
//		echoRepeat
////
function echoRepeat (%echoString, %repeatCount) {

    for (%count = 0; %count < %repeatCount; %count++)
    {
        echo(%echoString);
    }

}

////
//		echoRepeatRecurse
////
function echoRepeatRecurse (%echoString, %repeatCount) {

    if (%repeatCount > 0) 
    {
        echo(%echoString);
        echoRepeatRecurse(%echoString, %repeatCount--);
    }

}


////
//		test_packages( N ); // N == 0, 1, or 2
////
//
// Define an initial function: demo()
//
function demo() 
{
    echo("Demo definition 0");
}

//
// Now define three packages, each implementing 
// a new instance of: demo()
//
package DemoPackage1 
{
	  function demo() 
	{
    		echo("Demo definition 1");
	}
};

package DemoPackage2 
{
	function demo() 
	{
    	echo("Demo definition 2");
	}
};

package DemoPackage3 
{
	function demo() 
	{
    	echo("Demo definition 3");
    	echo("Prior demo definition was=>");
    	Parent::demo(); 
	}
};

//
// Finally, define some tests functions
//
function test_packages(%test_num) 
{
    if( ! sscls() ) return;
    switch(%test_num) 
	  {
    // Standard usage
    case 0: 
        echo("----------------------------------------");
        echo("A packaged function over-rides a prior"); 
        echo("defintion of the function, but allows");
        echo("the new definition to be \'popped\' "); 
        echo("off the stack."); 
        echo("----------------------------------------");
        demo();
        ActivatePackage(DemoPackage1);
        demo();
        ActivatePackage(DemoPackage2);
        demo();
        DeactivatePackage(DemoPackage2);
        demo();
        DeactivatePackage(DemoPackage1);
        demo();
    // Parents
    case 1: 
        echo("----------------------------------------");
        echo("The Parent for a packaged function is"); 
        echo("always the previously activated ");
        echo("packaged function."); 
        echo("----------------------------------------");
        demo();
        ActivatePackage(DemoPackage1);
        demo();
        ActivatePackage(DemoPackage3);
        demo();
        DeactivatePackage(DemoPackage3);
        DeactivatePackage(DemoPackage1);
        echo("----------------------------------------");

        demo();
        ActivatePackage(DemoPackage1);
        demo();
        ActivatePackage(DemoPackage2);
        demo();
        ActivatePackage(DemoPackage3);
        demo();
        DeactivatePackage(DemoPackage3);
        DeactivatePackage(DemoPackage2);
        DeactivatePackage(DemoPackage1);
    // Stacking oddities
    case 2: 
        echo("----------------------------------------");
        echo("Deactivating a \'tween\' package will"); 
        echo("deactivate all packages \'stacked\' after");
        echo("it."); 
        echo("----------------------------------------");
        demo();
        ActivatePackage(DemoPackage1);
        demo();
        ActivatePackage(DemoPackage2);
        demo();
        DeactivatePackage(DemoPackage1);
        demo();
    }
}

