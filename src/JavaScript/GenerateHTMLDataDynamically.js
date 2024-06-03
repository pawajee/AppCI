//debugger;
function generateTable(data)
{
	var rows="";
	for(var i=0;i<data.length;i++)
	{
		rows+=`
		<tr>${getRow(data[i])}
		</tr>`;
	}
	return `
	<table>${getHeader(data)}
		<tbody>${rows}</tbody>
	</table>
	`;
}
function getHeader( data)
{
	var tCols="";
	for(var key in data[0])
	{
		tCols+= `
			<th>${key}</th>`;
	}
		return `
		<thead>${tCols}
		</thead>`
}
function getRow( row)
{
	var tCols=""
	for(var key in row)
	{
		//console.log(`idx: ${i} - key: ${key}`);
		tCols+= `
			<td>${row[key]}</td>`;
	}
	return tCols;
}

__users=[
			{RollNo:1, Name:"Ammar", City:"FSD"},
			{RollNo:2, Name:"Qasim", City:"GJR"}			
		];
		
__cars =[
			{CarId:1, CarBrand:"Honda", CarModel:"Civic" , CarYear:2015},
			{CarId:2, CarBrand:"Honda", CarModel:"Acord" , CarYear:2023},			
			{CarId:3, CarBrand:"Toyota", CarModel:"Carola" , CarYear:2023},				
		];
console.log("generating users table");
console.log(generateTable(__users));
console.log("generating cars table");

console.log(generateTable(__cars));
 
