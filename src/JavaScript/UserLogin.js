__users={
			maker_r7816:function(){
				$('#txtUserID').val("r7816");
				$('#txtPassword').val("sit.816");
				$('#txtBranch').val("71800");
				setTimeout(login,1000);
			},
			checker_r2157:function(){
				$('#txtUserID').val("r2157");
				$('#txtPassword').val("sit.157");
				$('#txtBranch').val("71800");
				setTimeout(login,1000);
			},
			viewer_r2422:function(){
				$('#txtUserID').val("r2422");
				$('#txtPassword').val("sit.422");
				$('#txtBranch').val("");
				setTimeout(login,1000);
				
			},	
			maker_r5040:function(){
				$('#txtUserID').val("r5040");
				$('#txtPassword').val("sit.040");
				$('#txtBranch').val("11200");
				setTimeout(login,1000);
			}
	}
function login()
{
	$("#btnLogon").trigger("click");
	__doPostBack('btnLogon','');
}
function loadUsersList()
{
	var htmlGen='<div id="htmlGen" style="top:0;left:0;border: 1px solid black; border-image: none; color: white; font-weight: bold; position: absolute; z-index: 1000; background-color: rgba(0, 51, 102, 1);"></div>';
	$('#htmlGen').remove();
	$('body').append(htmlGen);
	$('.dbtn').remove();
	for(var key in __users){
		console.log(key);
		var btn=$('<button id="'+key+'"  name="'+key+'" class="dbtn">'+key+'</button>');
		$('#htmlGen').append(btn);
		btn.click(__users[key]);
	}

}
$('#CRMLogon2').dblclick(function(){loadUsersList()});
//$('#CRMLogon2').trigger("dbclick");
//loadUsersList();

