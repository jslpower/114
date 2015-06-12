

function CheckOrbberInstalled()
{
	try{
		var objhttp=new ActiveXObject("orbbertongye114.Install");
		if(objhttp != null)
			return true;
		else
			return false;
	}
	catch(e)
	{
		return false;
	}
	
	return false;
}
