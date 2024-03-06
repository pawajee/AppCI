$('.next_div').unbind('click').click(function(e){
	$('#epaper_image').attr('src')
	var fName=$('#epaper_image').attr('src');
	var arrFName = fName.split('/');
	var fParts=arrFName[arrFName.length-1].replace('.jpg','').split('-');
	var nextFileNo=((~~(fParts[fParts.length-1]))+1);
	
	fParts[fParts.length-1]=nextFileNo.toString().padStart(3,'0');
	
	arrFName[arrFName.length-1]=fParts.join('-')+'.jpg';
	fName=arrFName.join('/');
	console.log(fName);
	$('#epaper_image').attr('src',fName);
	e.preventDefault();
	return false;
});
$('.prev_div').unbind('click').click(function(e){
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
	e.preventDefault();
	return false;
});
