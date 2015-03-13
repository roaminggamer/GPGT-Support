////
//		BT19
////
function ItemData::GetFields( %ItemDbID ) 
{    
    echo ("Calling ItemData::GetFields () ==> on object" SPC %ItemDbID);    
    echo (" category     =>" SPC %ItemDbID.category);    
    echo (" shapeFile    =>" SPC %ItemDbID.shapeFile);    
    echo (" mass         =>" SPC %ItemDbID.mass);    
    echo (" elasticity   =>" SPC %ItemDbID.elasticity);    
    echo (" friction     =>" SPC %ItemDbID.friction);    
    echo (" pickUpName   =>" SPC %ItemDbID.pickUpName);      
}

function bt19() {

	BaseItem.GetFields();

}

bt19();