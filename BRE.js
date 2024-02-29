//*********************************************************************/
      function nothing(){
            console.log('condition NOT Met')
      }
      function setControlProperties(conf)
      {
            console.log('condition Met, setting GUI');
            //console.log(conf);
      }

      function executeRules(rules, model){
            for(var i=0;i<rules.length;i++){
                  let rule= rules[i];
                  let ruleExp= "let _=model;" +rule.condition + "?" + rule.then +":" + rule.otherWise+";";
                  
                  //console.log(ruleExp);
                  eval(ruleExp);
            }
      }
//*******************************Rules will come from configuration: TODO: prepare a designer to automatically generate the rules***************************************/      
      var rules=[
            {condition:"_.a==''", then: ' setControlProperties([{"textbox1":{disabled:false, value:"NewValue1"}},{"textbox2":{disabled:false, value:"NewValue2"}}])',otherWise:'nothing()'},
            {condition:"_.Amount==100 && _.Name=='Usman'", then: 'setControlProperties([{"CivilId":{disabled:false, value:""}},{"LastName":{disabled:false, value:"Mazhar"}}])',otherWise:'nothing()'}              
            
      ];
//**********************************************************************/           

//*********************************MODEL containing form values, TODO: it can contain control ids to avoid first reading values from the Form*************************************/            
      var model= {
            a:'',
            Amount: 1001,
            Name: "Usman"
            
      }
//**********************************************************************/           

//********************************Execution of the rules on Provided model**************************************/          
      executeRules(rules,model);
      
      
