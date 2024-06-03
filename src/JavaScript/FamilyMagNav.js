$(document).unbind('keydown').keydown(function(e){
	console.log('keydown');
	if(!!e.keyCode){
		console.log('keydown');
		
	}
	if(e.keyCode==37)
	{
		next();
	}
	else if(e.keyCode==39)
	{
		prev();
		
	}
});
$('.prev_div').unbind('click').click(prev);
$('.next_div').unbind('click').click(next);
function prev(e){
	$('#epaper_image').attr('src')
	var fName=$('#epaper_image').attr('src');
	var arrFName = fName.split('/');
	var fParts=arrFName[arrFName.length-1].replace('.jpg','').split('-');
	var nextFileNo=((~~(fParts[fParts.length-1]))-1);
	console.log(fParts);
	fParts[fParts.length-1]=nextFileNo.toString().padStart(3,'0');
	console.log(fParts);
	arrFName[arrFName.length-1]=fParts.join('-')+'.jpg';
	fName=arrFName.join('/');
	$('#epaper_image').attr('src',fName);
	
	
	
	if(!!e) e.preventDefault();
	return false;
	
}

function next(e){
		$('#epaper_image').attr('src')
	var fName=$('#epaper_image').attr('src');
	var arrFName = fName.split('/');
	var fParts=arrFName[arrFName.length-1].replace('.jpg','').split('-');
	var nextFileNo=((~~(fParts[fParts.length-1]))+1);
	console.log(fParts);
	fParts[fParts.length-1]=nextFileNo.toString().padStart(3,'0');
	console.log(fParts);
	arrFName[arrFName.length-1]=fParts.join('-')+'.jpg';
	fName=arrFName.join('/');
	$('#epaper_image').attr('src',fName);
	if(!!e) e.preventDefault();
	return false;
}
